<template>
  <div>
    <Navbar/>
    <div class="id-card" v-if="profilename == username">
      <div class="job">
        <h3 v-if="teachable === 1" class="job-text">TUTOR ID</h3>
        <h3 v-else class="job-text">TUTEE ID</h3>
      </div>
      <div class="container grid grid-cols-1 md:grid-cols-6">
        <div class="profile">
          <img :src="`@/../../../backend/media/${this.profile_image}`" class="user-image" alt="">
          <div class="profile-info">
            <div class="user-info">
              <span>{{ username }}</span>
              <svg @click="changeuserinfo('name')" xmlns="http://www.w3.org/2000/svg" width="1.3rem" height="1.3rem" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
              </svg>
            </div>
            <div class="user-info">
              <span>{{ birth }}</span>
              <svg @click="changeuserinfo('birth')" xmlns="http://www.w3.org/2000/svg" width="1.3rem" height="1.3rem" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
              </svg>
            </div>
            <div class="user-info">
              <span>{{ phone }}</span>
              <svg @click="changeuserinfo('phone')" xmlns="http://www.w3.org/2000/svg" width="1.3rem" height="1.3rem" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
              </svg>
            </div>
            <button class="delete" @click="changeDelete">
              <span>회원탈퇴</span>
            </button>
          </div>
        </div>
        <div class="class-info">
          <h2 v-if="teachable === 1">강의정보</h2>
          <h2 v-else>수강내역</h2>
          <div v-if="!lectures || lectures.length === 0">아직 강의가 없어요😢</div>
          <div v-else>
            <table border="1" class="lecture" v-if="teachable === 1">
              <th>Title</th>
              <th>Description</th>
              <th>password</th>
              <th>validation</th>
              <tr v-for="lecture, idx in lectures" :key="idx">
                <td>{{ lecture.title }}</td>
                <td>{{ lecture.description }}</td>
                <td>{{ lecture.password }}</td>
                <td>{{ lecture.validation }}</td>
              </tr>
            </table>
            <table border="1" class="lecture" v-else>
              <th>Title</th>
              <th>Description</th>
              <th>password</th>
              <tr v-for="lecture, idx in lectures" :key="idx">
                <td>{{ lecture.title }}</td>
                <td>{{ lecture.description }}</td>
                <td>{{ lecture.password }}</td>
              </tr>
            </table>
          </div>
        </div>
      </div>
    </div>
    <div class="ease_in_out under">
      <img :src="require('@/assets/bus.png')" class="bus" alt="" />
    </div>
  </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue'

export default {
  name: "Profile",
  data() {
    return {
      profilename: this.$route.params.username,
      error_message: false,
    }
  },
  components: {
    Navbar,
  },
  created() {
    this.$store.dispatch('userStore/FETCH_PROFILE', this.profilename)
  },
  computed: {
    teachable() {
      return this.$store.getters['userStore/getUserTeachable']
    },
    username() {
      return this.$store.getters['userStore/getUsername'] 
    },
    birth() {
      return  this.$store.getters['userStore/getUserBirth'] 
    },
    phone() {
      return this.$store.getters['userStore/getUserPhone'] 
    },
    lectures() {
      return this.$store.getters['userStore/getUserLectures'] 
    },
    profile_image() {
      return this.$store.getters['userStore/getProfileIamge'] 
    },
  },
  methods: {
    phone_error(info) {
      if (info === "") {
        this.error_message = '번호를 확인해주세요'
      }
      var regExp = /^01([0|1|6|7|8|9]?)-?([0-9]{3,4})-?([0-9]{4})$/
      if (info.match(regExp) === null) {
        this.error_message = '형식을 확인해주세요'
      }
      else if (info != "") {
        this.error_message = false
      }
    },
    birth_error(info) {
      if (info === "") {
        return this.error_message = '생일을 입력해주세요'
      }
      var regExp = /^(19[0-9][0-9]|20\d{2}).(0[0-9]|1[0-2]).(0[1-9]|[1-2][0-9]|3[0-1])$/
      if (info.match(regExp) === null) {
        this.error_message = '형식을 확인해주세요'
      }
      else if (info != "") {
        this.error_message = false
      }
    },
    toMain() {
      this.$router.push({ name: 'Main'})
    },
    refresh(info) {
      this.$router.push({name:"Profile", params: {username : info}})
    },
    changeDelete() {
      this.$router.push('/bye');
    },
    changeuserinfo(info) {
      var explain = ""
      if (info === "birth") {
        explain = "ex) 2000.01.01"
      }
      else if (info === "phone") {
        explain = "ex) 01012345678"
      }
      const Swal = require('sweetalert2')
      Swal.fire({
      text: `Change your ${info} ${explain}`,
      input: 'text',
      inputAttributes: {
        autocapitalize: 'off'
      },
      showCancelButton: true,
      confirmButtonText: 'Update',
      showLoaderOnConfirm: true,
      preConfirm: (new_info) => {
        console.log(this.error_message)
        const form = new FormData()
        var newuser = this.username
        if (info === "name") {
          newuser = new_info
          localStorage.setItem('username', new_info)
          form.append('username', new_info)
          form.append('birth_day', this.birth)
          form.append('phone_number', this.phone)
          form.append('character', '')
        }
        else if (info === "birth") {
          form.append('username', this.username)
          form.append('birth_day', new_info)
          form.append('phone_number', this.phone)
          form.append('character', '')
          this.birth_error(new_info)
        }
        else if (info === "phone") {
          form.append('username', this.username)
          form.append('birth_day', this.birth)
          form.append('phone_number', new_info)
          form.append('character', '')
          this.phone_error(new_info)
        }
        if (!this.error_message) {
          const form_info = [form, this.profilename]     
          this.$store.dispatch('userStore/UPDATE_PROFILE', form_info)
            .then(() => {
              Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: '수정되셨습니다',
                showConfirmButton: false,
                timer: 1000,
              })
              this.refresh(newuser)
              }
            )
            .catch(() => {
              if (info === 'name') {
                this.error_message = "중복된 닉네임입니다"
              }
              Swal.showValidationMessage(
                `Request failed: ${this.error_message}`
              )
              this.error_message = false
            })
        }
        else {
          Swal.showValidationMessage(
            `Request failed: ${this.error_message}`
          )
        }
      },
      allowOutsideClick: () => !Swal.isLoading()
      }).then((result) => {
        if (result.isConfirmed) {
          Swal.fire({})
        }
      })
    },
  }
}
</script>

