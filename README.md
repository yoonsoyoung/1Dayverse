# 1Dayverse
메타버스 환경의 원데이클래스
### 목차

---

0. 팀 소개

1. 서비스 소개

2. 기술 스택

3. 데이터 수집 및 추천 알고리즘

4. 아키텍처 상세

5. 레이아웃 및 디자인

6. UCC

---

## **0. 팀 소개**

- **팀장** 박준수 : Frontend, Unity, Photon
- 팀원 류지우 : Unity, Character, Unity-http
- 팀원 성루비 : Backend, Unity, Chatting, Audio
- 팀원 윤소영 : Frontend, WebRTC, Devops
- 팀원 이혜은 : Frontend, Unity, Multi User

### **🗓️구현 기간**

2021.10.11 - 11.18

## **1. 서비스 소개**

> 메타버스 환경의 원데이 클래스
> 

### **1.1. 타겟 고객군**

- 온라인에서도 현장감 있는 수업을 원하는 강사
- 비대면 상황에도 다양한 수업을 경험하고픈 학생
- 시간적, 공간적 제약 없이 취미를 즐기고픈 직장인

### **1.2. 주요 기능**

### **1.2.1. 수업 개설 및 신청**

1. 인가된 강사만 수업 개설
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/103f4006-cca3-4493-a479-b6f008f5d38a/Untitled.png)
    
2. 카카오 API 를 이용한 결제
    
    2.1 수강 정보 확인
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/c23a02e2-5791-4188-9dc0-45845caa8db2/Untitled.png)
    
    2.2 카카오 페이 결제
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/27973f6a-4830-4f85-90ab-26d11a29584b/Untitled.png)
    

### 1.2.2. 수강 관리

1. 강사와 학생은 마이페이지에서 자신의 수업을 확인하고 관리
2. 자신의 수업 교실 비밀번호 확인

### 1.2.3. Unity

1. 애니메이션과 건물 하이라이팅
    - 메인 화면 애니메이션
    
    ![unity_main.gif](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/29012fa0-306b-444a-8671-1499019b6e29/unity_main.gif)
    
    - 건물 하이라이팅
    - 
2. 캐릭터 선택
    
    ![unity_character.gif](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/e29603ab-72ce-47c6-be8e-d7f4c677bdcc/unity_character.gif)
    
3. 내 정보 모달
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/9601771b-e471-4276-a0d1-9203b8c56dc8/Untitled.png)
    
4. 간단한 게임을 접속할 수 있는 공간

### **1.2.2. WebRTC**

1. 화상 통화
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/0afb9fc6-653a-4258-abfc-a54317bb8921/Untitled.png)
    
2. 채팅
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/32e6f418-f803-4297-af2d-74c51bd8ec79/Untitled.png)
    
3. 화면 공유
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/eb99c7f2-f9ac-47da-9d9f-0761343af979/Untitled.png)
    

### 1.2.3. Photon Server

1. 방 생성
2. Multi Play

## **2. 기술 스택**

### **2.1. 유니티**

- Unity 2020.3.20f1
- Photon

### **2.2. 백엔드**

- Python 3.8.7
- Django 3.2.7
- Swagger
- Postman
- MySQL

### **2.3. 프론트엔드**

- Vue.js 3.2.20
- OpenVidu

### **2.4. 배포**

- AWS EC2
- Docker
- Nginx

### **2.4. 프로젝트 관리**

- GitLab
- Jenkins
- Notion
- Jira
- Google Sheet

## **3. 메타버스 환경 구축**

### **3.1. 3D 맵 구성**

- 교실
    - 5인
        
        ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/3ade6c51-32b6-4ede-97a4-2f0d91538246/Untitled.png)
        
    - 10인
        
        ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/cd9c9501-f3c9-459d-8e87-675af5a665f6/Untitled.png)
        
    - 20인
        
        ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/6f3d1183-129a-4ad1-a79c-1547dde1341b/Untitled.png)
        
- 복도
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/67edb95c-47bf-4753-9369-7ff57f1ec69d/Untitled.png)
    
- 라운지
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/bb43c871-08d3-4868-a67a-5c6cd510457c/Untitled.png)
    

### **3.2. 포톤 멀티 서버**

- 멀티플레이
    
    ![unity_hallway.gif](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/d5b4e1a7-d6ba-4379-ab46-67582b16c9e3/unity_hallway.gif)
    

### 3.3. 게임 공유

- 게임 오브젝트로 멀티 게임 링크 팝업

## **4. 아키텍처 상세**

- 아키텍처
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/42bed9ff-4e05-4360-a012-a41954f40af5/Untitled.png)
    
- ERD
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/49513106-3b88-4ea6-a7e0-a93bd6361577/Untitled.png)
    

## **5. 레이아웃 및 디자인**

- 와이어 프레임 (Figma)
    - Unity
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/02990c54-8e14-4e46-bed2-b91ff3c8e9e1/Untitled.png)
    
    - Web
    
    ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/3559157c-2c8b-4af6-91c4-2ba7f584391f/Untitled.png)
    

## 6. UCC

[https://youtu.be/oCjh1UEKajo](https://youtu.be/oCjh1UEKajo)
