<template>
  <div class="box">
    <footer>
      <div class="footer">
        <div class="site">
          <img :src="require('@/assets/1DAYVERSE.png')" class="image" alt="">
          <p class="rights">© 1Dayverse | All Rights Reserved</p>
        </div>
        <div class="site-explain">     
          <div class="footer-section-contact">
            <div class="footer-header">Contact us</div>
            <div class="member">
              <div><a href="https://github.com/kdm111" target='_blank'>Park Jun Su<img :src="require('@/assets/github_logo.png')" alt=""></a></div>
              <div><a href="https://github.com/Ryujiwoo" target='_blank'>Ryu Ji Woo<img :src="require('@/assets/github_logo.png')" alt=""></a></div>
              <div><a href="https://github.com/quipu1" target='_blank'>Sung Ru Bi<img :src="require('@/assets/github_logo.png')" alt=""></a></div>
              <div><a href="https://github.com/yoonsoyoung" target='_blank'>Yoon So Young<img :src="require('@/assets/github_logo.png')" alt=""></a></div>
              <div><a href="https://github.com/snflo16" target='_blank'>Lee Hye Eun<img :src="require('@/assets/github_logo.png')" alt=""></a></div>
            </div>
          </div>
          <div class="footer-section-news">
            <div class="footer-header">Sign up for updates</div>
            <div class="news">
              <div class="news-explain">Get the latest news, product updates, and best practices.</div>
              <div class="news-apply">
                <form action="#" class="singup-form">
                  <input class="news-input" type="email" v-model="email" placeholder="Email Address" required>
                  <input type="submit" placeholder="Sign In" @click="signup" class="news-button">
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
export default {
  name: "Footer",
  data() {
    return {
      email: ""
    }
  },
  computed: {
    email_error() {
      var regExp = /^[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*.[a-zA-Z]{2,3}$/i
      if (!this.email) {
        return '이메일을 확인해주세요'
      }
      if (this.email.match(regExp) === null) {
        return '이메일을 확인해주세요.'
      }
      return false
    },
  },
  methods: {
    signup() {
      if (this.email != "" && !this.email_error) {
        const Swal = require('sweetalert2')
        Swal.fire({
          icon: 'Success',
          text: '신청이 완료되었습니다!',
          confirmButtonText: 'OK'
        })
      }
    }
  }
}
</script>

<style scoped>
a{
  text-decoration: none;
  color: #bdbdbd;
  font-weight: lighter;
}
.box {
  background-color: hsl(286, 32%, 22%);
  color: white;
  width: 100%;
  height: 30vh;
}
.footer 
{
  display: flex;
  width: 100%;
  padding: 0.3vh 0 0.3vh 0;
  margin: 0 0;
  justify-content: space-between;
}
.footer > div
{
  margin: 0 5rem;
}
.rights
{
  font-size: 0.8rem;
  margin: 0 1rem;
  font-weight: lighter;
  color: #bdbdbd;
  text-align: start;
}
.site
{
  font-size: 0.6vh;
  display: flex;
  flex-direction: column;
}
.site-explain {
  display: flex;
  flex-direction: row;
}
.image
{
  height: 10vh;
  margin: 0 auto;
  margin-left: 0;
  margin-top: 0.8rem;
}
.footer-section-contact {
  display: flex;
  flex-direction: column;
  width: 150px;
  margin: 0 3rem;
}
.footer-section-news {
  display: flex;
  flex-direction: column;
  width: 300px;
  margin-right: 1rem;
}
.footer-header {
  font-weight: bold;
  font-size: 1rem;
  margin: 1.3rem 0 0.7rem 0;
  text-align: start;
}
.member > div {
  display: flex;
  font-weight: lighter;
  color: #bdbdbd;
  font-size: 0.8rem;
  margin: 0 0 0.5rem 0;
}
a > img
{
  height: 1rem;
  margin: 0 0 0 0.5rem;
}
.news {
  margin: 0 0 0.5rem 0;
}
.news-explain {
  font-weight: lighter;
  color: #bdbdbd;
  font-size: 0.8rem;
  text-align: start;
  margin: 0 0 0.5rem 0;
}
.news-apply {
  margin-top: 1.5rem;
  display: flex;
}
.news-input {
  width: 60%;
  border-radius: 30px;
  margin-right: 5%;
  border: solid 0.05rem white;
  background: transparent;
  color: white;
  font-weight: lighter;
  height: 5vh;
}
.news-button {
  width: 32%;
  border-radius: 30px;
  height: 5.5vh;
  background: #aa7ab8;
  border: none;
  color: rgb(41, 41, 41);
  font-weight: lighter;
  font-weight: bold;
  cursor: pointer;
}
.news-button:hover {
  background: #8d3da5;
}
.singup-form {
  width: 100%;
}
</style>

