 - 포팅 메뉴얼
    1. [Built With](https://www.notion.so/98546b981b95407face09f4bebed1959)
    2. [Getting Started](https://www.notion.so/98546b981b95407face09f4bebed1959)
        - [Prerequisites](https://www.notion.so/98546b981b95407face09f4bebed1959)
        - [Installation](https://www.notion.so/98546b981b95407face09f4bebed1959)
    3. [Setting](https://www.notion.so/98546b981b95407face09f4bebed1959)
    4. [Additional Settings](https://www.notion.so/98546b981b95407face09f4bebed1959)
    5. [Build & Deploy](https://www.notion.so/98546b981b95407face09f4bebed1959)
    6. [DB](https://www.notion.so/98546b981b95407face09f4bebed1959)
    7. [시연 시나리오]()

## Built With

This section should list any major frameworks that you built your project using. Leave any add-ons/plugins for the acknowledgements section. Here are a few examples.

- [Django](https://www.djangoproject.com/)
- [OpenJDK-8](https://openjdk.java.net/install/)
- [Maven](https://maven.apache.org/)
- [Unity](https://unity3d.com/kr/get-unity/download/archive)
- IDE
    - django-3.2.7
- Web Server
    - [Nginx](https://www.nginx.com/)
- DB
    - [mariaDB](https://mariadb.org/)
- Front
    - [NPM](https://www.npmjs.com/)

## Getting Started

### Prerequisites

```bash
$ sudo apt update && sudo apt -y upgrade

# Mysqldb, Openjdk, Maven, npm 설치
$ sudo apt install -y mysqldb-server &&
	sudo apt install openjdk-8-jdk &&
	sudo apt install maven &&
	sudo apt install npm

# python3, pip, venv 설치
$ sudo apt-get install python3
$ sudo apt-get install python3-pip python3-venv

# Django 및 라이브러리 설치
$ python3 -m venv venv
$ source venv/bin/activate
(venv) $ pip install django

# Nginx 웹서버 설치
$ sudo apt install nginx
```

### Installation

1. Project URL
    - [1Dayverse](https://k5c202.p.ssafy.io)
2. Clone the repo
    
    ```bash
    git clone https://lab.ssafy.com/s05-final/S05P31C202.git
    ```
    

## Setting

1. Server DB 배포

```bash
# Docker로 MySQL 설치
$ docker pull mysql
$ docker images

# 서버 로컬에 DB 저장소를 두고 컨테이너와 volume 공유
$ docker run -d -p 52000:3306 --name mysql-db -e MYSQL_ROOT_PASSWORD=password -v ~/mysqldata/:/var/lib/mysql mysql --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci
```

1. DB 설정

```bash
$ docker exec -it mysql bash
# root -u root -p

$ create database onedaydb;
$ CREATE USER 'oneclass'@'%' IDENTIFIED BY '5678';
$ GRANT ALL PRIVILEGES ON *.* TO 'oneclass'@'%';
$ flush privileges;

# 덤프파일로 db 세팅
Mysql > use onedaydb;
Mysql > source /exec/onedaydb.sql;
```

1. Nginx 설정

```bash
$ sudo vim /etc/nginx/sites-enabled/default
server {
        listen 80;
        
        server_name _;
				# http 로 들어 온 요청을 https 로 redirect
        return 301 https://$host$request_uri;
}
server {
         listen 443 ssl ;
         listen [::]:443 ssl ;

				server_name _;
				
        # Certificate
        ssl_certificate /etc/letsencrypt/live/k5c202.p.ssafy.io/fullchain.pem;

        # Private Key
        ssl_certificate_key /etc/letsencrypt/live/k5c202.p.ssafy.io/privkey.pem;
	# root /var/www/html;
	root /home/ubuntu/docker/jenkins/build/frontend/dist/;

	# Add index.php to the list if you are using PHP
  index index.html index.htm index.php;

	# 배포 들어오는 URL 설정
	location / {
              # First attempt to serve request as file, then
              # as directory, then fall back to displaying a 404.
              try_files $uri $uri/ /index.html;

  }

	# 8080 포트 : WebRTC
	location /api{
          proxy_pass http://localhost:8080/;
          proxy_http_version 1.1;
          proxy_set_header Connection "";

          proxy_set_header Host $host;
          proxy_set_header X-Real-IP $remote_addr;
          proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
          proxy_set_header X-Forwarded-Proto $scheme;
          proxy_set_header X-Forwarded-Host $host;
          proxy_set_header X-Forwarded-Port $server_port;
  }
	# 8000 포트 : Django
	location /od {
          include uwsgi_params;
          uwsgi_pass unix:/home/ubuntu/docker/jenkins/build/backend/config.sock;
          proxy_pass http://localhost:8000;
          proxy_http_version 1.1;
          proxy_set_header Connection "";
          proxy_set_header Host $host;
          proxy_set_header X-Real-IP $remote_addr;
          proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
          proxy_set_header X-Forwarded-Proto $scheme;
          proxy_set_header X-Forwarded-Host $host;
          proxy_set_header X-Forawrded-Port $server_port;
        }
	# 배포 서버 내 media 접근
  location /backend/media {
          alias /home/ubuntu/docker/jenkins/build/backend/backend_Django/media;
  }
```

```bash
# Nginx syntax 검사
$ sudo nginx -t

# Nginx 재시작
$ sudo service nginx restart
```

1. Jenkins 설정

```bash
# docker로 설치 및 서버 로컬과 volume 공유
$ docker run -d -u root -p 9090:8080 -v ~/docker/jenkins/build:/var/jenkins_home/workspace/pipeline_oneday --name=myjenkins myjenkins
```

## Additional Settings

1. HTTPS 적용

```bash
# certbot 저장소 설정 및 설치
$ sudo apt-get update
$ sudo apt-get install software-properties-common
$ sudo add-apt-repository universe
$ sudo add-apt-respository ppa:certbot/certbot
$ sudo apt-get update
$ sudo apt-get install certbot python3-certbot-nginx

# SSL 인증서 획득
$ sudo certbot --nginx -d k5c202.p.ssafy.io

# 인증서 자동 갱신
$ sudo certbot renew --dry-run
```

- HTTPS 적용으로 보다 보안을 높임

## Build & Deploy

1. Frontend
    
    ```bash
    $ cd frontend
    $ npm install
    $ npm run build
    ```
    
2. Backend
    
    2.1 django
    
    ```bash
    $ cd backend/scent
    $ python -m venv venv
    $ source venv/Scripts/activate
    $ pip install -r requirements.txt
    $ python manage.py runserver
    ```
    
3. Unity
    - Platform : WebGL
    - Build

## DB

- 계정: oneclass
- schema : onedaydb
