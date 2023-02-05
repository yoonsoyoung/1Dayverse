<template>
  <div class="warp">
    <Navbar/>
    <main>
      <h1>구매가 실패하였습니다.</h1>
    </main>
  </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue'

export default {
  name : "BuyFail",
  components : {
    Navbar,
  },
  data(){
    return {

    }
  },
}
</script>

<style scoped>

</style>