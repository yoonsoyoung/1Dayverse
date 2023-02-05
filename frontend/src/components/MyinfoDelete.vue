<template>
  <div id="MyinfoCancelRoot">
      <page-title pageTitle="회원탈퇴"/>
      <div class="content-box mt-5">
        <div v-if="!this.cancelYes" class="cancel-confirm-box mt-5">
          <div class="text-box mt-4">
            <p class="title-text"><b>{{username}} </b>님 우리의 추억이 사라져요😥</p>
            <p class="body-text">정말 저희 1Dayverse를 졸업하실건가요?</p>
          </div>
          <div class="img-box">
            <img src="../assets/lonely.jpg" alt="lonely">
          </div>
          <div class="button-group mt-3">
            <button class="cancel-btn" @click="cancelBtn">취소</button>
            <button class="delete-btn" @click="deleteBtn">탈퇴</button>
          </div>
        </div>
        <div v-if="this.cancelYes" class="ok-bye-box mt-5">
          <div class="text-box mt-4 mb-5">
            <p class="title-text"><b>언제든 1Dayverse로 향하는 버스는 운행합니다 :)</b></p>
            <span class="body-text">일상이 지겨울 때, 취미가 필요할 때, 온라인이더라도 함께 하고 싶을 때 1Dayverse</span><i class="far fa-paper-plane"></i>
          </div>
          <div class="img-box mt-5">
            <img src="../assets/bus.png" alt="bus">
          </div>
          <div class="text-box mt-5">
            <p class="info-text">{{this.timeCnt}}초 후 자동으로 이동됩니다.</p>
          </div>
        </div>
      </div>
  </div>
</template>

<script>
export default {
  name: 'MyinfoCancel',
  components: {
  },
  created() {
    this.getUser()
  },
  computed: {
    username() {
      return this.$store.getters['userStore/getUsername'] 
    },
  },
  methods: {
    getUser() {
      this.$store.dispatch('userStore/FETCH_PROFILE', this.profilename)
    },
    cancelBtn() {
      this.$router.replace({name:"Profile", params: {username : this.username}})
    },
    deleteBtn() {
      const Swal = require('sweetalert2')
      Swal.fire({
        text: '정말로 삭제하시겠습니까?',
        confirmButtonText: 'Delete',
        showCancelButton: true,
        confirmButtonColor: '#8D3DA5',
      }).then((result) => {
        if (result.isConfirmed) {
          this.$store.dispatch('userStore/DELETE_USER', this.username)
          this.cancelYes = true;
          Swal.fire({
              title: '삭제되셨습니다.',
              showClass: {
                popup: 'animate__animated animate__fadeInDown'
              },
              hideClass: {
                popup: 'animate__animated animate__fadeOutUp'
              }
            }).then(() => {
              this.countDownTimer();
              setTimeout(() => {
                this.$router.push({ name: 'Main'})
              }, 4000);
            })
        } 
      })
    },
    countDownTimer() {
      if(this.timeCnt > 0) {
        setTimeout(() => {
          this.timeCnt = this.timeCnt-1;
          this.countDownTimer();
        }, 1000);
      }
    },
  },
  data() {
    return {
      user: {
        userid: String,
        nickname: String,
      },
      cancelYes: false,
      timeCnt: 3,
      profilename: this.$route.params.username,
      error_message: false,
    }
  },
}
</script>

<style lang="scss" scoped>
@import "../styles/MyinfoDelete.scss";

</style>
