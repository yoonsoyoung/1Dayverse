//import axios from 'axios';

const camStore = {
  namespaced: true,
  state: {
    video: undefined,
    audio: undefined,
  },
  getters: {
    getVideo(state) {
      return state.video;
    },
    getAudio(state) {
      return state.audio;
    },
  },
  mutations: {
    setVideo(state, selected) {
      state.video = selected;
    },
    setAudio(state, selected) {
      state.audio = selected;
    },

  }
};

export default camStore