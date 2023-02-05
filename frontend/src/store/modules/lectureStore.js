// import axios from "axios"

const lectureStore = {
  namespaced: true,
  state: {
    lecture : null,

  },
  getters: {
    getThisLecture(state){
      return state.lecture
    }
  },
  mutations: {
    SET_LECTURE(state, Form){
      state.lecture= Form
    },
  },
  actions: {
    GET_LECTURE({commit}, Form){
      commit("SET_LECTURE", Form)
      console.log(Form)
    }
  }
}

export default lectureStore