<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-md-10 col-12 mx-auto py-4 px-md-0 px-3">
        <div class="masonry-layout">
          <KeepCard v-for="k in keeps" :key="k.id" :keep="k"/>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, onUpdated, onBeforeUnmount } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import KeepCard from '../components/KeepCard.vue';

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
      try {
        keepsService.getAllKeeps();
      } catch (error) {
        Pop.error(error.message);
        logger.error(error);
      }
    };
    
    const clearAllKeeps = async() => {
      try {
        keepsService.clearAllKeeps()
      } catch (error) {
        Pop.error(error.message);
        logger.error(error);
      }
    };

    onMounted(async() => await getAllKeeps());
    onBeforeUnmount(() => clearAllKeeps());
    onUpdated(() => {
      randomizeHeights()
      resizeAllGridItems()
    });

    return {
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard },
};
</script>

<style scoped lang="scss">

.masonry-layout {
  display: grid;
  row-gap: 1rem;
  column-gap: 2.5rem;
  grid-template-columns: repeat(4, minmax(200px, 1fr));
  grid-auto-rows: 3px;
  width: 100%;

  @media (max-width: 1024px) {
    grid-template-columns: repeat(3, minmax(150px, 1fr));
  }

  @media (max-width: 767px) {
    column-gap: 2rem;
    grid-template-columns: repeat(2, 1fr);
  }
}

</style>
