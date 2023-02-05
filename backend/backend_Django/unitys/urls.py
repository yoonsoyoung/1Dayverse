from django.urls import path
from . import views

urlpatterns = [
    path('profile/<str:username>/', views.profile),
    path('lecture/<str:username>/', views.lecture),
    path('character/', views.set_character),
]
