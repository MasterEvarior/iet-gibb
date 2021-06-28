import Vue from 'vue';
import Router from 'vue-router';
import Home from './components/Home.vue';
import Profile from './components/Profile.vue';
import Portfolio from './components/Portfolio.vue';
import EmbededPdf from './components/EmbededPdf.vue';

import VideoPage from './components/VideoPage.vue'
Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/profile/:name',
      component: Profile
    },
    {
      path: '/video',
      component: VideoPage
    },
    {
      path: '*', component: Home
    },
    {
      path: '/portfolios/:name',
      component: Portfolio
    },
    {
      path: '/assets/pdfs/:pdf',
      component: EmbededPdf
    }
  ]
});