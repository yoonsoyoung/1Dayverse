<template>
  <div id="LoadingRoot">
    <div class="content-box">
      <div class="gif-box">
        <img src="../assets/bus_gif.gif" alt="Loading...">
      </div>
      <div class="text-box">
        <p>{{this.timeCnt}}초 후 원데이버스 세계에 도착합니다.</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  created() {
    this.switchLoad();
  },
  methods: {
    switchLoad() {
        this.countDownTimer();
        setTimeout(() => {
          this.$emit('switchLoad');
        }, 3500);
    },
    countDownTimer() {
      if(this.timeCnt > 0) {
        setTimeout(() => {
          this.timeCnt = this.timeCnt-1;
          this.countDownTimer();
        }, 1000);
      }

    }
  },
  data() {
    return {
      timeCnt: 3,
    }
  },
  
}
</script>

<style lang="scss" scoped>
@import "../styles/Loading.scss";

</style>