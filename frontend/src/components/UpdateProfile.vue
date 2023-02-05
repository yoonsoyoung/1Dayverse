<template>
  <div>
    <div v-if="modal" id="my-modal" class="container">
      <div class="modal" v-if="!delete_modal">
        <div>
          <div class="profile-text">Update Profile</div>
          <div class="form-element">
            <input type="username" name="username" v-model="username" id="username" required />
            <label class="floating-label" for="username">Enter Your user username</label>
            <div v-if="username === ''" class="alert">
              <svg xmlns="http://www.w3.org/2000/svg" width="1rem" height="1rem" fill="currentColor" class="bi bi-exclamation-triangle" style="margin-right: 0.2rem">
                <path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
                <path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
              </svg>
              <span>
                username을 입력해주세요.
              </span>
            </div>
          </div>
          <div class="form-element">
            <input type="birth" name="birth" v-model="birth" id="birth" required />
            <label class="floating-label" for="birth">Enter Your user birth</label>
            <div v-if="birth === '' || birth_error" class="alert">
              <svg xmlns="http://www.w3.org/2000/svg" width="1rem" height="1rem" fill="currentColor" class="bi bi-exclamation-triangle" style="margin-right: 0.2rem">
                <path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
                <path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
              </svg>
              <span>
                생일을 입력해주세요.
              </span>
            </div>
          </div>
          <div class="form-element">
            <input type="phone" name="phone" v-model="phone" id="phone" required />
            <label class="floating-label" for="phone">Enter Your user phone</label>
            <div v-if="phone === '' || phone_error" class="alert">
              <svg xmlns="http://www.w3.org/2000/svg" width="1rem" height="1rem" fill="currentColor" class="bi bi-exclamation-triangle" style="margin-right: 0.2rem">
                <path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
                <path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
              </svg>
              <span>
                핸드폰 번호를 입력해주세요.
              </span>
            </div>
          </div>
          <button class="btn cancle active" @click="$emit('close')">cancle</button>
          <button v-if="!update_error" class="btn update active" @click="update">Update</button>
          <button v-else class="btn nonactive">Update</button>
        </div>
        <button class="delete" @click="changeDelete">회원탈퇴하기</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "UpateProfile",
  data() {
    return {
      username: null,
      birth: null,
      phone: null,
      profilename: this.$route.params.username,
    }
  },
  props: {
    // 전체 모달 변수
    modal: {
      type: Boolean
    },
  },
  created() {
    this.username = this.$store.getters['userStore/getUsername'] 
    this.birth = this.$store.getters['userStore/getUserBirth'] 
    this.phone = this.$store.getters['userStore/getUserPhone'] 
  },
  computed: {
    phone_error() {
      if (!this.phone) {
        return true
      }
      var regExp = /^01([0|1|6|7|8|9]?)-?([0-9]{3,4})-?([0-9]{4})$/
      if (this.phone != '') {
        if (this.phone.match(regExp) != null) {
          return false
        }
      }
      return true
    },
    birth_error() {
      if (!this.birth) {
        return true
      }
      var regExp = /^(19[0-9][0-9]|20\d{2}).(0[0-9]|1[0-2]).(0[1-9]|[1-2][0-9]|3[0-1])$/
      if (this.birth != '') {
        if (this.birth.match(regExp) != null) {
          return false
        }
      }
      return true
    },
    update_error() {
      if (this.username ==='' || this.birth === '' || this.phone === '') {
        return true
      }
      else if (this.birth_error || this.phone_error) {
        return true
      }
      return false
    }
  },
  methods: {
    update() {
      const form = new FormData()

      form.append('username', this.username)
      form.append('birth_day', this.birth)
      form.append('phone_number', this.phone)
      form.append('character', '')
      const info = [form, this.profilename]
      this.$store.dispatch('userStore/UPDATE_PROFILE', info)
        .then(() => {
          const Swal = require('sweetalert2')
          Swal.fire({
            text: '프로필 수정이 완료되었습니다.',
            icon: 'success',
            confirmButtonText: 'To Profile',
            confirmButtonColor: '#8D3DA5',
          }).then(() => {
            this.$emit('refresh')
          })
        })
        .catch(() => {})
    },
    changeDelete() {
      const Swal = require('sweetalert2')
      Swal.fire({
        text: '정말로 삭제하시겠습니까?',
        confirmButtonText: 'Delete',
        showCancelButton: true,
        confirmButtonColor: '#8D3DA5',
      }).then((result) => {
        if (result.isConfirmed) {
          this.$store.dispatch('userStore/DELETE_USER', this.username)
          Swal.fire({
              title: '삭제되셨습니다.',
              showClass: {
                popup: 'animate__animated animate__fadeInDown'
              },
              hideClass: {
                popup: 'animate__animated animate__fadeOutUp'
              }
            }).then(() => {
              this.$router.push({ name: 'Main'})
            })
        } 
      })
    },
    toMain() {
      this.$router.push({ name: 'Main'})
    },
    refresh() {
      this.changemodal()
      this.$router.push({name:"Profile", params: {username : this.username}})
    }
  }
}
</script>

