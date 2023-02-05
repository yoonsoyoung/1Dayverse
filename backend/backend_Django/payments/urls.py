from django.urls import path
from . import views

urlpatterns = [
    path('ready/', views.ready),
    path('enroll/', views.enroll),
]
