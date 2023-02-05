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
  props : ["group"],
  components : {
    Navbar,
    Lecture,
  },
  data(){
    return {
      lectures : "",
      loading  : false,
    }
  },
  watch : {
    group : function() {
      return this.groupChange();
    }
  },
  
  mounted(){
    this.loading = true;
    const BASE_URL = 'https://k5c202.p.ssafy.io/od/onedays/'
    switch(this.group){
      case "health":
        axios.get(BASE_URL+"category/1")
        .then((res)=>{
          this.lectures = res.data
          this.loading = false;
        })
        break
      case "hobby":
        axios.get(BASE_URL+"category/2")
        .then((res)=>{
          this.lectures = res.data; 
          this.loading = false;
        })
        break
      case "lang":
        axios.get(BASE_URL+"category/3")
        .then((res)=>{
          this.lectures = res.data;
          this.loading = false; 
        })
        break
    }
  },
  methods : {
    shutDown(){
      const dropdownBtn = document.querySelector(".dropdown-btn");
      const dropdownListContainer = document.querySelector(".dropdown-list-container");
      dropdownListContainer.style.display="none"
      dropdownBtn.style.backgroundColor="transparent"
    },
    groupChange(){
      const BASE_URL = 'https://k5c202.p.ssafy.io/od/onedays/'
      switch(this.group){
        case "health":
          axios.get(BASE_URL+"category/1")
          .then((res)=>{
            this.lectures = res.data; 
            this.loading = false;
          })
          .catch((err) => {console.log(err)})
          break
        case "hobby":
          axios.get(BASE_URL+"/category/2")
          .then((res)=>{
            this.lectures = res.data; 
            this.loading = false;
          })
          break
        case "lang":
          axios.get(BASE_URL+"category/3")
          .then((res)=>{ 
            this.lectures = res.data; 
            this.loading = false;
          })
          break
      }
    },

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
  text-align: center;
  height: 90vh;
  overflow: auto;
}
#lectureContainer{
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  justify-content:center;
  align-items: flex-start;
  justify-items: center;
  gap: 2.5%;
  padding: 0 5%;
}
h1 {
  margin: 2rem 0;
}
</style>