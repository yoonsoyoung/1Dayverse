from django.contrib.auth import get_user_model, authenticate, login, logout
from django.shortcuts import get_list_or_404, get_object_or_404

# Create your views here.
from rest_framework import status
from rest_framework import serializers
from rest_framework.decorators import api_view
from rest_framework.response import Response
from rest_framework.serializers import Serializer

from .models import Character, Tutee, Tutor, User
from .serializers import CharacterSerializer, CharacterlistSerializer, ProfileSerializer, UserSerializer, ProfileModifySerializer, TutorNameSerializer
from onedays.models import Lecture
from onedays.serializers import ProfileLectureSerializer


@api_view(['POST'])
def signup(request):
	# Client에서 온 데이터를 받아서
    password = request.data.get('password')
    password_confirmation = request.data.get('passwordConfirmation')
		
	# 패스워드 일치 여부 체크
    if password != password_confirmation:
        return Response({'error': '비밀번호가 일치하지 않습니다.'}, status=status.HTTP_400_BAD_REQUEST)
		
	# UserSerializer를 통해 데이터 직렬화
    serializer = UserSerializer(data=request.data)
    
	# validation 작업 진행 -> password도 같이 직렬화 진행
    if serializer.is_valid(raise_exception=True):
        user = serializer.save()

        # 튜터, 튜티 나누기
        teachable = request.data.get('teachable')

        if teachable == "0":
            tutee = Tutee()
            tutee.user_id = user.id
            tutee.save()
        elif teachable == "1":
            tutor = Tutor()
            tutor.user_id = user.id
            tutor.save()
        else:
            return Response({'error': '강사/학생을 체크해주세요.'}, status=status.HTTP_400_BAD_REQUEST)

        # password는 직렬화 과정에는 포함 되지만 → 표현(response)할 때는 나타나지 않는다.
        return Response(serializer.data, status=status.HTTP_201_CREATED)


@api_view(['POST'])
def login(request):

    email = request.POST['email']
    password = request.POST['password']
    teachable = request.POST['teachable']

    if User.objects.filter(email=email).exists():
        getUser = User.objects.get(email=email)
        if getUser.teachable == int(teachable):
            if getUser.password == password:
                request.session['user'] = getUser.id
                request.session['login'] = True
                serializer = UserSerializer(getUser)
                data = {
                    'session': request.session,
                    'user': serializer.data
                }
                return Response(data, status=status.HTTP_200_OK)
            else:
                request.session['login'] = False
                return Response({'error': '비밀번호가 틀렸습니다.'}, status=status.HTTP_400_BAD_REQUEST)
        else:
            request.session['login'] = False
            return Response({'error': '강사/학생 체크를 확인해주세요.'}, status=status.HTTP_400_BAD_REQUEST)
    else:
        request.session['login'] = False
        return Response({'error': '존재하지 않는 이메일입니다.'}, status=status.HTTP_400_BAD_REQUEST)


# 닉네임 체크
@api_view(['GET'])
def check_username(request, username):
    if User.objects.filter(username=username).exists():
        data = {
            'available': 0,
            'message': '이미 존재하는 닉네임입니다.'
        }
        return Response(data, status=status.HTTP_400_BAD_REQUEST)
    else:
        data = {
            'available': 1,
            'message': '사용 가능한 닉네임입니다.'
        }
        return Response(data, status=status.HTTP_200_OK)


# 이메일 체크
@api_view(['GET'])
def check_email(request, email):
    if User.objects.filter(email=email).exists():
        data = {
            'available': 0,
            'message': '이미 존재하는 이메일입니다.'
        }
        return Response(data, status=status.HTTP_400_BAD_REQUEST)
    else:
        data = {
            'available': 1,
            'message': '사용 가능한 이메일입니다.'
        }
        return Response(data, status=status.HTTP_200_OK)


# 로그아웃 - 세션값 삭제
@api_view(['POST'])
def logout(request):
    if request.session.get('user'):
        request.session['login'] = False
    return Response(request.session, status=status.HTTP_200_OK)


# 탈퇴
@api_view(['DELETE'])
def withdraw(request, username):
    user = get_object_or_404(User, username=username)
    user.delete()
    return Response({"status":"회원탈퇴"}, status=status.HTTP_204_NO_CONTENT)


# 프로필, 프로필 수정
@api_view(['GET', 'PUT'])
def profile(request, username):
    user = get_object_or_404(User, username=username)

    if request.method == 'GET':

        # 유저 정보
        serializer = ProfileSerializer(user)
        userId = serializer.data['id']
        characterId = serializer.data['character']

        # tutor일 경우 - 개인정보, 내 강의 목록
        if serializer.data['teachable'] == 1:
            tutor = get_object_or_404(Tutor, user=userId)
            tutorId = tutor.id
            lectures = Lecture.objects.filter(tutor=tutorId)
            l_serializer = ProfileLectureSerializer(lectures, many=True)

        else:
            # tutee일 경우 - 개인정보, 수강 강의 목록
            # 유저pk를 통해 튜티pk 가져오기
            tutee = get_object_or_404(Tutee, user=userId)
            tuteeId = tutee.id

            # tuteeId를 통해 수강중인 강의 목록 불러오기
            lectures = tutee.lecture_set.all()
            l_serializer = ProfileLectureSerializer(lectures, many=True)

        if Character.objects.filter(id=characterId).exists():
            character = Character.objects.filter(id=characterId).get()
            cserializer = CharacterSerializer(character)

            data = {
                'profile': serializer.data,
                'lectures': l_serializer.data,
                'character': cserializer.data["character_image"]
            }
        else:

            data = {
                    'profile': serializer.data,
                    'lectures': l_serializer.data,
                    'character': "characters/default.PNG"
                }

        return Response(data)
    
    elif request.method == 'PUT':
        serializer = ProfileModifySerializer(user, data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data)
        else:  
            data = {
                'message': '올바르지 않은 형식입니다.'
            }
            return Response(data, status=status.HTTP_400_BAD_REQUEST)


@api_view(['GET'])
def character_list(request):

    characters = Character.objects.all()
    serializer = CharacterlistSerializer(characters, many=True)

    return Response(serializer.data)


@api_view(['GET'])
def tutor_name(request, tutor_id):
    tutor = Tutor.objects.filter(pk=tutor_id).get()
    serializer = TutorNameSerializer(tutor)
    user_id = serializer.data["user"]
    
    user = User.objects.filter(pk=user_id).get()
    name = user.username
    return Response(name)
