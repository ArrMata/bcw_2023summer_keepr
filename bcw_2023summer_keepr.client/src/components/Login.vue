<template>
  <span class="navbar-text">
    <button class="btn btn-success selectable lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <!-- TODO Recolor Dropdowns/Dropups to match theme -->
      <!-- SECTION DESKTOP PROFILE PICTURE DROPDOWN -->
      <div class="d-none d-md-block dropdown my-2 my-lg-0">
        <div type="button" role="button" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" class="profile-picture" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-end p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link v-if="account.id" :to="{ name: 'Profile', params: { userId: account.id } }">
              <div class="list-group-item dropdown-item list-group-item-action">
                My Profile
              </div>
            </router-link>
            <router-link v-if="account.id" :to="{ name: 'EditAccount' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Edit Profile
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              Logout
            </div>
          </div>
        </div>
      </div>
      <!-- SECTION MOBILE PROFILE PICTURE DROPUP -->
      <div class="d-block d-md-none dropup my-2 my-lg-0">
        <div type="button" role="button" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" class="profile-picture" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-end p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link v-if="account.id" :to="{ name: 'Profile', params: { userId: account.id } }">
              <div class="list-group-item dropdown-item list-group-item-action">
                My Profile
              </div>
            </router-link>
            <router-link v-if="account.id" :to="{ name: 'EditAccount' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Edit Profile
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              Logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.profile-picture {
  border-radius: 50%;
  width: 2.75rem;
  object-fit: cover;
  object-position: center;
  aspect-ratio: 1/1;
}
</style>
