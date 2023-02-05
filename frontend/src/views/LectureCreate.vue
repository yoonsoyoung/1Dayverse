<template>
  <div class="warp">
    <Navbar/>
    <main @click="shutDown">
      <div id="ClassCreateForm">
        <div id="selectTable">
          <div class="select-component" @click="onClickBtn('title')" id="title" style="background-color:#202D91; color:white;">강의 제목</div>
          <div class="select-component" @click="onClickBtn('category')" id="category">카테고리</div>
          <div class="select-component" @click="onClickBtn('tutor')" id="tutor">강사명</div>
          <div class="select-component" @click="onClickBtn('description')" id="description">강의 내용</div>
          <div class="select-component" @click="onClickBtn('time')" id="time">강의 시간</div>
          <div class="select-component" @click="onClickBtn('cost')" id="cost">강의 비용</div>
          <div class="select-component" @click="onClickBtn('size')" id="size">강의 크기</div>
          <div class="select-component" @click="onClickBtn('password')" id="password">강의 비밀번호</div>
        </div>
        <div id="selectedArea">
          <section v-if="idx=='title'" class="section-content" id="classTitle" >
            <h1 class="section-header">
              <div style="width:10%;"></div> 
              <span style="width:60%;" class="section-title">강의 이름 정하기 </span>
              <button style="width:10%;" @click="onClickBtn('category')" class="next-btn"><i class="fas fa-chevron-right"></i></button>
            </h1>
            <div class="section-container">
              <label>강의 이름</label>
              <input type="text" class="input-form" placeholder="15글자 미만으로 정해주세요" maxlength="15" v-model="title">
            </div>
          </section>
          <section v-if="idx=='category'" class="section-content">
            <h1 class="section-header">
              <div style="width:10%;"></div> 
              <span style="width:60%;" class="section-title">강의의 카테고리 정하기</span>
              <button style="width:10%;" @click="onClickBtn('tutor')" class="next-btn"><i class="fas fa-chevron-right"></i></button>
            </h1>
            <div class="section-container"> 
              <div class="category-card" id="excerciseCard" @click="selectCategory('excerciseCard')">
                <h3 class="half-hl">운동</h3>
              </div>
              <div class="category-card" id="hobbyCard" @click="selectCategory('hobbyCard')">
                <h3 class="half-hl">예술</h3>
              </div>
              <div class="category-card" id="langCard" @click="selectCategory('langCard')">
                <h3 class="half-hl">언어</h3>
              </div>          
            </div>
          </section>
          <section v-if="idx=='tutor'" class="section-content">
            <h1 class="section-header">
              <div style="width:10%;" ></div> 
              <span style="width:60%;" class="section-title">강사 확인</span>
              <button style="width:10%;" @click="onClickBtn('description')" class="next-btn"><i class="fas fa-chevron-right"></i></button>
            </h1>
            <div class="section-container" id="tutorContent">
              <div class="profile-image">
                <img :src="`@/../../../backend/media/${this.profile_image}`" alt="" class="tutor-image">
              </div>
              <div class="profile-content">
                <span>{{this.$store.getters["userStore/getUsername"]}}</span>
              </div>
            </div>
          </section>
          <section v-if="idx=='description'" class="section-content">
            <h1 class="section-header">
              <div style="width:10%;"></div> 
              <span style="width:60%;" class="section-title">강의의 설명 입력 정하기 </span>
              <button style="width:10%;" @click="onClickBtn('time')" class="next-btn"><i class="fas fa-chevron-right"></i></button>
            </h1>
            <div class="section-container" id="lectureContent">
              <div class="content-row">
                <label>강의 사진</label>
                <input type="file" @change="uploadImage" ref="lectureImages">
              </div>
              <div class="content-row" style="height: 100%;">
                <label for="">강의 내용</label>
                <textarea type="text" class="input-form" v-model="description"></textarea>
              </div>
            </div>
          </section>
          <section v-if="idx=='time'" class="section-content">
            <h1 class="section-header">
              <div style="width:10%;"></div> 
              <span style="width:60%;" class="section-title"> 강의의 시간 정하기 </span>
              <button style="width:10%;" @click="onClickBtn('cost')" class="next-btn"><i class="fas fa-chevron-right"></i></button>
            </h1>
            <div class="section-container" id="timeSection">
              <div>
                <label>강의 날짜</label>
                <input type="date" class="input-form" v-model="date">
              </div>
              <div>
                <label for="">시작 시간</label>
                <input type="time" class="input-form" v-model="start">
                <label for="">종료 시간</label>
                <input type="time" class="input-form" v-model="end">
              </div>
            </div>
          </section>
          <section v-if="idx=='cost'" class="section-content">
            <h1 class="section-header">
              <div style="width:10%;"></div> 
              <span style="width:60%;" class="section-title">강의의 금액 정하기</span>
              <button style="width:10%;" @click="onClickBtn('size')" class="next-btn"><i class="fas fa-chevron-right"></i></button>
            </h1>
            <div class="section-container">
              <label>강의 금액</label>
              <input type="text" class="input-form" v-model="cost" @change="commaCost">
            </div>
          </section>
          <section v-if="idx=='size'" class="section-content">
            <h1 class="section-header">
              <div style="width:10%;"></div> 
              <span style="width:60%;" class="section-title">강의의 인원 정하기</span> 
              <button style="width:10%;" @click="onClickBtn('password')" class="next-btn"><i class="fas fa-chevron-right"></i></button>
            </h1>
            <div class="section-container">
              <div class="class-card" id="smallCard" @click="selectSize('smallCard')">
                <h3>~5인</h3>
              </div>
              <div class="class-card" id="mediumCard" @click="selectSize('mediumCard')">
                <h3>~10인</h3>
              </div>
              <div class="class-card" id="largeCard" @click="selectSize('largeCard')">
                <h3>~20인</h3>
              </div>                
            </div>
          </section>
          <section v-if="idx=='password'" class="section-content">
            <h1 class="section-header">
              <div style="width:10%;"></div> 
              <span style="width:60%;" class="section-title">강의의 비밀번호 정하기</span>
              <button style="width:10%;" @click="onSubmit" id="submitBtn">제출</button>
            </h1>
            <div class="section-container">
              <label>강의 비밀번호</label>
              <input type="text" v-model="password" class="input-form" placeholder="8글자 이하로 적어주세요" maxlength="8" >
            </div>
          </section>
        </div>
      </div>
    </main>
  </div>
