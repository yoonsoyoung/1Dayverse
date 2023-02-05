from decimal import Context
from django.shortcuts import get_list_or_404, get_object_or_404, render
from rest_framework.decorators import api_view
from rest_framework.response import Response
from rest_framework import serializers, status

from accounts.models import Tutor, Tutee
from .models import Lecture, Registration
from .serializers import LectureListSerializer, LectureSerializer, LectureDetailSerializer

# 강의 등록
@api_view(['POST'])
def register(request):
    # 프론트에서 유저pk를 가져오면
    # 유저pk를 통해 튜터pk 가져오기
    # print(request.data)
    userId = request.data['tutor']
    tutor = get_object_or_404(Tutor, user=userId)
    tutorId = tutor.id

    # 튜터pk를 데이터에 새로 저장
    data = request.data.copy()
    data['tutor'] = tutorId

    serializer = LectureSerializer(data=data)
    if serializer.is_valid():
        serializer.save()
        return Response(serializer.data, status=status.HTTP_201_CREATED)
    else:
        data = {
            'message': '올바르지 않은 형식입니다.'
        }
        return Response(data, status=status.HTTP_400_BAD_REQUEST)


# 카테고리별 강의
@api_view(['GET'])
def category(request, c_num):
    lectures = Lecture.objects.filter(category=c_num, validation=1)
    serializer = LectureListSerializer(lectures, many=True)
    return Response(serializer.data)


# 강의 세부정보
@api_view(['GET'])
def detail(request, l_num):
    lecture = get_object_or_404(Lecture.objects.filter(pk=l_num))
    serializer = LectureDetailSerializer(lecture)

    cnt = Registration.objects.filter(lecture=l_num).count()
    
    if serializer.data['room_size'] > cnt:
        enroll = True
        remain = serializer.data['room_size'] - cnt
    else: 
        enroll = False
        remain = 0

    data = {
        'detail': serializer.data,
        'max': serializer.data['room_size'],
        'now': cnt,
        'enroll': enroll,
        'remain': remain,
    }
    return Response(data)


# 강의 등록 여부 체크
@api_view(['GET'])
def check(request, user, lecture):
    # 프론트에서 유저pk를 가져오면
    # 유저pk를 통해 튜티pk 가져오기
    tutee = get_object_or_404(Tutee, user=user)
    tuteeId = tutee.id

    # 이미 등록한 유저인지 확인
    if Registration.objects.filter(tutee=tuteeId, lecture=lecture).exists():
        check = False
    else:
        check = True
    return Response(check)


@api_view(['POST'])
def search(request):
    query = request.data['q']
    lectures = Lecture.objects.filter(title__icontains=query)
    lecture_list = LectureListSerializer(lectures, many=True)
    return Response(lecture_list.data)