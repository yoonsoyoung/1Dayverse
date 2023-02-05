<template>
  <div id="WebCamRoot">
    <div id="session" v-if="data.session">
			<div id="video-container" class="col">
				<camera :data="data" :OVScreen="OVScreen" :location="location" v-on:leaveSession="leaveSession" v-on:updateStream="updateStream" v-on:joinScreen="joinScreen"/>
				<chat :data="data" v-on:send="send" v-on:settingModal="settingModal" />
				<camera-setting v-if="activeSetting" :activeModal="activeSetting" v-on:settingModal="settingModal" />
			</div>
		</div>
  </div>
</template>

<script>
import axios from 'axios';
import { OpenVidu } from 'openvidu-browser';
import Camera from './Camera.vue';
import Chat from './Chat.vue';
import CameraSetting from './CameraSetting.vue'

axios.defaults.headers.post['Content-Type'] = 'application/json';

const OPENVIDU_SERVER_URL = "https://k5c202.p.ssafy.io:5443";
const OPENVIDU_SERVER_SECRET = "oneday";
export default {
  name: 'WebCam',
  components: {
    Camera,
    Chat,
		CameraSetting,
  },
	props: {
		location: String,
	},
  data() {
    return {
			data: {
				OV: undefined,
				roomName: '',
				session: undefined,
				mainStreamManager: undefined,
				publisher: undefined,
				subscribers: [],
				participants: 1,
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
				// chat
				message: [],
				MessageBell: false,
				msgTime: '',
				// share screen
				share: {
					active: false,
					screen: undefined,
				},
			},
			OVScreen: {
				OV: undefined,
				session: undefined,
			},
			user: {},
			activeSetting: false,
			lectureId: '',
			lecutreCate: '',
    }
  },
  created() {
		this.user = this.$store.state.userStore;
		this.lectureId = localStorage.getItem('inLectureId');
		this.lecutreCate = localStorage.getItem('inLectureName');
		this.data.roomName = this.location + this.lecutreCate + this.lectureId;
		this.OVScreen.roomName = this.data.roomName;
		this.data.setting.videoSource = this.$store.state.camStore.video;
		this.data.setting.audioSource = this.$store.state.camStore.audio;
    this.joinSession();
  },
  unmounted() {
      if (this.data.session) this.data.session.disconnect();

			this.data.session = undefined;
			this.data.mainStreamManager = undefined;
			this.data.publisher = undefined;
			this.data.subscribers = [];
			this.data.OV = undefined;
			this.data.message = [];
			this.data.share.active = false;
			this.data.share.screen = undefined;
    
  },
  methods: {
		settingModal(v) {
			if (v == 1) {
				this.activeSetting = !this.activeSetting;
			} else {
				this.activeSetting = false;
			}
    },
		joinSession () {
			this.data.OV = new OpenVidu(); // 유저 캠
			this.OVScreen.OV = new OpenVidu(); // 화면 공유

			this.data.session = this.data.OV.initSession();
			this.OVScreen.session = this.OVScreen.OV.initSession();
			

			this.data.session.on('streamCreated', ({ stream }) => {
				const subscriber = this.data.session.subscribe(stream);
				if(stream.typeOfVideo == "CAMERA") {
					this.data.subscribers.push(subscriber);
				} else if (stream.typeOfVideo == "SCREEN") {
					this.data.share.active = true;
					this.data.share.screen = subscriber;
				}
					this.data.participants = this.data.subscribers.length + 1;
			});

			this.data.session.on('streamDestroyed', ({ stream }) => {
				const index = this.data.subscribers.indexOf(stream.streamManager, 0);
				if (index >= 0) {
					this.data.subscribers.splice(index, 1);
				}
				if(stream.typeOfVideo == "SCREEN")
					this.data.share.active = false;
				this.data.participants = this.data.subscribers.length + 1;
			});

			// Chat
			this.data.session.on('signal:my-chat', (event) => {
				this.data.message.push({sender: JSON.parse(event.from.data), message: event.data, time: this.msgTime});
				this.data.MessageBell = true;
			});

			// 캠 getToken
			this.getToken(this.data.roomName).then(token => {
				this.data.session.connect(token, { clientData: this.user })
					.then(() => {
						let publisher = this.data.OV.initPublisher(undefined, this.data.setting);

						this.data.mainStreamManager = publisher;
						this.data.publisher = publisher;

						this.data.session.publish(this.data.publisher);
					})
					.catch(error => {
						console.log('There was an error connecting to the session[Cam]:', error.code, error.message);
					});
			});
			// 화면 공유 getToken
			this.getToken(this.OVScreen.roomName).then(token => {
				this.OVScreen.session.connect(token, { clientData: this.user })
					.catch(error => {
						console.log('There was an error connecting to the session[OVScreen]:', error.code, error.message);
					});
			});

			window.addEventListener('beforeunload', this.leaveSession)
		},
		

		leaveSession () {
			if (this.data.session) this.data.session.disconnect();
			if (this.OVScreen.session) this.OVScreen.session.disconnect();

			this.data.session = undefined;
			this.data.mainStreamManager = undefined;
			this.data.publisher = undefined;
			this.data.subscribers = [];
			this.data.OV = undefined;
			this.data.message = [];

			this.OVScreen.session = undefined;
			this.OVScreen.OV = undefined;

			window.removeEventListener('beforeunload', this.leaveSession);
			// this.$router.replace('/study');
			window.close();
		},

		updateMainVideoStreamManager (stream) {
			if (this.data.mainStreamManager === stream) return;
			this.data.mainStreamManager = stream;
		},

		getToken (roomName) {
			return this.createSession(roomName).then(sessionId => this.createToken(sessionId));
		},

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

		// Chat
		send(sendMessage, sendTime) {
			this.msgTime = sendTime
			this.data.session.signal({
				data: sendMessage,
				to: [],
				type: 'my-chat'
			})
			.catch(error => {
					console.error(error);
			});
		},

		updateStream(type) {
			if(type == 1) {
				this.data.setting.publishAudio = !this.data.setting.publishAudio;
				this.data.publisher.publishAudio(this.data.setting.publishAudio);
			} else {
				this.data.setting.publishVideo = !this.data.setting.publishVideo;
				this.data.publisher.publishVideo(this.data.setting.publishVideo);
			}
		}
	}
}
</script>

<style lang="scss" scoped>
@import '../../styles/WebCam.scss';
</style>