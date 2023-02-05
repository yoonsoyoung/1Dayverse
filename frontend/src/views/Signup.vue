<template>
  <div>
    <h1 class="title">Sign Up</h1>
    <div class="container">
      <div>
        <p class="radio-text">Select Your Job</p>
        <div class="radio">
          <div class="radio-input">
            <input type="radio" id="Tutee" v-model="job" name="job" value="Tutee" checked>
            <label for="Tutee">Tutee(학생)</label>
          </div>
          <div class="radio-input">
            <input type="radio" id="Tutor" v-model="job" name="job" value="Tutor">
            <label for="Tutor">Tutor(강사)</label>
          </div>
        </div>
      </div>
      <div class="form-element username-form check-need">
        <input type="text" name="username" v-model="username" id="username" required autofocus @input="CheckChange('username')"/>
        <label class="floating-label" for="username">User ID
        </label>
        <div v-if="username === '' || username_error" class="alert">
          <svg xmlns="http://www.w3.org/2000/svg" width="1rem" height="1rem" fill="currentColor" class="bi bi-exclamation-triangle" style="margin-right: 0.2rem">
            <path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
            <path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
          </svg>
          <span>
            {{ username_error }}
          </span>
        </div>
        <button class="check-btn" @click="checkUsername(this.username)">중복 확인</button>
      </div>
      <div class="form-element check-need">
        <input type="text" name="email" v-model="email" id="email" required @input="CheckChange('email')"/>
        <label class="floating-label" for="email">Email Address</label>
        <div v-if="email === '' || email_error" class="alert">
          <svg xmlns="http://www.w3.org/2000/svg" width="1rem" height="1rem" fill="currentColor" class="bi bi-exclamation-triangle" style="margin-right: 0.2rem">
            <path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
            <path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
          </svg>
          <span>
            {{ email_error }}
          </span>
        </div>
        <button class="check-btn" @click="checkEmail(this.email)">중복 확인</button>
      </div>
      <div class="form-element">
        <input type="password" name="password" v-model="password" id="password" required />
        <label class="floating-label password-label" for="password">Password</label>
        <div v-if="password === '' || password_error" class="alert">
          <svg xmlns="http://www.w3.org/2000/svg" width="1rem" height="1rem" fill="currentColor" class="bi bi-exclamation-triangle" style="margin-right: 0.2rem">
            <path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
            <path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
          </svg>
          <span>
            비밀번호를 확인해주세요.
          </span>
        </div>
      </div>
      <div class="explain">*비밀번호는 최소 8 자, 하나 이상의 문자, 하나의 숫자 및 하나의 특수 문자가 포함되어야 합니다.</div>
      <div class="form-element">
        <input type="password" name="password_confirm" v-model="password_confirm" id="password_confirm" required />
        <label class="floating-label" for="password_confirm">Password Confirm</label>
        <div v-if="password != password_confirm" class="alert">
          <svg xmlns="http://www.w3.org/2000/svg" width="1rem" height="1rem" fill="currentColor" class="bi bi-exclamation-triangle" style="margin-right: 0.2rem">
            <path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
            <path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
          </svg>
          <span>
            비밀번호가 다릅니다.
          </span>
        </div>
      </div>
      <div class="form-element">
        <input type="text" name="birth" v-model="birth" id="birth" required />
        <label class="floating-label" for="birth">Birth ex)2000.01.01</label>
        <div v-if="birth==='' || birth_error" class="alert">
          <svg xmlns="http://www.w3.org/2000/svg" width="1rem" height="1rem" fill="currentColor" class="bi bi-exclamation-triangle" style="margin-right: 0.2rem">
            <path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
            <path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
          </svg>
          <span>
            생일을 확인해주세요.
          </span>
        </div>
      </div>
      <div class="form-element">
        <input type="text" name="phone" v-model="phone" id="phone" required @keyup.enter="createUser"/>
        <label class="floating-label" for="phone">Phone Number ex)01012345678</label>
        <div v-if="phone === '' || phone_error" class="alert">
          <svg xmlns="http://www.w3.org/2000/svg" width="1rem" height="1rem" fill="currentColor" class="bi bi-exclamation-triangle" style="margin-right: 0.2rem">
            <path d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"/>
            <path d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"/>
          </svg>
          <span>
            핸드폰 번호를 확인해주세요.
          </span>
        </div>
      </div>
      <button class="btn active main-pink" v-if="!singup_error" @click="createUser">Sign Up</button>
      <button class="btn nonactive" v-else>Sign Up</button>
    </div>
    <div class="ease_in_out under">
      <img :src="require('@/assets/bus.png')" class="bus" alt="">
    </div>
  </div>
