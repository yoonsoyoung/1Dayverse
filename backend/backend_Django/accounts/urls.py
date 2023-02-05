from django.urls import path
from . import views
from rest_framework_jwt.views import obtain_jwt_token


urlpatterns = [
    path('signup/', views.signup),
    path('login/', views.login),
    path('check/username/<str:username>/', views.check_username),
    path('check/email/<str:email>/', views.check_email),
    path('logout/', views.logout),
    path('withdraw/<str:username>/', views.withdraw),
    path('profile/<str:username>/', views.profile),
    path('characterlist/', views.character_list),
    path('gettutor/<int:tutor_id>/', views.tutor_name)
]