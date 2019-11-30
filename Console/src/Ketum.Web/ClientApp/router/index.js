import Vue from 'vue'
import VueRouter from 'vue-router'
import { routes } from '@/router/routes'

Vue.use(VueRouter)

let router = new VueRouter({
  mode: 'history',
  routes
})

export default router