<style scoped>
.box
{
  display: flex;
  align-items: center;
  height: 100vh;
}
.id-card {
  height: 70vh;
  position: relative;
  background-color: white;
  width: 70%;
  max-width: 1500px;
  margin: 3rem auto;
  box-shadow: 1px 1px 20px 2px rgba(0,0,0,.3);
  border-radius: 20px;
  display: flex;
}
.container {
  width: 100%;
  position: absolute;
  top: 10vh;
  display: flex;
  text-align: center;
}
.job{
  width: 100%;
  background-color: #8D3DA5;
  height: 13vh;
  position: absolute;
  border-radius: 20px 20px 0 0;
  border-top: 5px solid rgba(153, 97, 160, 0.9);
  text-align: right;
}
.job-text {
  margin-right: 2rem;
  margin-top: 5vh;
  color: white;
  font-size: 3vh;
  font-weight: bold;
  text-align: right;
}
.profile {
  margin: 0 3rem;
  height: 51vh;
  font-weight: bold;
  font-size: 1.2rem;
}
.user-image {
  width: 20vw;
  height: 20vw;
  max-width: 200px;
  max-height: 200px;
  box-shadow: 1px 3px 10px 3px rgba(158, 158, 158, 0.3);
  margin: 1rem;
  border-radius: 100%;
}
.class-info {
  margin: 5vh 3rem 0 0;
  width: 100%;
  background-color: #d6c3db;
  padding: 1rem;
  border-radius: 10px;
  height: 50vh;
  overflow: auto;
}
.lecture {
  margin: 1rem 0;
  width: 100%;
  border-color: transparent !important;
  table-layout: fixed;
}
button {
  background-color: transparent !important;
  border: none;
  cursor: pointer;
}
.profile-info
{
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}
.profile-info > h4, svg
{
  margin: 0.5rem 0;
}
.to-main-box
{
  position: absolute;
  top: 45vh;
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
}
.to-main-btn {
  width: 30%;
  max-width: 100px;
  height: 30px;
  margin: 0.5rem 12px;
  margin-bottom: 16px;
  border-radius: 30px;
  border: none;
  font-weight: bold;
  color: #ffffff;
  cursor: pointer;
  background-color: #8D3DA5 !important;
}
.user-info {
  display: flex;
  align-items: center;
}
.user-info > span {
  margin-right: 1rem;
}
.delete {
  margin: 1rem;
}
.bus
{
  z-index: -2;
  width: 30vh;
  position: absolute;
  bottom: 0;
  left: 0;
  animation: move 8s infinite alternate;
  overflow: hidden;
}
.under {
  z-index: -3;
  position: fixed;
  bottom: 0;
  background-image: url("./../assets/background.png");
  height: 40vh;
  width: 100vw;
  background-size: cover;
  background-repeat: no-repeat;
}
.ease_in_out {
  animation-timing-function: ease-in-out;
  overflow: hidden;
}
@-webkit-keyframes move {
  from {
    margin-left: 20vw;
  }
  to {
    margin-left: 60vw;
  }
}
</style>