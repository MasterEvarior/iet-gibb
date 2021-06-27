import Vue from 'vue'
import VueRouter from 'vue-router'
import Command from '../views/Command.vue'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'
import About from '../views/About.vue'
import { authGuard } from '../auth';

Vue.use(VueRouter)

const routes = [
  {
    path: '/command',
    name: 'Command',
    component: Command,
    beforeEnter: authGuard,
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
  },
  {
    path: '/about',
    name: 'About',
    component: About,
  },
  {
    path: '*',
    name: 'Default',
    component: About,
  }
]

const router = new VueRouter({
  routes
})

export default router
