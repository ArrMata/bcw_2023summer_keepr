import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard, authSettled } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage')
  },
  {
    path: '/profile/:userId',
    name: 'Profile',
    component: loadPage('ProfilePage'),
    beforeEnter: authSettled,
  },
  {
    path: '/vault/:vaultId',
    name: 'Vault',
    component: loadPage('VaultPage'),
    beforeEnter: authSettled,
  }
];

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})
