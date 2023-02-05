<template>
    <nav>
    <div style="display:flex; align-items:center; width:50%; justify-content:space-around; padding:0 2.5%; max-height: 9vh;">
      <img :src="logo" alt="" id="logoImage" @click="goToMainPage">
      <div style="display:flex; justify-content:center; height: 100%" id="dropdown">
        <button @click="showMenu" class='dropdown-btn'>전체 카테고리 <i class="fas fa-caret-down" style="background-color:none; color:#EEAEF9;"></i></button>
        <div class='dropdown-list-container' style="display:none;">
          <a href="#" @click="goToHealthCategory">운동</a>
          <a href="#" @click="goToHobbyCategory">예술</a>
          <a href="#" @click="goToLanguageCategory">언어</a>
          <!-- <a href="#" @click="goToETCCategory">기타</a> -->
        </div>
      </div>
      <div class="btn" @click="makeClass"><span v-show="isTeacher">강의 생성</span></div>
    </div>
   <div style="display:flex; align-items:center; width:50%; justify-content:space-around; padding:0 2.5%;">
      <div class="input-box">
        <input id="inputTag" type="text" v-model="keyword" placeholder="강의 이름으로 검색"  @keyup.enter="search">
        <i class="search-icon fas fa-search"></i>
      </div>
      <div class="btn"> 
        <span v-if="isLogin" @click="goToMyPage">내 정보</span>
        <span v-else @click="goToSignupPage">회원가입</span>
      </div>
      <div class="btn">
        <span v-if="isLogin" @click="logout">로그 아웃</span>
        <span v-else @click="login">로그인</span>
      </div>
    </div>
  </nav>
</template>

<script>

import logo from '@/assets/1DAYVERSE.png'
// import {mapGetters} from 'vuex';
// 
export default {
  name : "Navbar",
  data (){
    return {
      logo : logo,
      keyword : "",
    }
  },
  computed: {
    username() {
      return this.$store.getters["userStore/getUsername"]
    },
    
    isLogin() {
      const tmp = this.$store.getters["userStore/getUsername"]
      if (tmp) {
        return true
      }
      else {
        return false
      }
    },
    isTeacher() {
      if (this.$store.getters["userStore/getUserTeachable"] == '1') {
        return true
      }
      return false
    }
  },
  methods : {
    shutDown(){
      const dropdownBtn = document.querySelector(".dropdown-btn");
      const dropdownListContainer = document.querySelector(".dropdown-list-container");
      dropdownListContainer.style.display="none"
      dropdownBtn.style.backgroundColor="transparent"
    },
    showMenu(){
      const dropdownBtn = document.querySelector(".dropdown-btn");
      const dropdownListContainer = document.querySelector(".dropdown-list-container");
      if (dropdownListContainer.style.display=="none"){
        dropdownListContainer.style.display="block";
        dropdownBtn.style.backgroundColor="#DBBFFF"
      }else{
        dropdownListContainer.style.display="none"
        dropdownBtn.style.backgroundColor="transparent"
      }
    },
    search(){
      if(this.keyword !==""){
      this.$router.push({name : "Search", params: {"keyword" : this.keyword}})
      }
      else{
        const Swal = require('sweetalert2')
        Swal.fire({
          text: '검색어를 입력해주세요.',
        })
      }
    },
    makeClass(){
      this.$router.push({name : "LectureCreate"})
    },
    goToMyPage(){
      const Swal = require('sweetalert2')
      let timerInterval
      Swal.fire({
        html: '내 정보 페이지로 이동',
        timer: 700,
        didOpen: () => {
          Swal.showLoading()
        },
        willClose: () => {
          clearInterval(timerInterval)
        }
      }).then(() => {
        if (this.username) {
          this.$router.push({name:"Profile", params: {username : this.username}})
        }
      })
    },
    goToSignupPage(){
      this.$router.push({name : "Signup"})
    },
    logout(){
      const Swal = require('sweetalert2')
      Swal.fire({
        text: '로그아웃 하시겠습니까?',
        confirmButtonText: 'Logout',
        showCancelButton: true,
        confirmButtonColor: '#8D3DA5',
      }).then((result) => {
        if (result.isConfirmed) {
          this.$store.dispatch('userStore/LOGOUT')
          if (this.$route.name != 'Main') {
            this.$router.push({ name: 'Main' })
          }
          else {
            this.$router.go()
          }
        } 
      })
    },
    login(){
      this.$router.push({name : "Login"})

    },
    goToMainPage(){
      this.$router.push({name : "Main"})

    },
    goToHealthCategory(){
      this.$router.push({name : "Category", params: {group : "health"}})
      this.shutDown();
    },
    goToHobbyCategory(){
      this.$router.push({name : "Category", params: {group : "hobby"}})
      this.shutDown();

    },
    goToLanguageCategory(){
      this.$router.push({name : "Category", params: {group : "lang"}})
      this.shutDown();

    }
  }

}
</script>

<style scoped>
nav{
  display: flex;
  justify-content: space-around;
  align-items: center;
  justify-items: center;
  width: 100%;
  background-color: #FFEEF7;
  height: 10vh;
  /* margin-bottom: 5%; */
}
#logoImage{
  max-height: 9vh;
  cursor: pointer;
}
#dropdown{
  width: 30%;
}

.dropdown-btn{
  border: none;
  font-size: 1.1rem;
  background-color:transparent;
  text-align: center;
  cursor:pointer;
  border-radius: 3px;
}
.down-caret {
  width: 0;
  height: 0;
  border-style: solid;
  border-width: 5px 5px 0 5px;
  border-color: #ffffff transparent transparent transparent;
  display: inline-block;
  margin-left: 6px;
  top: -3px;
  position: relative;
  transform: rotate(0deg);
  transition: all .25s ease-in;
}
.dropdown-list-container{
  position: absolute;
  display: none;
  background-color:#ffffff;
  margin-top:2rem;
  transition: all .25s linear;
  border-radius: 5px;
  z-index: 10;
}
.dropdown-list-container a {
  font-size: 1.1rem;
  line-height: 40px;
  text-decoration: none;
  color:black;
  display: block;
  width: 200px;
  line-height: 75px;
  /* border-bottom: 1px solid black; */
  text-align: center;
  border-radius: 5px;

}
.dropdown-list-container a:hover{
  background-color: #DBBFFF;
}
#inputTag{
  width: 70%;
  font-size: 1.0rem;
  background-color: none;
  border:none;
  /* border-radius: 5px; */
  text-align: center;
  line-height: 30px;
  padding-right: 5px;
}
#inputTag:focus{
  border:none;
  outline: none;
}
.input-box {
  width: 50%;
  background-color: white;
  border:none;
  border-radius: 50px;
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  align-content: center;
  align-items: center;
  justify-content: space-evenly;
}
.search-icon {
  color: lightgray;
}
.btn {
  width: 20%;
  font-size: 1.1rem;
  cursor:pointer;

}
</style>
