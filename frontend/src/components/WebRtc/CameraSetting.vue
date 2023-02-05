<template>
  <div id="CamSettingRoot">
    <div class="row content-box modal-box">
      <div v-if="alertActive" class="alert-box alert alert-info alert-dismissible fade show">
        <span>
          <strong>알림!</strong> 1Dayverse 는 카메라와 마이크 권한을 필요로 합니다.
        </span>
        <button type="button" class="close" data-dismiss="alert" @click="closeBtn">&times;</button>
      </div>
      <div class="setting-card shadow-lg">
        <p class="card-title mb-5">카메라 설정</p>
        <div class="card-box">
          <div class="left_box">
            <user-video class="set-cam" :stream-manager="publisher" />
          </div>

          <div class="right-box">
            <ul class="set-option">
              <li class="col">
                <p class="col icon"><i class="fas fa-video"></i></p>
                <select name="cam" id="cam" v-model="selectWebCam">
                  <option disabled >카메라 설정</option>
                  <option value="" @click="enabledSetting(video)">설정 안함</option>
                  <option v-for="(item, idx) in webcam" :key="idx" :value=item.deviceId @change="changeCamera">{{item.label}}</option>
                </select>
              </li>
              <li class="col">
                <p class="col icon"><i class="fas fa-microphone"></i></p>
                <select name="cam" id="cam" v-model="selectAudio">
                  <option disabled >마이크 설정</option>
                  <option value="" @click="enabledSetting(audio)">설정 안함</option>
                  <option v-for="(item, idx) in audio" :key="idx" :value=item.deviceId @change="changeAudio">{{item.label}}</option>
                </select>
              </li>
            </ul>
            <div class="reset-col">
              <button class="reset-btn" @click.stop="reset"><i class="fas fa-redo-alt"></i></button>
            </div>
          </div>
        </div>
        <div class="btn-box mt-4">
          <button v-if="activeModal" class="cancel-btn" @click.stop="cancel">취소</button>
          <button v-if="activeModal" class="submit-btn" @click.stop="submitSetting">설정 저장</button>
          <button v-else class="submit-btn" @click.stop="submitSetting">설정 저장</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { OpenVidu } from 'openvidu-browser';
