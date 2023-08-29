<template>
	<div class="container-fluid">
		<section v-if="activeVault.id" class="row">
			<div class="col-md-5 col-12 mx-auto py-3">
				<div class="header-image" :style="{backgroundImage:`linear-gradient(0deg, rgba(0,0,0,0.65) 0%, rgba(0,0,0,0) 50%), url(${activeVault.img})`}">
					<div class="text-center">
						<p class="mb-0 fs-3">{{ activeVault.name }}</p>
						<p>By {{ activeVault.creator.name }}</p>
					</div>
				</div>
				<div class="d-flex justify-content-center">
					<div class="inner-flexbox"></div>
					<div class="inner-flexbox">
						<p class="oxygen fw-bold keep-count">{{ keeps.length }} Keeps</p>
					</div>
					<div class="inner-flexbox justify-content-end">
						<div v-if="account?.id === activeVault.creatorId" class="dropdown pe-2">
							<i class="mdi mdi-dots-horizontal fs-2" title="Click to expand options" type="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></i>
							<div class="dropdown-menu" aria-labelledby="triggerId">
								<p @click="deleteVault" role="button" title="Delete this vault" class="dropdown-item d-flex align-items-center mb-0 oxygen fw-bold" href="#">
									<i class="mdi mdi-trash-can fs-5 me-1"></i>
									Delete Vault
								</p>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-8 col-12 mx-auto">
				<section class="row">
						<div v-for="k in keeps" :key="k.id" class="col-md-3 col-6 px-0 my-2">
							<div role="button" @click="getActiveKeep(k.id, k.vaultKeepId)" class="card-style mx-2" :style="{backgroundImage:`linear-gradient(0deg, rgba(0,0,0,0.65) 0%, rgba(0,0,0,0) 50%), url(${k.img})`}">
								<p class="mb-0">{{ k.name }}</p>
							</div>
						</div>
					</section>
			</div>
		</section>
	</div>
</template>

<script>
import { useRoute, useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { computed, onBeforeUnmount, onMounted } from 'vue'
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService'
import { Modal } from 'bootstrap'

export default {
	setup() {
		
		const route = useRoute();
		const router = useRouter();

		async function getVaultDetails() {
			try {
				await vaultsService.getVaultById(route.params.vaultId);
				await keepsService.getKeepsByVaultId(route.params.vaultId);
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

		function clearAppState() {
			try {
				vaultsService.clearActiveVault();
				keepsService.clearAllKeeps();
			} catch (error) {
				Pop.error(error.message);
				logger.error(error);
			}
		}

		onMounted(() => getVaultDetails());
		onBeforeUnmount(() => clearAppState());

		return {
			account: computed(() => AppState.account),
			activeVault: computed(() => AppState.activeVault),
			keeps:computed(() => AppState.keeps),
			async getActiveKeep(keepId, vaultKeepId) {
				try {
					await keepsService.getActiveKeep(keepId, vaultKeepId);
					Modal.getOrCreateInstance('#keepDetailsModal').show()
				} catch (error) {
					Pop.error(error.message);
					logger.error(error);
				}
			},
			async deleteVault() {
				try {
					const wantsToDelete = await Pop.confirm('Are you sure you want to delete this vault?', '', 'Yes, delete this vault.', 'warning');
					if (wantsToDelete) {
						await vaultsService.deleteVault();
						router.push({ name: 'Home' });
					}
				} catch (error) {
					Pop.error(error.message);
					logger.error(error);
				}
			}	
		}
	}
}
</script>

<style lang="scss" scoped>

.inner-flexbox {
	flex: 1;
	display: flex;
	justify-content: center;
}

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

.keep-count {
	margin-top: 1rem;
	font-size: 1.25rem;
	background-color: #ded6e9;
	padding: .25rem 1rem;
	width: fit-content;
	border-radius: 1rem;
	color: #2d3436;
}

.card-style {
	aspect-ratio: 1/1;
	background-position: center;
	background-repeat: no-repeat;
	background-size: cover;
	border-radius: 1rem;
	padding: 0.25rem .5rem;
	display: flex;
	flex-direction: column;
	justify-content: end;
	color: white;
	font-size: 1.5rem;
	box-shadow: 0px 7px 10px rgba(0, 0, 0, 0.35);
	transition: all 0.15s ease;
	&:hover {
		filter: brightness(1.25);
		transform: translateY(-10px);
	}
}

</style>