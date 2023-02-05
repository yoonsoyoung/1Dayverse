from django.contrib.auth import get_user_model, authenticate, login, logout
from django.shortcuts import get_list_or_404, get_object_or_404

# Create your views here.
from rest_framework import status
from rest_framework import serializers
from rest_framework.decorators import api_view
from rest_framework.response import Response
from rest_framework.serializers import Serializer

from accounts.models import Tutee, Tutor, User, Character
from accounts.serializers import ProfileSerializer, UserSerializer, ProfileModifySerializer
from onedays.models import Lecture
from onedays.serializers import ProfileLectureSerializer


# Create your views here.
# 프로필, 프로필 수정
@api_view(['GET'])
def profile(request, username):
    user = get_object_or_404(User, username=username)

    if request.method == 'GET':

        # 유저 정보
        serializer = ProfileSerializer(user)            
        return Response(serializer.data)

@api_view(['GET'])
def lecture(request, username):
    user = get_object_or_404(User, username=username)

    if request.method == 'GET':

        # 유저 정보
        serializer = ProfileSerializer(user)
        userId = serializer.data['id']

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

        data = {
            'lectures': l_serializer.data
        }

        return Response(data)

@api_view(['POST'])
def set_character(request):
    get_data = request.data

    # 유니티에서 받아온 데이터
    username = get_data['nickname']
    character = get_data['character']


    data = {
        'character': get_data['character'],
    }

    # 해당 유저에 데이터 저장
    user = get_object_or_404(User, username=username)
    user.character_id = request.POST.get('character')
    user.save()

    return Response(data)


# 유니티 캐릭터 등록
# @api_view(['POST'])
# def set_character(request):
#     data = request.data


#     return Response('hi')

