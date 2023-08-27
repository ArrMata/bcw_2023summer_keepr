<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-md-10 col-12 mx-auto py-4 px-md-0 px-3">
        <div class="masonry-layout">
          <div v-for="k in keeps" :key="k.id" class="keep-card" :style="{backgroundImage:`linear-gradient(0deg, rgba(0,0,0,0.65) 0%, rgba(0,0,0,0) 25%), url(${k.img})`}">
            <p>{{ k.name }}</p>
            <img class="d-md-block d-none profile-picture" :src="k.creator.picture" :alt="k.creator.name" :title="k.creator.name">
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, onUnmounted, onUpdated } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';

export default {
  setup() {
    
    function randomizeHeights(){
      const cards = document.getElementsByClassName('keep-card');
      if(cards.length < 0){
        return
      }
      for(let i = 0; i < cards.length; i++){
        if (isNaN(parseInt(cards[i].style.height))){
          cards[i].style.height = `${Math.floor((Math.random() * 500) + 200)}px`;
        }
      }
    }

    // NOTE Masonry Functions / Grid Approach from Andy Barefoot https://medium.com/@andybarefoot/a-masonry-style-layout-using-css-grid-8c663d355ebb

    function resizeAllGridItems() {
      const cards = document.getElementsByClassName('keep-card');
      if(cards.length > 0){
        for(let i = 0; i < cards.length; i++){
          resizeGridItem(cards[i])
        }
      }
    }

    function resizeGridItem(item){
      const grid = document.getElementsByClassName("masonry-layout")[0];
      const rowHeight = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-auto-rows'));
      const rowGap = parseInt(window.getComputedStyle(grid).getPropertyValue('row-gap'));
      const rowSpan = Math.ceil((item.getBoundingClientRect().height+rowGap)/(rowHeight+rowGap));
      item.style.gridRowEnd = "span "+rowSpan;
    }

    const getAllKeeps = async() => {
      keepsService.getAllKeeps();
    };

    const clearAllKeeps = async() => {
      keepsService.clearAllKeeps()
    };

    onMounted(() => getAllKeeps())
    onUnmounted(() => clearAllKeeps())
    onUpdated(() => {
      randomizeHeights()
      resizeAllGridItems()
    })

    return {
      keeps: computed(() => AppState.keeps),
    };
  }
};
</script>

<style scoped lang="scss">

.keep-card {
  display: flex;
  align-items: end;
  justify-content: space-between;
  width: 100%;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  break-inside: avoid;  
  border-radius: .5rem;
  padding: 1rem .75rem;
  box-shadow: 0px 7px 10px rgba(0, 0, 0, 0.35);

  p {
    text-overflow: ellipsis;
    font-size: 1.5rem;
    color: white;
    margin-bottom: 0;
  }
}

.profile-picture {
  height: 3rem;
  width: 3rem;
  border-radius: 50%;
}

.masonry-layout {
  display: grid;
  row-gap: 1rem;
  column-gap: 2.5rem;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  grid-auto-rows: 3px;
  width: 100%;
  @media (max-width: 767px) {
    column-gap: 2rem;
    grid-template-columns: repeat(2, 1fr);
  }
}

</style>
