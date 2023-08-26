<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-10 mx-auto py-4 px-0">
        <div class="masonry-layout">
          <div v-for="k in keeps" :key="k.id" class="keep-card" :style="{backgroundImage: `url(${k.img})`}">
            <p>{{ k.name }}</p>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, onUnmounted } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';

export default {
  setup() {

    const getAllKeeps = async() => {
      keepsService.getAllKeeps();
    };

    const clearAllKeeps = async() => {
      keepsService.clearAllKeeps()
    };

    onMounted(() => getAllKeeps())
    onUnmounted(() => clearAllKeeps())

    return {
      keeps: computed(() => AppState.keeps),
    };
  }
};
</script>

<style scoped lang="scss">

.keep-card {
  position: relative;
  width: 100%;
  height: 250px;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  margin-bottom: .5rem;
  break-inside: avoid;  
}

.masonry-layout {
  display: grid;
  gap: 2rem;
  grid-template-columns: repeat(4, minmax(250px, 1fr));
}

</style>
