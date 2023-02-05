<template>
  <div class="warp">
    <Navbar/>
    <main @click="shutDown">
      <div class="left">
        <div class="image-container">
          <i class></i>
          <img :src="`@/../../../backend${this.lecture.main_image}`" alt="" id="classImage">
        </div>
      </div>
      <div class="right">
        <div class="class-info">
          <div v-if="lecture.category===1">[운동]</div>
          <div v-else-if="lecture.category===2">[예술]</div>
          <div v-else-if="lecture.category===3">[언어]</div>
          <div style="font-size: 2.5rem"><b>{{lecture.title}}</b></div>
          <div>강사 : {{lecture.name}}</div>
          <div>모집 인원 : {{lecture.lecture_cnt}} / {{lecture.room_size}}명</div>
          <div>가격 : {{cost}}원</div>
          <div v-if="!err_message && teachable != 1">
            <button @click="Pay" id="payBtn">
              <img :src="PayImage" style="width: 5rem; margin-top: 1rem;" alt="">
            </button>
          </div>
          <div v-else>{{ err_message }}</div>
        </div>
      </div>
    </main>
    <div style=" text-align: center; margin-top: 5rem;">[상세 설명]</div>
    <hr style="width: 73%; max-width: 1000px"/>
    <div v-html="description" class="lecture-explain"></div>
  </div>  
</template>

<script>
import axios from 'axios'
import Navbar from '@/components/Navbar.vue'
import image from '@/assets/drawing_image.jpg'
import PayImage from '@/assets/payment_icon_yellow_medium.png'

export default {
  name : "ClassDetail",
  props : ["lecture_id"],
  components : {
    Navbar,
  },
  data(){
    return {
      PayImage : PayImage,
      lecture : "",
      cost : "",
      image : image,
      registerable: null,
    }
  },
  created(){
    axios.get(`https://k5c202.p.ssafy.io/od/onedays/lecture/detail/${this.lecture_id}`)
    .then((res)=>{
      this.lecture = res.data.detail
      this.cost = res.data.detail.price.toLocaleString('ko-KR')
    })
    const user_id = this.$store.getters["userStore/getUserId"]
    axios.get(`https://k5c202.p.ssafy.io/od/onedays/check/${user_id}/${this.lecture_id}`)
    .then((res) => {
      this.registerable = res.data
      console.log(this.lecture.tutor)
      
      }
    )
    .catch((err) => console.log(err))
  },
  computed : {
    
    teachable() {
      return this.$store.getters['userStore/getUserTeachable']
    },
    err_message() {
      if (this.registerable == false) {
        return '이미 등록한 강좌입니다.'
      }
      else if (Number(this.lecture.lecture_cnt) >= Number(this.lecture.room_size)) {
        return '이미 마감된 강좌입니다.'
      }
      return null
    },
    description() {
      if(this.lecture.description===""){
        return "";
      }
      return this.lecture.description.replace(/(?:\r\n|\r|\n)/g, '<br />')

    },
  },

  methods : {
    shutDown(){
      const dropdownBtn = document.querySelector(".dropdown-btn");
      const dropdownListContainer = document.querySelector(".dropdown-list-container");
      dropdownListContainer.style.display="none"
      dropdownBtn.style.backgroundColor="transparent"
    },
    Pay(){
      const Form = new FormData();
      Form.append("lecture_price", this.lecture.price)
      Form.append("lecture_title", this.lecture.title)
      Form.append("lecture_id", this.lecture.id)
      Form.append("user_id", this.$store.getters["userStore/getUsername"])
      axios.post("https://k5c202.p.ssafy.io/od/payments/ready/", Form)
      .then((res)=>{
        location.href = res.data
      })
    }
  }
}
</script>

<style scoped>
.warp{
  position: relative;
  width: 100%;
  height: 100%;
}
main{
  display: flex;
  width: 70%;
  max-width: 1000px;
  margin: 0 auto;
  margin-top: 5vh;
}
.left{
  width: 50%;
  /* justify-content: center; */
}
.image-container {
  /* width: 50%; */
  width: auto;
  box-sizing: border-box;
  margin-bottom: 5%;

}

#classImage{
  /* width: 100%;/ */
  width: 90%;
}
.right{
  width: 50%;
  /* justify-content: center; */
}
.class-info{
  width: 100%;
  height: 40%;
  font-size: 1.3rem;
  margin-bottom: 5%;
}
.class-info > div {
  margin-bottom: 1rem;
}
#payBtn{
  background-color: transparent;
  border: none;
  cursor: pointer;
}
.lecture-explain {
  text-align: center;
  line-height: 1.7rem;
  width: 70%;
  max-width: 1000px;
  margin: 0 auto;
}
div {
  text-align: start;
}

</style>