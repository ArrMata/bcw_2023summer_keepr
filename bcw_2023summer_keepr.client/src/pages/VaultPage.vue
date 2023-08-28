<template>
	<div class="container-fluid">
		<section v-if="activeVault.id" class="row">
			<div class="col-md-4 col-6 mx-auto py-3">
				<div class="header-image" :style="{backgroundImage:`linear-gradient(0deg, rgba(0,0,0,0.65) 0%, rgba(0,0,0,0) 50%), url(${activeVault.img})`}">
					<div class="text-center">
						<p class="mb-0 fs-3">{{ activeVault.name }}</p>
						<p>By {{ activeVault.creator.name }}</p>
					</div>
				</div>
			</div>
		</section>
	</div>
</template>

<script>
import { useRoute, useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState';

export default {
	setup() {
		
		const route = useRoute();
		const router = useRouter();

		async function getVaultDetails() {
			try {
				await vaultsService.getVaultById(route.params.vaultId);
			} catch (error) {
				if(error.response.data === 'No vault found!') {
					Pop.error('Sorry! That vault does not exist.');
					router.push({ name: 'Home' });
					return;
				}
				Pop.error(error.message);
				logger.error(error);
			}
		}

		onMounted(() => getVaultDetails());

		return {
			activeVault: computed(() => AppState.activeVault),
		}
	}
}
</script>

<style lang="scss" scoped>

.header-image {
	width: 100%;
	height: 25dvh;
	background-position: center;
	background-size: cover;
	border-radius: 1rem;
	position: relative;
	display: flex;
	flex-direction: column;
	justify-content: end;
	align-items: center;
	color: white;
	
}

</style>