</template>

<script>
import axios from "axios"

  export default {
    name: "Signup",
    data() {
      return {
        job: 'Tutee',
        username: '',
        email: '',
        password: '',
        password_confirm: '',
        birth: '',
        phone: '',
        username_check: false,
        email_check: false,
      }
    },
    computed: {
      username_error() {
        if (!this.username) {
          return '닉네임을 확인해주세요'
        }
        if (!this.username_check) {
          return '닉네임 중복체크를 진행해주세요'
        }
        return false
      },
      email_error() {
        var regExp = /^[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*.[a-zA-Z]{2,3}$/i
        if (!this.email) {
          return '이메일을 확인해주세요'
        }
        if (this.email.match(regExp) === null) {
          return '이메일을 확인해주세요.'
        }
        if (!this.email_check) {
          return '이메일 중복체크를 진행해주세요'
        }
        return false
      },
      password_error() {
        var regExp = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/
        if (this.password != '') {
          if (this.password.match(regExp) != null) {
            return false
          }
        }
        return true
      },
      phone_error() {
        var regExp = /^01([0|1|6|7|8|9]?)-?([0-9]{3,4})-?([0-9]{4})$/
        if (this.phone != '') {
          if (this.phone.match(regExp) != null) {
            return false
          }
        }
        return true
      },
      birth_error() {
        var regExp = /^(19[0-9][0-9]|20\d{2}).(0[0-9]|1[0-2]).(0[1-9]|[1-2][0-9]|3[0-1])$/
        if (this.birth != '') {
          if (this.birth.match(regExp) != null) {
            return false
          }
        }
        return true
      },
      singup_error() {
        if (this.username ==='' || this.email ==='' || this.password ==='' || this.birth === '' || this.phone === '') {
          return true
        }
        else if (this.email_error || this.birth_error || this.phone_error || this.password_error) {
          return true
        }
        else if (this.password != this.password_confirm) {
          return true
        }
        return false
      },
    },
    methods: {
      createUser() {
        if (this.singup_error) {
          return false
        }
        const form = new FormData()
        form.append('username', this.username)
        form.append('email', this.email)
        form.append('password', this.password)
        form.append('passwordConfirmation', this.password)
        form.append('character', '1')
        form.append('profile_image', '') // 프로필 이미지
        form.append('birth_day', this.birth)
        form.append('phone_number', this.phone)
        if (this.job === 'Tutee') {
          form.append('teachable', 0)
        }
        else if (this.job === 'Tutor') {
          form.append('teachable', 1)
        }
        this.$store.dispatch('userStore/CREATE_USER', form)
          .then(() => {
            const Swal = require('sweetalert2')
            Swal.fire({
              text: '회원가입이 완료되셨습니다.',
              icon: 'success',
              confirmButtonText: 'To Login',
              confirmButtonColor: '#8D3DA5',
            }).then(() => {
              this.$router.push({ name: 'Login'})
            })
          })
          .catch(() => {
            const Swal = require('sweetalert2')
            Swal.fire({
              text: 'username와 email 중복체크를 진행해주세요.',
              icon: 'error',
              confirmButtonText: 'Back',
              confirmButtonColor: '#8D3DA5',
            })
          })
      },
      checkUsername(username) {
        if (!username) {
          return
        }
        const CHECK_USERNAME_URL = `https://k5c202.p.ssafy.io/od/accounts/check/username/${username}/`
        axios.get(CHECK_USERNAME_URL, username)
          .then((res) => {
            console.log(res.data.available)
            if (res.data.available === 1) {
              this.username_check = true
            }
            else {
              this.username_check = false
            }
            const Swal = require('sweetalert2')
            Swal.fire({
              text: '사용 가능한 닉네임입니다.',
              confirmButtonText: 'Back',
              confirmButtonColor: '#8D3DA5',
            })
          })
          .catch(() => {
            this.username_check = false
            const Swal = require('sweetalert2')
            Swal.fire({
              text: '이미 존재하는 닉네임입니다.',
              confirmButtonText: 'Back',
              confirmButtonColor: '#8D3DA5',
            })
          })
      },
      checkEmail(email) {
        if (!email) {
          return
        }
        const CHECK_USEREMAIL_URL = `https://k5c202.p.ssafy.io/od/accounts/check/email/${email}/`
        axios.get(CHECK_USEREMAIL_URL, email)
          .then((res) => {
            if (res.data.available === 1) {
              this.email_check = true
            }
            else {
              this.email_check = false
            }
            const Swal = require('sweetalert2')
            Swal.fire({
              text: '사용 가능한 이메일입니다.',
              confirmButtonText: 'Back',
              confirmButtonColor: '#8D3DA5',
            })
          })
          .catch(() => {
            this.email_check = false
            const Swal = require('sweetalert2')
            Swal.fire({
              text: '이미 등록된 이메일입니다.',
              confirmButtonText: 'Back',
              confirmButtonColor: '#8D3DA5',
            })
          })
      },
      CheckChange(value) {
        if (value === 'username') {
          this.username_check = false
        }
        else if (value === 'email') {
          this.email_check = false
        }
      }
    }
  }
</script>

<style scoped>
body {
  font-size: 16px;
}
.title
{
  font-size: 2rem;
  font-weight: bold;
  margin: 0 auto;
  margin-top: 3rem;
}
.container {
  display: block;
  width: 600px;
  margin: 50px auto;
  padding: 50px;
  border-radius: 8px;
  background: rgb(255, 234, 237);
}
.container .form-element {
  position: relative;
  padding-top: 16px;
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
.check-need input {
  width: 80%;
}
.floating-label {
  box-sizing: border-box;
  position: absolute;
  top: 45%;
  left: 20px;
  font-size: 14px;
  cursor: text;
  color: #8c8c8c;
  transition: font 0.1s ease, top 0.1s ease, transform 0.1s ease;
  transition: font 0.1s ease, top 0.1s ease, transform 0.1s ease,
    -webkit-transform 0.1s ease, -moz-transform 0.1s ease,
    -o-transform 0.1s ease;
}
.password-label
{
  top: 53%;
}
.explain
{
  text-align: start;
  font-size: 0.7rem;
  margin: 0.1rem 0 0 0.3rem;
}
.btn {
  width: 100%;
  height: 50px;
  margin-top: 24px;
  font-size: 18px;
  font-weight: 700;
  border-radius: 30px;
  border: none;
  color: #ffffff;
}
.active {
  cursor: pointer;
}
.nonactive {
  background-color: gray;
}
.active:hover
{
  background-color: #b9638d;
}
input:focus ~ .floating-label,
input:valid ~ .floating-label {
  font-size: 11px;
  top: 1.3rem;
}
input:-webkit-autofill,
input:-webkit-autofill:focus {
  transition: background-color 600000s 0s, color 600000s 0s;
}
.alert {
  position: absolute;
  top: 40%;
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
.check-need
{
  display: flex;
  justify-content: space-between;
}
.check-need .alert 
{
  right: 20%;
}
.check-btn
{
  width: 15%;
  border: none;
  cursor: pointer;
}
.radio {
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 1rem 1rem;
}
.radio-text {
  font-weight: bold;
  font-size: 1.5rem;
}
.radio-input {
  /* display: flex; */
  margin: auto 1rem;
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
@-webkit-keyframes move{
  from {
    margin-left: 10vw;
  }
  to{
    margin-left: 80vw;
  }
}
</style>