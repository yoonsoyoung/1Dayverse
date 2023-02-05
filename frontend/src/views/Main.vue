<template>
  <div>
    <Navbar/>
    <main @click="shutDown">
      <section class="first-box">
        <div class="content-text flex-column">
          <span class="title">배움이 있는 메타버스의 세계</span>
          <span class="explain">원데이버스는 기존 원데이 클래스에</span>
          <span class="explain">메타버스 환경을 더해</span>
          <span class="explain">현장감 있는 비대면 수업 환경을 제공하는 서비스입니다.</span>
          <button  id="goToUnityBtn" @click="goToUnity">유니티로 입장하기</button>
        </div>
        <div class="empty-box"></div>
        <div class="content-image">
          <img :src="require('@/assets/image0.jpg')" alt="" class="image">
        </div>
      </section>
      <section>
        <div class="content-image">
          <img :src="require('@/assets/explain1.png')" alt="" class="image">
        </div>
        <div class="empty-box"></div>
        <div class="content-text">
          <span class="title">원데이 클래스에 참여해보세요.</span>
          <span class="explain">원데이버스에서는</span>
          <span class="explain">웹사이트를 통해</span>
          <span class="explain">간단하게 강좌 개설과 수강 신청이 가능합니다.</span>
        </div>
      </section>
      <section>
        <div class="content-text">
          <span class="title">1dayverse는 <br/>자신만의 캐릭터를<br/> 생성 할 수 있습니다.</span>
          <span class="explain">메타버스 환경 내에서</span>
          <span class="explain">자신만의 캐릭터를 생성하고</span>
          <span class="explain">자유롭게 이동하며</span>
          <span class="explain">강좌 수강과 여가 활동이 가능합니다.</span>
        </div>
        <div class="empty-box"></div>
        <div class="content-image">
          <img :src="require('@/assets/image1.webp')" alt="" class="image">
        </div>
      </section>
    </main>
    <Footer/>
  </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue'
import Footer from '@/components/Footer.vue'

export default {
  name : "Main",
  components : {
    Navbar,
    Footer,
  },
  methods : {
    shutDown(){
      const dropdownBtn = document.querySelector(".dropdown-btn");
      const dropdownListContainer = document.querySelector(".dropdown-list-container");
      dropdownListContainer.style.display="none"
      dropdownBtn.style.backgroundColor="transparent"
    },
    goToUnity(){
      if (this.$store.getters['userStore/getUsername']  === null) {
        this.$router.push({name : "Login"})
      }
      else {
        this.$router.push({name : "CamSetting" })
      }
    },
  }
}
</script>

<style>
@font-face {
    font-family: 'GmarketSansMedium';
    src: url('https://cdn.jsdelivr.net/gh/projectnoonnu/noonfonts_2001@1.1/GmarketSansMedium.woff') format('woff');
    font-weight: normal;
    font-style: normal;
}

* {
    font-family: 'GmarketSansMedium';
}
.main-pink{
  background-color: #C86B98;
}
.main-purple{
  background-color: #8D3DA5;
}
.main-blue{
  background-color: #202D91;
}

</style>

<style scoped>
*, *:before, *:after {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}
main{
  width:100%;
  height: 100%;
  overflow: hidden;
  position: relative;
  /* height: 100vh; */
}
section {
  display: flex;
  padding: 0 10%;
  align-items: center;
  height: 100vh;
}
.first-box {
  height: 90vh;
}
.flex-column{
  display: flex;
  flex-direction: column;
}
#goToUnityBtn{
  margin-top: 5%;
  width: 30%;
  line-height: 65px;
  border:none;
  cursor: pointer;
  border-radius: 15px;
  color: white;
  font-weight: bold;
  background-color: #C86B98;
}
#goToUnityBtn:hover{
  background-color: #202D91;
  color: white;
}
/* section:nth-child(1){
  background-color: rgb(255, 205, 243);
}
section:nth-child(2){
  background-color: rgb(244, 183, 237);
}
section:nth-child(3){
  background-color: rgb(223, 186, 255);
} */
.content-text{
  width: 45%;
  font-size: 1.5rem;
  text-align: start;
  margin-left: 0;
  padding-left: 0;
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.content-image{
  width: 45%;
}
.image{
  height: 50vh;
  width: 100%;
}
.title {
  font-weight: bold;
  margin-bottom: 3rem;
  font-size: 2.3rem;
}
.explain {
  font-size: 1.3rem;
}
.empty-box {
  width: 10%;
}
</style>