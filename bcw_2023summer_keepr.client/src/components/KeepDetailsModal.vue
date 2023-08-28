<template>
	<div class="modal fade" id="keepDetailsModal" tabindex="-1" role="dialog" aria-labelledby="modalTitleId" aria-hidden="true">
			<div class="modal-dialog modal-xl modal-fullscreen-md-down" role="document">
				<div class="modal-content">
					<div v-if="activeKeep" class="modal-body p-0">
						<div class="container-fluid">
								<section class="row">
									<div class="col-md-6 col-12 p-0 h-100">
										<img class="keep-image" :src="activeKeep.img" :alt="activeKeep.name">
									</div>
									<div class="col-md-6 col-12 py-2 h-100">
										<div class="keep-details-section">
											<div class="top-section">
												<button type="button" class="btn-close ms-auto" data-bs-dismiss="modal" aria-label="Close"></button>
												<div class="d-flex py-3 mb-auto">
													<p class="mb-0 me-3 inter fs-5"><i title="View count" class="mdi mdi-eye-outline me-2 fs-4"></i>{{ activeKeep.views }}</p>
													<p class="mb-0 inter fs-5"><i title="Kept count" class="mdi mdi-alpha-k-box-outline me-2 fs-4"></i>{{ activeKeep.kept }}</p>
												</div>
											</div>
											<div class="center-section">
												<div class="w-100">
													<p class="fs-2 text-center">{{ activeKeep.name }}</p>
													<p class="inter fs-5 px-5">{{ activeKeep.description }}</p>
												</div>
											</div>
											<div class="bottom-section">
												<div class="d-flex align-items-center w-100 px-2">
													<form v-if="account.id" class="d-flex" @submit.prevent="saveToVault">
														<select v-model="selected" required class="form-select form-select-md">
															<option value="">SAVE KEEP</option>
															<option v-for="v in accountVaults" :key="v.id" :value="v.id">{{ v.name }}</option>
														</select>
														<button class="save-button oxygen fw-bold ms-2">save</button>
													</form>
													<div @click="routeToProfile" role="button" class="d-flex ms-auto align-items-center">
														<img class="profile-picture me-2" :src="activeKeep.creator.picture" :alt="activeKeep.creator.name">
														<span class="oxygen fw-bold">{{ activeKeep.creator.name }}</span>
													</div>
												</div>
											</div>
										</div>
									</div>
								</section>
						</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { keepsService } from '../services/KeepsService';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { useRouter } from 'vue-router';
import { Modal } from 'bootstrap';

export default {
	setup() {

		const selected = ref("");
		const router = useRouter();
		const activeKeep = computed(() => AppState.activeKeep);
		
		function clearActiveKeep() {
			try {
				keepsService.clearActiveKeep();
			} catch (error) {
				Pop.error(error.message);
				logger.error(error);
			}
		}

		onMounted(() => {
			const modalElem = document.getElementById('keepDetailsModal');
			modalElem.addEventListener('hidden.bs.modal', () => {
				clearActiveKeep();
			})
		})

		return {
			activeKeep,
			accountVaults: computed(() => AppState.accountVaults),
			account: computed(() => AppState.account),
			async saveToVault() {
				try {
					await vaultKeepsService.saveToVault(selected.value);
					selected.value = "";
					Pop.toast('Saved to vault!', 'success', 'bottom-end');
				} catch (error) {
					Pop.error(error.message);
					logger.error(error);
				}
			},
			routeToProfile() {
				try {
					router.push({name: 'Profile', params: { userId: activeKeep.value.creatorId }});
					Modal.getOrCreateInstance('#keepDetailsModal').hide();
				} catch (error) {
					Pop.error(error.message);
					logger.error(error)
				}
			},
			selected,
		}
	}
}
</script>

<style lang="scss" scoped>

@mixin inner-flexbox {
	flex: 1;
	display: flex;
	justify-content: center;
}

section {
	@media (min-width: 769px) {
		height: 80dvh;
	}
}

option, select {
	font-family: 'Oxygen', sans-serif;
	font-weight: bold;
}

select {
	max-width: 55%;
}

.keep-details-section {
	display: flex;
	flex-direction: column;
	height: 100%;
}

.center-section {
	@include inner-flexbox();
}

.bottom-section {
	@include inner-flexbox();
	justify-content: end;
	align-items: end;
}

.top-section {
	@include inner-flexbox();
	flex-direction: column;
	align-items: center;
}

.keep-image {
	width: 100%;
	height: 100%;
	object-fit: cover;
	@media (min-width:768px) {
		border-top-left-radius: .5rem;
		border-bottom-left-radius: .5rem;
	}
}

.profile-picture {
	height: 3.5rem;
	width: 3.5rem;
	border-radius: 50%;
}

.modal-body {
	border-radius: .5rem;
	background-color: #fef6f0;
}

.save-button {
  background-color: #877a8f;
  color: #fef6f0;
  cursor: pointer;
  padding: 0.25rem .8rem;
  border: none;
  border-radius: .75rem;
}

</style>