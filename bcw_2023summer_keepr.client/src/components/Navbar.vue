<template>
  <nav class="navbar nav-position elevation-3">
    <div class="container-fluid px-3 px-md-5">
      <!-- SECTION DESKTOP DROPDOWN -->
      <ul class="navbar-nav d-none d-md-flex order-1 order-md-0">
        <li class="nav-item">
          <RouterLink :class="{selected: route.name == 'Home' }" :to="{ name : 'Home' }">Home</RouterLink>
        </li>
        <li class="nav-item dropdown">
          <p class="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown" aria-expanded="false">Create</p>
          <ul class="dropdown-menu position-absolute">
            <li><p class="dropdown-item" role="button">New Keep</p></li>
            <li><hr class="dropdown-divider"></li>
            <li><p class="dropdown-item" role="button">New Vault</p></li>
          </ul>
        </li>
      </ul>
      <!-- SECTION MOBILE DROPUP -->
      <ul class="navbar-nav d-md-none d-md-block order-1 order-md-0">
        <li class="nav-item dropup">
          <btn class="create-button dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">Create</btn>
          <ul class="dropdown-menu position-absolute">
            <li><p class="dropdown-item" role="button">New Keep</p></li>
            <li><hr class="dropdown-divider"></li>
            <li><p class="dropdown-item" role="button">New Vault</p></li>
          </ul>
        </li>
      </ul>
      <img class="order-0 order-md-1" src="../assets/img/Keepr logo.svg" alt="Keepr Logo"/>
      <Login class="order-2"/>
    </div>
  </nav>
</template>

<script>
import { RouterLink, useRoute } from 'vue-router';
import Login from './Login.vue';
export default {
  setup() {
    const route = useRoute();
    return {
      route,

    }
  },
  components: { Login, RouterLink }
};
</script>

<style scoped lang="scss">

nav {
  font-family: 'Oxygen', sans-serif;
  font-weight: bold;
  font-size: 1.35rem;
  background-color: #fef6f0;
}

a, .nav-link {
  padding: 0.15rem 1rem;
  color: #2d2d2d;
}

p {
  margin-bottom: 0;
}

hr {
  margin: 0.25rem 0.75rem;
  border-top: #2d2d2d solid 2px;
}

.nav-position {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  @media (max-width: 768px){
    top:auto;
    bottom: 0;
  }
}

.dropdown-menu {
  background-color: #ded6e9;
  outline:#2d2d2d solid 2px;
  padding: 0;
}

.dropdown-item {
  font-size: 1.3rem;
  font-weight: bold;
  padding: 0.1rem 1rem;
  margin: .5rem 0;
  cursor: pointer;
  transition: all ease-in-out 0.15s;
  &:hover {
    background-color: #bdb5c7;
  }
}

.create-button {
  background-color: #877a8f;
  color: #fef6f0;
  cursor: pointer;
  padding: 0.75rem;
  border-radius: 0.25rem;
}

.navbar-nav {
  flex-direction: row;
  align-items: center;
}

.selected {
  border-radius: .75rem;
  background-color: #e9d8d6;
}

</style>
