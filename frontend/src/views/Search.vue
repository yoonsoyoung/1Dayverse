<template>
  <div class="warp">
    <Navbar/>
    <main @click="shutDown" >    
      <div v-if="loading">
        <h1>로딩 중..</h1>
      </div>
      <div v-else-if="lectures.length > 0">
        <h1>강의 목록</h1>
      </div>
      <div v-else>
        <h1>강의가 없습니다.</h1>
      </div>
      <div id="lectureContainer">
        <Lecture
          v-for="(lecture,idx) in lectures"
          :key="idx"
          :lecture="lecture"
        />
      </div>
    </main>
  </div>
</template>

<script>
import axios from 'axios'
import Navbar from '@/components/Navbar.vue'
import Lecture from '@/components/Lecture.vue'

export default {
  name : "Category",
  props : ["keyword"],
  components : {
    Navbar,
    Lecture,
  },
  data(){
    return {
      lectures : "",
      loading  : false,
      q : "",
    }
  },
  watch : {
    keyword : function() {
      return this.search();
    },
  },
  created(){
    console.log(this.keyword)
    this.loading = true;
    const Form = new FormData();
    Form.append("q", this.keyword)
    axios.post(`https://k5c202.p.ssafy.io/od/onedays/search/`, Form)
    .then((res)=>{
      this.lectures = res.data
      this.loading = false;
    })
  },
  methods : {
    shutDown(){
      const dropdownBtn = document.querySelector(".dropdown-btn");
      const dropdownListContainer = document.querySelector(".dropdown-list-container");
      dropdownListContainer.style.display="none"
      dropdownBtn.style.backgroundColor="transparent"
    },
    search(){
      this.loading = true;
      const Form = new FormData();
      Form.append("q", this.keyword)
      axios.post(`https://k5c202.p.ssafy.io/od/onedays/search/`, Form)
      .then((res)=>{
        this.lectures = res.data
        console.log(res)
        this.loading = false;
      })
    },
   },

}
</script>

<style scoped>
.warp{
  position: relative;
  width: 100%;
  height: 100%;
}
main{
  text-align: center;
  height: 100vh;
}
#lectureContainer{
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  justify-content:center;
  align-items: flex-start;
  justify-items: center;
  gap: 2.5%;
  padding: 0 5%;
  height: 100%;
  margin-top: 5%;
}
</style>