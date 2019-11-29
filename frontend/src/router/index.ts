import Vue from 'vue'
import VueRouter from 'vue-router'
import Router from 'vue-router'
import ContactPage from '../views/ContactPage.vue'
import ContactList from '../views/ContactList.vue'

//Vue.use(VueRouter)
Vue.use(Router)

const routes = [
  {
    path: '/edit/:Cid',
    name: 'edit',
    component: () => import('../views/ContactPage.vue')
  },
  {
    path: '/add',
    name: 'add',
    component: () => import('../views/ContactPage.vue')
  },
  {
    path: '/',
    name: 'list',
    component: () => import('../views/ContactListPage.vue')
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  }
]

// const router = new VueRouter({
//   routes
// })
const router = new Router({
  routes
})

export default router
