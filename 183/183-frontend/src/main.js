import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import router from './router'

Vue.config.productionTip = false
Vue.prototype.API_URL = 'https://security-backend-183.herokuapp.com/api/v1';

new Vue({
  vuetify,
  router,
  render: h => h(App)
}).$mount('#app')
