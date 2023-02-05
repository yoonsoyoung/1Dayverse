import { createStore } from 'vuex'
import userStore from '././modules/userStore'
import classStore from './modules/lectureStore'
import camStore from './modules/camStore'

import createPersistedState from "vuex-persistedstate";

export default createStore({
  plugins: [createPersistedState({
      paths: ['userStore', "lectureStore", 'camStore'],
    })
  ],
  state: {
  },
  mutations: {
  },
  actions: {
    
  },
  modules: {
    userStore,
    classStore,
    camStore,
  }
})
