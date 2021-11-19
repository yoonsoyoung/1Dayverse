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
    
    ![image](https://user-images.githubusercontent.com/32251962/142535655-febb1691-274a-4c0a-b137-540946886bc4.png)
    
2. 카카오 API 를 이용한 결제
    
    2.1 수강 정보 확인
    
    ![image](https://user-images.githubusercontent.com/32251962/142536164-eace72d2-8a8b-4a17-8060-22a98ef164b5.png)
    
    2.2 카카오 페이 결제
    
    ![image](https://user-images.githubusercontent.com/32251962/142536340-94697da4-0f42-49ef-85ec-66ce828e6b28.png)
    

### 1.2.2. 수강 관리

1. 강사와 학생은 마이페이지에서 자신의 수업을 확인하고 관리
2. 자신의 수업 교실 비밀번호 확인

### 1.2.3. Unity

1. 애니메이션과 건물 하이라이팅
    - 메인 화면 애니메이션
    
    ![image](https://user-images.githubusercontent.com/32251962/142536715-8125bc07-341c-4156-9d0b-695b159c93be.png)
    
    - 건물 하이라이팅
    
    ![image](https://user-images.githubusercontent.com/32251962/142555818-3e9380ea-c313-4bc3-a450-49d1e5fe9dfd.png)

2. 캐릭터 선택
    
    ![image](https://user-images.githubusercontent.com/32251962/142536922-1c4ddaad-1cd9-459e-8c83-dd4626c1f5ce.png)
    
3. 내 정보 모달
    
    ![image](https://user-images.githubusercontent.com/32251962/142538116-74d7e77d-ec3e-4b04-927f-a3d6e649e45d.png)
    
4. 간단한 게임을 접속할 수 있는 공간

### **1.2.2. WebRTC**

1. 화상 통화
    
    ![image](https://user-images.githubusercontent.com/32251962/142538370-01a0625a-0dde-43c8-b9a6-9a3f5b2349ec.png)
    
2. 채팅
    
    ![image](https://user-images.githubusercontent.com/32251962/142538523-809cef65-0b57-4f73-884c-8e7b134c7e0e.png)
    
3. 화면 공유
    
    ![image](https://user-images.githubusercontent.com/32251962/142538641-d23c8ab5-19cf-4fda-84e2-99ea549793d4.png)
    

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
        
        ![image](https://user-images.githubusercontent.com/32251962/142539046-a40155b0-7ec2-4c19-a6ee-c90cd772e3e5.png)
        
    - 10인
        
        ![image](https://user-images.githubusercontent.com/32251962/142541148-e20296e7-cccf-454d-96f9-59831b3e475a.png)
        
    - 20인
        
        ![image](https://user-images.githubusercontent.com/32251962/142541303-4c3744ad-23c1-49da-91d5-8b2564fe739e.png)
        
- 복도
    
    ![image](https://user-images.githubusercontent.com/32251962/142541488-250e7910-ebc5-45d5-aba8-df532635cff0.png)
    
- 라운지
    
    ![image](https://user-images.githubusercontent.com/32251962/142542519-186a57cd-080f-4f72-8a87-bf12a45e3bba.png)
    

### **3.2. 포톤 멀티 서버**

- 멀티플레이
    
    ![image](https://user-images.githubusercontent.com/32251962/142543713-a296fbac-54b7-4c58-a3c9-eddb9eb9ae4f.png)
    

### 3.3. 게임 공유

- 게임 오브젝트로 멀티 게임 링크 팝업

## **4. 아키텍처 상세**

- 아키텍처
    
    ![image](https://user-images.githubusercontent.com/32251962/142543897-bcc09e55-98fb-4479-8933-36b6c5fb72c9.png)
    
- ERD
    
    ![image](https://user-images.githubusercontent.com/32251962/142544049-cc38197e-f9b4-4d88-a1f9-21239f9a21f8.png)
    

## **5. 레이아웃 및 디자인**

- 와이어 프레임 (Figma)
    - Unity
    
    ![image](https://user-images.githubusercontent.com/32251962/142544217-292fa140-4fc7-4527-ab2e-99f60b5b7311.png)
    
    - Web
    
    ![image](https://user-images.githubusercontent.com/32251962/142544383-75246e4e-280a-4e0b-89e2-4d69a54c954f.png)
    

## 6. UCC
[![UCC](https://img.youtube.com/vi/oCjh1UEKajo/0.jpg)](https://youtu.be/oCjh1UEKajo)
