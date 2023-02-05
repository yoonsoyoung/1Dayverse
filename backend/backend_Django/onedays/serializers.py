from django.db import models
from django.db.models import fields
from rest_framework import serializers
from rest_framework.exceptions import ValidationError
from django.contrib.auth import get_user_model
import re

from .models import Lecture, Registration

class LectureSerializer(serializers.ModelSerializer):
    class Meta:
        model = Lecture
        fields = '__all__'


class LectureListSerializer(serializers.ModelSerializer):
    
    class Meta:
        model = Lecture
        fields = ('id', 'title', 'name', 'description', 'main_image')


class LectureDetailSerializer(serializers.ModelSerializer):

    class Meta:
        model = Lecture
        exclude = ('password',)


class ProfileLectureSerializer(serializers.ModelSerializer):

    class Meta:
        model = Lecture
        fields = ('id', 'name', 'title', 'description', 'password', 'validation')


class RegistrationSeralizer(serializers.ModelSerializer):

    class Meta:
        model = Registration
        fields = '__all__'