</template>

<script>
import axios from "axios"
import Navbar from '@/components/Navbar.vue'

export default {
  name : "ClassCreate",
  components : {
    Navbar,
  },
  data(){
    return {
      idx : 'title',
      title : "",
      category : "",
      tutor : "",
      image : "",
      description : "",
      date : "",
      start : "",
      end : "",
      size : "",
      cost : "",
      password :"",
    }
  },
  computed: {
    profile_image() {
      return this.$store.getters['userStore/getProfileIamge'] 
    },
  },
  methods : {
    shutDown(){
      const dropdownBtn = document.querySelector(".dropdown-btn");
      const dropdownListContainer = document.querySelector(".dropdown-list-container");
      dropdownListContainer.style.display="none"
      dropdownBtn.style.backgroundColor="transparent"
    },
    onClickBtn(content){
      const comps = document.getElementsByClassName("select-component")
      for(let i = 0 ; i < comps.length;i++){
        comps[i].style.borderRight="10px solid white";  
        comps[i].style.backgroundColor=" #FFEEF7";
        comps[i].style.color="black"
      }
      document.getElementById(content).style.borderRight="10px solid white" 
      document.getElementById(content).style.backgroundColor="#202D91"
      document.getElementById(content).style.color="white"
      this.idx =content
    },
    selectCategory(name){
      const cards = document.getElementsByClassName("category-card")
      for (let i  = 0; i< cards.length;i ++){cards[i].style.opacity = "1"; cards[i].style.transform="scale(1)";} 
      document.getElementById(name).style.opacity="0.5";
      // document.getElementById(name).style.transform="scale(0.9);"
      this.category=name;
      switch(name){
        case "excerciseCard":
          this.category = "1"
          break
        case "hobbyCard":
          this.category = "2"
          break
        case "langCard":
          this.category = "3"
          break
      }
    },
    uploadImage(){
      this.image = this.$refs.lectureImages.files
    },
    commaCost(){
      this.cost = this.cost.toLocaleString('ko-KR')
    },
    selectSize(onsize){
      const cards = document.getElementsByClassName("class-card")
      for (let i  = 0; i< cards.length;i ++){cards[i].style.opacity = "1";}
      document.getElementById(onsize).style.opacity="0.5";
      // document.getElementById(size).style.transform="scale(0.9);"
      switch(onsize){
        case "smallCard":
          this.size = "1"
          break
        case "mediumCard":
          this.size = "2"
          break
        case "largeCard":
          this.size = "3"
          break
      }
    },
    onSubmit(){

      var Form = new FormData();
      Form.append('name', this.$store.getters["userStore/getUsername"]);
      Form.append('title', this.title);
      Form.append('tutor', this.$store.getters["userStore/getUserId"]);
      if (this.image != "") {
        Form.append('main_image', this.image[0]);
      }
      else {
        Form.append('main_image', '');
      }
      Form.append('category', this.category);
      Form.append('start', this.start);
      Form.append('end', this.end);
      Form.append('date', this.date);
      Form.append('description', this.description);
      var max_num = null
      if (this.size == '1') {
        max_num = '5'
      }
      else if (this.size == '2') {
        max_num = '10'
      }
      else if (this.size == '3') {
        max_num = '20'
      }
      Form.append('room_size', max_num);
      Form.append('price', this.cost);
      Form.append('lecture_cnt', '1');
      Form.append('validation', '');
      Form.append('password', this.password);
      axios.post("https://k5c202.p.ssafy.io/od/onedays/register/", Form)
      .then(()=>{
        const Swal = require('sweetalert2')
          Swal.fire({
            text: '생성이 완료되었습니다.',
            showClass: {
              popup: 'animate__animated animate__fadeInDown'
            },
            hideClass: {
              popup: 'animate__animated animate__fadeOutUp'
            },
            confirmButtonText: 'To Main',
          }).then(() => {
            this.$router.push({ name: 'Main'})
          })
      })
      .catch((res)=>{
        console.log(res)
      })
    },
  }
}
</script>

