<template>
  <div id="CameraRoot">
    <div v-if="alertActive" class="alert-box alert alert-info alert-dismissible fade show">
      <span>
        <strong>알림!</strong> 화면 공유 시 상태창을 '숨기기' 하지 마세요.
      </span>
      <button type="button" class="close" data-dismiss="alert" @click="closeBtn">&times;</button>
    </div>
    <div class="cam-content-box mt-5">
      <div class="video-content" :class="{'flex-column': datas.share.active}">
        <div id="prev" class="paging-btn">
          <button class="webcam-btn page-btn" @click="page -= 1;" v-if="prev">
            <div v-if="!datas.share.active"><i class="fas fa-chevron-left"></i></div>
            <div v-else><i class="fas fa-chevron-up"></i></div>
          </button>
        </div>
        <div id="videos">
          <user-video :class="{publisher : true}" :stream-manager="datas.publisher" v-if="page == 0"></user-video>
          <user-video :class="{subscribers : true}" v-for="(sub, idx) in pageSub" :key="idx" :stream-manager="sub" ></user-video>
        </div>
        <div id="next" class="paging-btn">
          <button class="webcam-btn page-btn" @click="page += 1;" v-if="next">
            <div v-if="!datas.share.active"><i class="fas fa-chevron-right"></i></div>
            <div v-else><i class="fas fa-chevron-down"></i></div>
          </button>
        </div>
      </div>
      <div id="share-content" v-if="datas.share.active">
        <div id="share-screen" v-if="datas.share.screen">
          <user-video class="screen-video" :stream-manager="datas.share.screen"></user-video>
        </div>
      </div>
    </div>
    <div id="webcam-nav" class="my-2">
      <button id="btnSetvideo" @click="updateStream(0)" class="webcam-btn">
          <div v-if="!datas.setting.publishVideo"><i class="webcam-btn-icon fas fa-video-slash"></i></div>
          <div v-else><i class="webcam-btn-icon fas fa-video"></i></div>
      </button>
      <button id="btnSetAudio" @click="updateStream(1)" class="webcam-btn">
          <div v-if="!datas.setting.publishAudio"><i class="webcam-btn-icon fas fa-microphone-slash"></i></div>
          <div v-else><i class="webcam-btn-icon fas fa-microphone"></i></div>
      </button>
      <button id="btnShareScreen" @click="shareScreen" class="webcam-btn">
        <div v-if="!screenShare"><i class="webcam-btn-icon far fa-play-circle"></i></div>
        <div v-else><i class="webcam-btn-icon far fa-stop-circle"></i></div>
      </button>
      <button id="btnLeaveSession" @click="leaveSession" class="webcam-btn"><i class="webcam-btn-icon leave-icon fas fa-phone-slash"></i></button>
    </div>

  </div>
</template>

<script>
import axios from 'axios'
import UserVideo from '@/components/WebRtc/UserVideo.vue'
axios.defaults.headers.post['Content-Type'] = 'application/json';
export default {
  name: 'Camera',
  components: {
    UserVideo,
  },
  props: {
    data: Object,
    location: String,
    OVScreen: Object,
  },
  data() {
    return {
      page: 0,
      screenShare : false,
      maxHeight : 0,
      datas: this.data,
      screens: this.OVScreen,
      screenSrc: {},
      alertActive: false,
    }
  },
  mounted() {
    const target = document.querySelector('.cam-content-box')
    const targetRect = target.getBoundingClientRect();
    this.maxHeight = targetRect.height;
  },
  updated(){
    const screen = document.querySelector('.screen-video video');
    if(screen !== null) screen.setAttribute('style', `max-height:${this.maxHeight-80}px;`);
  },

  computed : {
    next : function(){
      if((!this.datas.share.active && this.datas.subscribers.length+1 - (this.page+1)*6 > 0 ) || (this.datas.share.active && this.datas.subscribers.length+1 - (this.page+1)*4 > 0 )){
        return true;
      }
      return false;
    },
    prev : function(){
      if(this.page > 0){
        return true;
      }
      return false;
    },
    totalPage : function(){
      let remain = (this.datas.subscribers.length+1)%6;
      if(remain != 0){
        return (this.datas.subscribers.length+1)/6+1;
      }
      return (this.datas.subscribers.length+1)/6;
    },
    pageSub : function(){
      if(this.page == 0){
        if(!this.datas.share.active){
          return this.datas.subscribers.slice(0,5);
        }
        return this.datas.subscribers.slice(0,3);
      }else{
        if(!this.datas.share.active){
          return this.datas.subscribers.slice(this.page*5, Math.min(this.page*5+6,this.datas.subscribers.length));
        }
        return this.datas.subscribers.slice(this.page*3, Math.min(this.page*3+4,this.datas.subscribers.length));
      }
    },
  },

  methods: {
    closeBtn() {
      this.alertActive = false;
    },
    updateMainVideoStreamManager (stream) {
			if (this.datas.mainStreamManager === stream) return;
			this.datas.mainStreamManager = stream;
		},
    updateStream(type) {
      this.$emit('updateStream', type);
		},
    shareScreen() {
      let screen = this.screens.OV.initPublisher(undefined, {
        resolution: "1280x720",
        videoSource: "screen",
      });
      if(!this.screenShare) {
        screen.once('accessAllowed', () => {
          screen.stream
            .getMediaStream()
            .getVideoTracks()[0]
            .addEventListener('ended', () => {
              // Stop sharing
              this.screens.session.unpublish(screen);
              this.screenShare = false;
              this.datas.share.active = false;
            });
          this.screenShare = true;
          this.alertActive = true;
          this.screens.session.publish(screen);
          this.datas.share.active = false;
        });
        screen.once("accessDenied", () => {
          this.screenShare = false;
          this.datas.share.active = false;
          console.warn("ScreenShare: Access Denied");
        });
      } 
    },
    
    leaveSession() {
        this.datas.share.active = false;
        this.$emit('leaveSession');
    },

  },
}
</script>

<style lang="scss" scoped>
@import "../../styles/Camera.scss";
</style>