from django.db import models
from django.db.models import fields
from rest_framework import serializers
from rest_framework.exceptions import ValidationError
from django.contrib.auth import get_user_model
import re

from accounts.models import Tutee, Tutor
from accounts.models import Character

User = get_user_model()

class UserSerializer(serializers.ModelSerializer):

    # write_only : 시리얼라이징은 하지만 응답에는 포함시키지 않음
    password = serializers.CharField(write_only=True)

    # def validate_username(self, value):
    #     regex = re.compile('^[\w\d_]{2,20}')
    #     if not regex.match(value):
    #         raise ValidationError('유효한 아이디 형식이 아닙니다.')
            
    #     return value
    
    def validate_password(self, value):
        regex = re.compile('^(?=.*[a-zA-z])(?=.*[0-9])(?=.*[$`~!@$!%*#^?&\\(\\)\-_=+])(?!.*[^a-zA-z0-9$`~!@$!%*#^?&\\(\\)\-_=+]).{3,20}$')
        if not regex.match(value):
            raise ValidationError('비밀번호 형식이 올바르지 않습니다.')

        return value

    class Meta:
        model = User
        fields = '__all__'


class ProfileSerializer(serializers.ModelSerializer):

    class Meta:
        model = User
        fields = ('id', 'username', 'birth_day', 'phone_number', 'profile_image', 'character', 'teachable')


class ProfileModifySerializer(serializers.ModelSerializer):

    class Meta:
        model = User
        fields = ('username', 'birth_day', 'phone_number', 'character', 'profile_image')


class CharacterSerializer(serializers.ModelSerializer):

    class Meta:
        model = Character
        fields = '__all__'

class CharacterlistSerializer(serializers.ModelSerializer):

    class Meta:
        model = Character
        fields = '__all__'

class TutorNameSerializer(serializers.ModelSerializer):

    class Meta:
        model = Tutor
        fields = '__all__'