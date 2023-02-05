<template>
<Nav />
  <div id="LiveRoot">
    <div class="wrap">
      <web-cam :location=location />
    </div>
  </div>
</template>

<script>
import WebCam from '@/components/WebRtc/WebCam';
import Nav from '@/components/Navbar.vue'
export default {
  name : 'Live',
  components: {
    WebCam,
    Nav,
  },
  data() {
    return {
      location: 'lecture',
    }
  },

}
</script>
<style lang="scss" scoped>
@import "../styles/LiveLecture.scss";
</style>