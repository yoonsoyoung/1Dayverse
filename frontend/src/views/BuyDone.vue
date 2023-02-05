<template>
  <div class="warp">
    <Navbar/>
    <main @click="shutDown">
      <div id="buyCompleteForm">
        <div id="buyCompleteFormHeader">
          <h1 style="margin: 0;">결제가 완료되었습니다.</h1>
        </div>
        <div class="lecture-buy-form">
          <span class="lecture-title-buy-form">{{lecture.title}}</span>
          <div class="lecture-image-container"><img :src="require('@/assets/class1.jpeg')" alt=""></div>
          <span>강사 : {{lecture.name}}</span>
          <div>
            <span>{{lecture.date}} {{lecture.start}}</span>
          </div>
        </div>
        <div id="buyCompleteFormBottom">
          <div>
            <button @click="goToMyPage">입장 비밀번호 확인하러 가기</button>
          </div>
          <div>
            <button @click="goToUnity">유니티로 입장하기</button>
          </div>
          <div>
            <button @click="goToMainPage">메인 화면 으로 가기</button>
          </div>
        </div>
      </div>
    </main>
    <Footer/>
  </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue'
import axios from 'axios'
import Footer from '@/components/Footer.vue'


export default {
  name : "BuyDone",
  props : ["lecture_id"],
  components : {
    Navbar,
    Footer,
  },
  data(){
    return {
      lecture : {}
    }
  },
  created(){
    axios.get(`https://k5c202.p.ssafy.io/od/onedays/lecture/detail/${this.lecture_id}`)
    .then((res)=>{this.lecture= res.data})
    const Form = new FormData();
    Form.append("tutee", this.$store.getters["userStore/getUserId"])
    Form.append("lecture",this.lecture_id)
    axios.post(`https://k5c202.p.ssafy.io/od/payments/enroll/`, Form)
    .catch((err)=>{
      console.log(err )
    })
  },
  mounted(){  
  },
  methods : {
    shutDown(){
      const dropdownBtn = document.querySelector(".dropdown-btn");
      const dropdownListContainer = document.querySelector(".dropdown-list-container");
      dropdownListContainer.style.display="none"
      dropdownBtn.style.backgroundColor="transparent"
    },
    goToMyPage(){
      this.$router.push({name : "Profile", params : {"username" : this.$store.getters["userStore/getUsername"]}})
    },
    goToUnity(){
      alert('메타버스로 입장하기!')
    },
    goToMainPage(){
      this.$router.push({name : "Main"})
    },
  }
}
</script>

<style scoped>
  .warp{
    position: relative;
    width: 100%;
    height: 100vh;
  }
  main {
    padding: 0 5%;
    height: 100%;
  }
  #buyCompleteForm{
    display: block;
    background-color: #FFEEF7;
    width: 100%;
    margin:  5% 0;
    padding: 5% 0;
  }

  .lecture-buy-form{
    margin: 2% 0;
    background-color: white;

  }
  .lecture-title-buy-form{
    font-size: 2rem;
  }
  
</style>