<template>
  <div id="unity" :class="{'mini-map':!showMap}">
    <Navbar />
    <loading v-if="activeLoad" v-on:switchLoad="switchLoad" />
    <iframe src="unity2/index.html" class="unity-box" ></iframe>
  </div>
</template>

<script>
import Navbar from "@/components/Navbar.vue";
import Loading from '@/components/Loading.vue';

// const sendMessage = (object, method, param) => {
//   window.gameInstance.SendMessage(object, method, param);
// };

export default {
  name: "Study",
  components: {
    Navbar,
    Loading,
  },
  computed: {
    showMap: function() {
      if(this.$route.name === 'Study')
        return true;
      else 
        return false;
    },
  },
  methods: {
    goLive() {
      window.open("/live")
    },
    switchLoad() {
      this.activeLoad = false;
    },
  },
  data() {
    return {
      activeLoad: true,
    }
  },
};
</script>

<style scoped>
.unity-box {
  border: none;
  background: none;
  height: 90vh;
  width: 100%;
  display: flex;
  justify-content: center;
  margin-top: 3vh;
}
.mini-map {
  position: fixed;
  right: 5px;
  bottom: 75px;
  width: 70px;
  height: 100px;
  z-index: 95;
  position: absolute;
  background: rgba(255,255,255,0.5);
}
.go-live-box {
  position: absolute;
  top: 100px;
  right: 0;
}
</style>