import UserVideo from '@/components/WebRtc/UserVideo';
const OPENVIDU_SERVER_URL = "https://k5c202.p.ssafy.io:5443";
const OPENVIDU_SERVER_SECRET = "oneday";
axios.defaults.headers.post['Content-Type'] = 'application/json';
export default {
  name: 'CamSetting',
  components: {
    UserVideo,
  },
  props: {
    activeModal: Boolean,
  },
  data() {
    return {
      OV: undefined,
      roomName: 'setting',
			session: undefined,
			mainStreamManager: undefined,
			publisher: undefined,
      webcam: [],
      audio: [],
      selectWebCam: '',
      selectAudio: '',
      setting: {
        audioSource: undefined, 
        videoSource: undefined, 
        publishAudio: false,  	
        publishVideo: false,  	
        resolution: '640x480',  
        frameRate: 30,			
        insertMode: 'APPEND',	
        mirror: false
      },
			user: {},
      alertActive: true,
    }
  },
  created() {
    this.joinSession();
    this.user = this.$store.state.userStore;
  },
  unmounted() {
      if (this.session) this.session.disconnect();

			this.session = undefined;
			this.mainStreamManager = undefined;
			this.publisher = undefined;
			this.subscribers = [];
			this.OV = undefined;
    
  },
  watch: {
    selectWebCam: function() {
      this.changeCamera();
    },
    selectAudio: function() {
      this.changeAudio();
    }
  },
  methods: {
    closeBtn() {
      this.alertActive = false;
    },
    cancel() {
      this.$emit('settingModal', 2);
    },
    findDevices() {
      this.OV.getDevices().then(devices => {
        let videoDevices = devices.filter(device => device.kind === 'videoinput');
        let audioDevices = devices.filter(device => device.kind === 'audioinput');
        this.webcam = videoDevices;
        this.audio = audioDevices;
      })
    },
    changeCamera() {
      
      this.setting.videoSource = this.selectWebCam;

      this.OV.getDevices().then(devices => {
        let videoDevices = devices.filter(device => device.kind === 'videoinput');
        if(videoDevices && videoDevices.length > 1) {
          var newPublisher = this.OV.initPublisher(undefined, {
            videoSource: this.selectWebCam,
            publishAudio: true,
            publishVideo: true,
            videoActive: true,
          });
          this.session.unpublish(this.publisher).then(() => {
            this.publisher = newPublisher;

            this.session.publish(this.publisher).then(() => {
              console.log("New publisher published!")
            })
          })
        } else {
          // this.publisher.stream.outboundStreamOpts.publisherProperties.videoSource = this.selectWebCam;
          // this.publisher.videoActive = true;
          this.publisher.publishVideo(true);
        }
      })

      // this.publisher.publishVideo(true);
      this.$store.commit('camStore/setVideo', this.selectWebCam);
    },
    changeAudio() {
      this.publisher.stream.outboundStreamOpts.publisherProperties.audioSource = this.selectAudio;
      this.publisher.publishAudio(true);
      this.$store.commit('camStore/setAudio', this.selectAudio);
    },
    enabledSetting(v) {
      if (v === 'audio') {
        this.selectAudio = '';
        this.publisher.publishAudio(false);
      } else if(v === 'video') {
        console.log("설정안함");
        this.selectWebCam = '';
        this.publisher.publishAudio(false);
        this.session.unpublish(this.publisher);
      }
    },
    submitSetting() {
      if(this.activeModal) {
        this.leaveSession();
        this.$emit('settingModal', 2);
      } else {
        this.leaveSession();
        this.$router.replace('/study');
      }
    },
    reset() {
      this.$router.go();
    },
		joinSession () {
      
			this.OV = new OpenVidu();
			this.session = this.OV.initSession();

			this.getToken(this.roomName).then(token => {
				this.session.connect(token, { clientData: this.user })
					.then(() => {
						this.findDevices();
            let initpublisher = this.OV.initPublisher(undefined, this.setting);
            this.publisher = initpublisher;

						this.session.publish(this.publisher);
					})
					.catch(error => {
						console.log('There was an error connecting to the session:', error.code, error.message);
					});
			});

			window.addEventListener('beforeunload', this.leaveSession)
		},

		leaveSession () {
			if (this.session) this.session.disconnect();

			this.session = undefined;
			this.mainStreamManager = undefined;
			this.publisher = undefined;
			this.subscribers = [];
			this.OV = undefined;

			window.removeEventListener('beforeunload', this.leaveSession);
		},
		getToken (roomName) {
			return this.createSession(roomName).then(sessionId => this.createToken(sessionId));
		},

		// See https://docs.openvidu.io/en/stable/reference-docs/REST-API/#post-session
		createSession (sessionId) {
			return new Promise((resolve, reject) => {
				axios
					.post(`${OPENVIDU_SERVER_URL}/openvidu/api/sessions`, JSON.stringify({
						customSessionId: sessionId,
					}), {
						auth: {
							username: 'OPENVIDUAPP',
							password: OPENVIDU_SERVER_SECRET,
						},
					})
					.then(response => response.data)
					.then(data => resolve(data.id))
					.catch(error => {
						if (error.response.status === 409) {
							resolve(sessionId);
						} else {
							console.warn(`No connection to OpenVidu Server. This may be a certificate error at ${OPENVIDU_SERVER_URL}`);
							if (window.confirm(`No connection to OpenVidu Server. This may be a certificate error at ${OPENVIDU_SERVER_URL}\n\nClick OK to navigate and accept it. If no certificate warning is shown, then check that your OpenVidu Server is up and running at "${OPENVIDU_SERVER_URL}"`)) {
								location.assign(`${OPENVIDU_SERVER_URL}/accept-certificate`);
							}
							reject(error.response);
						}
					});
			});
		},
		createToken (sessionId) {
			return new Promise((resolve, reject) => {
				axios
					.post(`${OPENVIDU_SERVER_URL}/openvidu/api/sessions/${sessionId}/connection`, {}, {
						auth: {
							username: 'OPENVIDUAPP',
							password: OPENVIDU_SERVER_SECRET,
						},
					})
					.then(response => response.data)
					.then(data => resolve(data.token))
					.catch(error => reject(error.response));
			});
		},
	}
}
</script>

<style lang="scss" scoped>
@import "../../styles/CameraSetting.scss";
</style>