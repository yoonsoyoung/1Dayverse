<template>
  <div>
    <h1 class="title">Login</h1>
    <div class="container">
      <div>
        <p class="radio-text">Select Your Job</p>
        <div class="radio">
          <div class="radio-input">
            <input
              type="radio"
              id="Tutee"
              v-model="job"
              name="job"
              value="0"
              checked
            />
            <label for="Tutee">Tutee(학생)</label>
          </div>
          <div class="radio-input">
            <input type="radio" id="Tutor" v-model="job" name="job" value="1" />
            <label for="Tutor">Tutor(강사)</label>
          </div>
        </div>
      </div>
      <div class="form-element">
        <input
          type="text"
          name="email"
          autofocus
          v-model="email"
          id="email"
          required
        />
        <label class="floating-label" for="email">Enter Your Email</label>
        <div v-if="email === ''" class="alert">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="1rem"
            height="1rem"
            fill="currentColor"
            class="bi bi-exclamation-triangle"
            style="margin-right: 0.2rem"
          >
            <path
              d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"
            />
            <path
              d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"
            />
          </svg>
          <span> 이메일을 입력해주세요. </span>
        </div>
      </div>
      <div class="form-element">
        <input
          type="password"
          name="password"
          v-model="password"
          id="password"
          required
          @keyup.enter="Login"
        />
        <label class="floating-label" for="password"
          >Enter Your user Password</label
        >
        <div v-if="password === ''" class="alert">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="1rem"
            height="1rem"
            fill="currentColor"
            class="bi bi-exclamation-triangle"
            style="margin-right: 0.2rem"
          >
            <path
              d="M7.938 2.016A.13.13 0 0 1 8.002 2a.13.13 0 0 1 .063.016.146.146 0 0 1 .054.057l6.857 11.667c.036.06.035.124.002.183a.163.163 0 0 1-.054.06.116.116 0 0 1-.066.017H1.146a.115.115 0 0 1-.066-.017.163.163 0 0 1-.054-.06.176.176 0 0 1 .002-.183L7.884 2.073a.147.147 0 0 1 .054-.057zm1.044-.45a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566z"
            />
            <path
              d="M7.002 12a1 1 0 1 1 2 0 1 1 0 0 1-2 0zM7.1 5.995a.905.905 0 1 1 1.8 0l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995z"
            />
          </svg>
          <span> 비밀번호를 입력해주세요. </span>
        </div>
      </div>
      <button class="btn active main-pink" v-if="!login_error" @click="Login">
        Sign In
      </button>
      <button class="btn nonactive" v-else>Sign In</button>
    </div>
    <div class="ease_in_out under">
      <img :src="require('@/assets/bus.png')" class="bus" alt="" />
    </div>
  </div>
</template>

<script>
export default {
  name: "Login",
  data() {
    return {
      email: "",
      password: "",
      job: "0",
    };
  },
  computed: {
    login_error() {
      if (
        this.email === "" ||
        this.password === "" ||
        (this.job != "0" && this.job != "1")
      ) {
        console.log(this.job, typeof this.job);
        return true;
      }
      return false;
    },
  },
  methods: {
    async Login() {
      if (this.login_error) {
        return false;
      }
      const form = new FormData();
      form.append("email", this.email);
      form.append("password", this.password);
      if (this.job === "0") {
        form.append("teachable", 0);
      } else if (this.job === "1") {
        form.append("teachable", 1);
      }
      this.$store
        .dispatch("userStore/AUTH_USER", form)
        .then(() => {
          this.$router.push({ name: "Main" });
        })
        .catch(() => {
          const Swal = require("sweetalert2");
          Swal.fire({
            text: "직업 혹은 username 혹은 비밀번호를 확인해주세요.",
            icon: "error",
            confirmButtonText: "Back",
            confirmButtonColor: "#8D3DA5",
          });
        });
    },
  },
};
</script>

<style scoped>
body {
  font-size: 16px;
  text-align: start;
}
.title {
  font-size: 2rem;
  font-weight: bold;
  margin: 0 auto;
  margin-top: 3rem;
}
.container {
  display: block;
  width: 600px;
  margin: 50px auto;
  border-radius: 4px;
  padding: 50px;
  border-radius: 8px;
  background: rgb(255, 234, 237);
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
  background-color: #f6f6f6;
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

.bus {
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
.active:hover {
  background-color: #b9638d;
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
