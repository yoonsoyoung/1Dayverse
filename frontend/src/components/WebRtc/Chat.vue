<template>
  <div id="ChatRoot">
    <div class="chat-content-box">
      <div v-if="activeChat || activeUsers" class="chat-main">
        <div class="chat-title">
          <p v-if="activeChat">채팅</p>
          <p v-if="activeUsers">참여자</p>
        </div>

        <div v-if="activeChat" class="chat-box">
          <div v-for="(msg, idx) in datas.message" :key="idx" class="message">
            <table v-if="msg.sender.userid !== user.userid" class="msg-table">
              <tr class="user-info" colspan="2">
                <td class="user-name">{{msg.sender.clientData.username}}</td>
                <td class="send-time">[{{msg.time}}]</td>
              </tr>
              <tr class="user-msg">
                <td>
                  <p class="user-msg-text">{{msg.message}}</p>
                </td>
              </tr>
            </table>
            <table v-else class="msg-table my-msg">
              <tr class="user-info">
                <td class="user-name">{{msg.sender.clientData.username}}</td>
              </tr>
              <tr>
                <td>
                  <p class="user-msg-text">{{msg.message}}</p>
                </td>
              </tr>
            </table>
          </div>
        </div>
        
        <div v-if="activeUsers" class="users-box">
          <div class="users-info my-info">
            <span class="my-info-icon"><i class="fas fa-user"></i></span>
            <span class="users-info-name">{{user.username}} (나)</span>
          </div>
          <div v-for="(sub, index) in datas.subscribers" :key="index" class="users-info">
            <span class="uesrs-info-icon"><i class="far fa-user"></i></span>
            <span class="users-info-name">{{JSON.parse(sub.stream.connection.data).clientData.username}}</span>
          </div>
        </div>

        <div v-if="activeChat" class="input-box">
          <div class="input-form">
            <input v-model="sendMsg" type="text" id="sendMsg" class="input-text" @keyup.enter="sendMessage">
            <i class="send-icon fas fa-paper-plane" @click="sendMessage"></i>
          </div>
        </div>
      </div>
      <div class="side-nav">
        <!-- 참여자 목록 Btn -->
        <div class="users-tab mt-5" :class="{active: activeUsers, 'tab-select' : activeUsers}" @click="activeChange(1)">
          <i class="fas fa-user-friends" ></i>
          <span class="users-cnt">{{datas.participants}}</span>
        </div>
        <!-- 채팅창 Btn -->
        <div class="messenger-tab" :class="{active: activeChat, 'tab-select' : activeChat}" @click="activeChange(2)">
          <i class="far fa-comments"></i>
          <span v-if="newChat" class="new-chat">●</span>
        </div>
        <!-- 설정 Btn -->
        <div class="setting-tab" @click="activeSetting">
          <i class="fas fa-user-cog"></i>
        </div>
      </div>
    </div>
    
  </div>
</template>

<script>
export default {
  name: "Chat",
  props: {
    data: Object,
  },
  created() {
    this.user = this.$store.state.userStore;
    this.datas = this.data;
  },
  computed: {
    newChat: function() {
      if(this.activeChat) {
        this.inactiveBell();
        return false;
      }
      return this.datas.MessageBell;
    },
  },
  data() {
    return {
      datas: {},
      sendMsg: "",
      activeChat: false,
      activeUsers: false,
      user: {},
    }
  },
  methods: {
    sendMessage() {
      var nowDate = new Date();
      var time = nowDate.getHours() + ":" + nowDate.getMinutes();
      if(this.sendMsg.length > 0) {
        this.$emit('send', this.sendMsg, time);
        this.sendMsg = '';
      }
    },
    activeChange(type) {
      if(type == 1) { // 참여자 목록
        this.activeChat = false;
        this.activeUsers = !this.activeUsers;
      } else if (type == 2) { // 채팅탭
        this.activeChat = !this.activeChat;
        this.activeUsers = false;
      } else {
        this.activeChat = false;
        this.activeUsers = false;
      }
    },
    inactiveBell() {
      this.datas.MessageBell = false;
    },
    activeSetting() {
      this.$emit('settingModal', 1);
    },
  },
}
</script>

<style lang="scss" scoped>
@import "../../styles/Chat.scss";
</style>