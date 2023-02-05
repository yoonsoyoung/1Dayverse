from django.db import models
from django.contrib.auth.models import AbstractUser

class Character(models.Model):
    name = models.CharField(max_length=20)
    character_image = models.CharField(max_length=200, null=True)

# Create your models here.
class User(AbstractUser):
    username = models.CharField(max_length=150, unique=True)
    email = models.EmailField(max_length=100, unique=True)
    password = models.CharField(max_length=200)
    character = models.ForeignKey(Character, on_delete=models.CASCADE, default=1, blank=True, null=True)
    profile_image = models.ImageField(blank=True, null=True)
    birth_day = models.CharField(max_length=15)
    phone_number = models.CharField(max_length=20)
    teachable = models.IntegerField()
    

class Tutor(models.Model):
    user = models.ForeignKey(User, on_delete=models.CASCADE)


class Tutee(models.Model):
    user = models.ForeignKey(User, on_delete=models.CASCADE)

