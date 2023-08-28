<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view /> 
  </main>
  <CreateKeepModal />
  <CreateVaultModal />
  <KeepDetailsModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import CreateKeepModal from './components/CreateKeepModal.vue'
import CreateVaultModal from './components/CreateVaultModal.vue'
import KeepDetailsModal from './components/KeepDetailsModal.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, CreateKeepModal, CreateVaultModal, KeepDetailsModal }
}
</script>

<style lang="scss">
@import "./assets/scss/main.scss";
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@300;400;700&family=Marko+One&family=Oxygen:wght@400;700&display=swap');

// SECTION CSS Font Families 
// font-family: 'Inter', sans-serif;
// font-family: 'Marko One', serif;
// font-family: 'Oxygen', sans-serif;

body {
  font-family: 'Marko One', serif;
  background-color: #f9f6fa;
  height: 100dvh;
}

header{
  height: 82px;
  @media (max-width: 767px) {
    height: 0;
  }
}

main {
  height: calc(100dvh - (82px + .5rem));
  background-color: #fef6f0;
  margin-top: 0.5rem;
  overflow-y: scroll;
  @media (max-width: 767px) {
    margin-top: 0;
    margin-bottom: 82px;
  }
}

.inter {
  font-family: 'Inter', sans-serif;
}

.marko-one {
  font-family: 'Marko One', serif;
}

.oxygen {
  font-family: 'Oxygen', sans-serif;
}

</style>
