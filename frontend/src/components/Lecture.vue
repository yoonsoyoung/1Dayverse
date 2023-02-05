<template>
  <div class="lecture-container" @click="goToLectureDetail"> 
    <div class="image-container">
      <img :src="`@/../../../backend${lecture.main_image}`" class="image" alt="">
    </div>
    <div class="flex">
      <b>{{lecture.name}}</b>
    </div>
    <div class="flex">
      {{lecture.title}}
    </div>
  </div>
</template>
<script>
export default {
  name : "Lecture",
  props : ["lecture"],
  data(){
    return  {
      // lecture : lecture,
    }
  },
  methods : {
    goToLectureDetail(){
      this.$router.push({name :"LectureDetail", params : {"lecture_id" : this.lecture.id} })
    },
  },
}
</script>

<style scoped>
.lecture-container{
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  box-sizing: border-box;
}
.image-container{
  width: 300px;
  height: 200px;
}
.image{
  width: 100%;
  height: 100%;
}
.flex{
  width: 100%;
  display: flex;
  margin: 2.5% 0;
  text-align: start;
}

</style>