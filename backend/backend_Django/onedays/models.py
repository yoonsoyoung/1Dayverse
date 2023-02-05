from django.db import models
from datetime import datetime
from accounts.models import Tutor, Tutee, User
from django.core.validators import MaxValueValidator, MinValueValidator


# Create your models here.
class Category(models.Model):
    name = models.CharField(max_length=20)

class Lecture(models.Model):
    tutor = models.ForeignKey(Tutor, on_delete=models.CASCADE)
    tutees = models.ManyToManyField(Tutee, through='Registration')
    name = models.CharField(max_length=30)
    title = models.CharField(max_length=100, default='title')
    main_image = models.ImageField(upload_to='images/', null=True, blank=True)
    category = models.ForeignKey(Category, on_delete=models.CASCADE)
    password = models.CharField(max_length=30)
    start = models.TimeField(default=datetime.now, blank=True, null=True)
    end = models.TimeField(default=datetime.now, blank=True, null=True)
    date = models.DateField(default=datetime.now, blank=True, null=True)
    description = models.TextField()
    room_size = models.PositiveIntegerField(validators=[MinValueValidator(1), MaxValueValidator(20)])
    price = models.PositiveIntegerField()
    lecture_cnt = models.PositiveIntegerField(validators=[MinValueValidator(1), MaxValueValidator(7)])
    validation = models.BooleanField(default=False)


class Registration(models.Model):
    lecture = models.ForeignKey(Lecture, on_delete=models.CASCADE)
    tutee = models.ForeignKey(Tutee, on_delete=models.CASCADE)

class Attendance(models.Model):
    registration = models.ForeignKey(Registration, on_delete=models.CASCADE)
    arrive_at = models.DateTimeField(auto_now=True)
    status = models.IntegerField(default=0)

