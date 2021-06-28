import Vue from 'vue';
import {BootstrapVue, IconsPlugin} from 'bootstrap-vue';
import VueRouter from 'vue-router';
import VueFriendlyIframe from 'vue-friendly-iframe';

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import router from './router';
import App from './App.vue';

import LoadScript from 'vue-plugin-load-script';


Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use(VueFriendlyIframe);
Vue.use(VueRouter);
Vue.use(LoadScript);


Vue.config.productionTip = false;

new Vue({
  router,
  render: (h) => h(App),
}).$mount('#app');
