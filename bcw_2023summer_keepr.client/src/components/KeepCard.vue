<template>
	<div @click="getActiveKeep" title="Show more details about this keep!" 
	role="button" class="keep-card" data-bs-toggle="modal" data-bs-target="#keepDetailsModal" 
	:style="{backgroundImage:`linear-gradient(0deg, rgba(0,0,0,0.65) 0%, rgba(0,0,0,0) 45%), url(${keep.img})`}">
		<p>{{ keep.name }}</p>
		<img class="ms-md-1 ms-0 d-md-block d-none profile-picture" :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name">
	</div>
</template>

<script>
import { Modal } from 'bootstrap';
import { Keep } from '../models/Keep';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
	props: {
		keep: {type: Keep, required: true}
	},
	setup(props) {
		
		return {
			async getActiveKeep() {
				try {
					await keepsService.getActiveKeep(props.keep.id);
					Modal.getOrCreateInstance('#keepDetailsModal').show()
				} catch (error) {
					Pop.error(error.message);
					logger.error(error);
				}
			},
		}
	}
}
</script>

<style lang="scss" scoped>

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
	transition: all 0.15s ease;

	&:hover {
		filter: brightness(1.25);
		transform: translateY(-10px);
	}

  p {
    white-space:normal;
    text-overflow: ellipsis;
    font-size: 1.5rem;
    color: white;
    margin-bottom: 0;
		@media (min-width:768px) {
			white-space: nowrap;
			overflow: hidden;
		}
  }
}

.profile-picture {
  width: 3rem;
	aspect-ratio: 1 / 1;
	object-fit: cover;
	object-position: center;
  border-radius: 50%;
}

</style>