<style scoped>
  .warp{
    position : relative;
    width: 100%;
    height: 100%;
  }
  main{
    width: 100%;
    padding: 2% 0;
    display: flex;
    justify-content: center;
    height: 80vh;
  }
  #ClassCreateForm{
    display: flex;
    position: relative;
    width: 90%;
    justify-content: center;
    /* background-color: black; */
  }
  #selectTable{
    width: 15%;
    height: 100%;
    /* background-color: #FFEEF7; */

    display: grid;
    grid-template-rows: 8;
    justify-items: flex-end;
    align-items: center;
    /* justify-content: center; */
    gap: 1%;
  }
  .select-component{
    width: 100%;
    background-color: #FFEEF7;
    height: 100%;
    cursor:pointer;
    display: flex;
    justify-content: center;
    align-items: center;
    border-right: 10px solid white;
    transition: 0.25s;
  }
  #selectedArea{
    width: 75%;
    background-color: #FFEEF7;
    
    display:flex;
    justify-content: center;

  }
  .section-content{
    padding: 2% 0;
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items:center;
    justify-content: space-between;
  }
  .section-header{
    /* top: 20%; */
    /* background-color: lightgray; */
    width: 90%;
    height: 15%;
    font:bolder;
    border-radius: 15px;
    display: flex;
    justify-content: space-around;
    align-items: center;
    opacity: 75;
  }
  .section-title{
    font-size: 1.5rem;
    font:bolder;
  }
  .section-container{
    display: flex;
    width: 80%;
    height: 60%;
    padding: 2%;
    justify-content: space-between;
    align-items: center;
    background-color: white;
    border-radius: 15px;
    opacity: 75;
    color: black;
    margin-bottom: 5%;
  }
  .section-container > label {
    width: 25%;
    font-size: 1.25rem;
    text-align: center;
  }
  .section-container > input{
    width: 65%;
    font-size: 1.25rem;
    font: bolder;
    border: none;
    /* border-radius: 15px; */
    margin: 0 5%;
    text-align: center;
    border-bottom: 1px solid gray;
  }
  .section-container > input:focus{
    outline: none;
  }
  .category-card{
    background-color: lightgray;
    width: 30%;
    border: 1px solid black;
    border-radius: 15px;
    height: 70%;
    /* aspect-ratio: 1; */
    margin: 0 1%;
    cursor:pointer;
    display: flex;
    border: none;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;

    transition: transform .25s;
  }
  #excerciseCard{
    background-color: #c9cbe0;
  }
  #hobbyCard{
    background-color: #ecd4d4;
  }
  #langCard{
    background-color: #ccdbe2;
  }
  #tutorContent{
    flex-direction: column;
    justify-content: space-around;
    align-items: center;
    /* height: 100%; */
  }

  .profile-image{
    background-color: azure;
    border-radius: 50%;
    width: 20%;
    aspect-ratio: 1;
  }
  .profile-content{
    /* background-color: aliceblue; */
    width: 50%;
    height: 25%;
    font-size: 1.25rem;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  .profile-content > span {
    background: linear-gradient(to top, #d1c9ff 50%, transparent 50%);
  }
  #lectureContent{
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
  }

  .content-row{
    width: 100%;
    height: 40%;
    display: flex;
    justify-content: space-around;
    align-items: center;
  }
  .content-row > label{
    width: 20%;
  }
  .content-row > input, textarea{
    width: 80%;
  }
  .content-row > textarea{
    height: 50%;
    border: 1px solid silver;
    border-radius: 5px;
    margin-right: 3%;
  }
  #timeSection{
    display: flex;
    flex-direction: column;
    justify-content: space-around;
  }
  .class-card{
    background-color: lightgray;
    width: 30%;
    border: none;
    border-radius: 15px;
    height: 70%;
    /* aspect-ratio: 1; */
    margin: 0 1%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-around;

    transition: transform .25s;
  }
  #smallCard{
    background-color: #d5e1df;
  }
  #mediumCard{
    background-color: #a3b6c5;
  }
  #largeCard{
    background-color: #e2b3a3;
  }
  .next-btn{
    /* border: 1px solid #8D3DA5; */
    border: none;
    border-radius: 15px;
    background-color: transparent;
    width: 20%;
    height: 40%;
    cursor:pointer;
  }
  .next-btn:hover{
    background-color: #8D3DA5;
    color:white;
  }
  #submitBtn{
    border: none;
    border-radius: 15px;
    background-color: #aa7ab8;
    width: 20%;
    height: 50%;
    cursor:pointer;
  }
  #submitBtn:hover{
    background-color: #202D91;
    color:white;
  }
</style>