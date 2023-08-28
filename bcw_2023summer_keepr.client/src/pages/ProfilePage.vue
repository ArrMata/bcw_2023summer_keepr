<template>
	<div class="container-fluid">
		<section class="row">
			<div v-if="activeAccount.id" class="col-md-8 col-12 mx-auto py-4">
				<div class="position-relative mx-3">
					<img class="cover-image" :src="activeAccount.coverImg" :alt="activeAccount.name">
					<img class="profile-picture elevation-4" :src="activeAccount.picture" :alt="activeAccount.name">
				</div>
				<div class="text-center mt-5 pt-1 oxygen fw-bold fs-3">
					<p class="mb-0">{{ activeAccount.name }}</p>
					<p class="fs-5 fw-normal">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
				</div>

				<div>
					<p class="fw-bold oxygen fs-3">Vaults</p>
					<section class="row">
						<div v-for="v in vaults" :key="v.id" class="col-md-3 col-6 px-0">
							<RouterLink :to="{ name: 'Vault', params: { vaultId: v.id } }">
								<div role="button" class="card-style elevation-5 mx-2" :style="{backgroundImage:`linear-gradient(0deg, rgba(0,0,0,0.65) 0%, rgba(0,0,0,0) 50%), url(${v.img})`}">
									<i v-if="v.isPrivate" class="mdi mdi-lock mb-auto ms-auto fs-3" title="Private Vault"></i>
									<p class="mb-0">{{ v.name }}</p>
								</div>
							</RouterLink>
						</div>
					</section>
				</div>

				<div class="mt-5">
					<p class="fw-bold oxygen fs-3">Keeps</p>
					<section class="row">
						<div v-for="k in keeps" :key="k.id" class="col-md-3 col-6 px-0">
							<div role="button" @click="getActiveKeep(k.id)" class="card-style mx-2" :style="{backgroundImage:`linear-gradient(0deg, rgba(0,0,0,0.65) 0%, rgba(0,0,0,0) 50%), url(${k.img})`}">
								<p class="mb-0">{{ k.name }}</p>
							</div>
						</div>
					</section>
				</div>
			</div>
		</section>
	</div>
</template>

<script>
import { useRoute } from 'vue-router';
import { accountService } from '../services/AccountService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { computed, onMounted, onBeforeUnmount } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';
import { Modal } from 'bootstrap';

export default {
	setup() {
		const route = useRoute();
		async function getAccountDetails() {
			try {
					await accountService.getAccountById(route.params.userId);
					await keepsService.getKeepsByUid(route.params.userId);
					await vaultsService.getVaultsByUid(route.params.userId);
			}
			catch (error) {
					Pop.error(error.message);
					logger.error(error);
			}
		}
		function clearAppState() {
				try {
					keepsService.clearAllKeeps();
					vaultsService.clearAllVaults();
				}
				catch (error) {
					Pop.error(error.message);
					logger.error(error);
				}
		}
		onMounted(() => getAccountDetails());
		onBeforeUnmount(() => clearAppState());
		return {
			activeAccount: computed(() => AppState.activeAccount),
			keeps: computed(() => AppState.keeps),
			vaults: computed(() => AppState.vaults),
			async getActiveKeep(keepId) {
				try {
					await keepsService.getActiveKeep(keepId);
					Modal.getOrCreateInstance('#keepDetailsModal').show()
				} catch (error) {
					Pop.error(error.message);
					logger.error(error);
				}
			},						
		};
	},
}
</script>

<style lang="scss" scoped>

i {
	-webkit-text-stroke: 1px rgba(0, 0, 0, 0.601);
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

.cover-image {
	width: 100%;
	height: 35dvh;
	object-fit: cover;
	border-radius: 1rem;
}

.profile-picture {
	width: 6rem;
	height: 6rem;
	border-radius: 50%;
	position: absolute;
	bottom: -15%;
	left: 50%;
	transform: translateX(-50%);
	border: white solid 3px;
}

</style>