<style scoped>
.container {
  position: fixed;
  left: 0;
  top: 0;
  width: 100%;
  overflow: auto;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.65);
}

.modal {
  margin: 0 auto;
  width: 40%;
  min-width: 400px;
  max-width: 600px;
  box-shadow: 0 5px 8px 0 rgba(0, 0, 0, 0.2), 0 7px 20px 0 rgba(0, 0, 0, 0.17);
  animation-name: modalopen;
  animation-duration: var(--modal-duration);
  padding: 10px 20px;
  background: #ceb3ce;
  border-radius: 10px;
}

.container
{
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.container .form-element {
  position: relative;
  padding-bottom: 16px;
}
.form-element input {
  box-sizing: border-box;
  width: 100%;
  padding: 16px 20px 0 20px;
  height: 50px;
  font-size: 16px;
  border: none;
  border-radius: 5px;
  background-color: #F6F6F6;
  color: #000000;
}
.floating-label {
  box-sizing: border-box;
  position: absolute;
  top: 25%;
  left: 20px;
  font-size: 14px;
  cursor: text;
  color: #8c8c8c;
  transition: font 0.1s ease, top 0.1s ease, transform 0.1s ease;
  transition: font 0.1s ease, top 0.1s ease, transform 0.1s ease,
    -webkit-transform 0.1s ease, -moz-transform 0.1s ease,
    -o-transform 0.1s ease;
}
input:focus ~ .floating-label,
input:valid ~ .floating-label {
  font-size: 11px;
  top: 7px;
}
input:-webkit-autofill,
input:-webkit-autofill:focus {
  transition: background-color 600000s 0s, color 600000s 0s;
}
.alert {
  position: absolute;
  top: 20%;
  right: 0%;
  color: rgba(255, 97, 97, 0.822);
  text-align: start;
  font-size: 0.8rem;
  padding: 0 1rem;
  vertical-align: top;
  display: flex;
  align-content: center;
  margin-top: 0.3rem;
}
.image-input{
  width: 100%;
  margin: 8px 0 16px 0;
}
.user-image {
  width: 30vw;
  height: 30vw;
  max-width: 300px;
  max-height: 300px;
  margin-bottom: 16px;
  border-radius: 100%;
}
.btn {
  width: 20%;
  height: 30px;
  margin: 0 12px;
  margin-bottom: 16px;
  border-radius: 30px;
  border: none;
  font-weight: bold;
  color: #ffffff;
}
.active {
  cursor: pointer;
}
.nonactive {
  background-color: gray;
}
.cancle {
  background-color: red;
}
.update {
  background-color: rgb(75, 145, 75);
}
.profile-text {
  margin-top: 1rem;
  text-align: center;
  font-weight: bold;
  margin-bottom: 1rem;
}
.delete {
  font-size: 0.7rem;
  background: none;
  border: none;
  cursor: pointer;
}
.radio {
  display: flex;
  justify-content: center;
  margin: 1rem 1rem;
}
.radio-text {
  font-weight: bold;
  font-size: 1rem;
  margin: 1rem 1rem;
}
.radio-input {
  margin: 0 1rem;
}
.delete-btn {
  cursor: pointer;
}
</style>