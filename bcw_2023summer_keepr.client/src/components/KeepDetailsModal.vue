<template>
	<div class="modal fade" id="keepDetailsModal" tabindex="-1" role="dialog" aria-labelledby="modalTitleId" aria-hidden="true">
			<div class="modal-dialog modal-xl modal-fullscreen-md-down" role="document">
				<div class="modal-content">
					<div v-if="activeKeep" class="modal-body p-0">
						<div class="container-fluid h-100">
							<section class="row">
								<div class="col-md-6 col-12 p-0 h-100">
									<img class="keep-image" :src="activeKeep.img" :alt="activeKeep.name">
								</div>
								<div class="col-md-6 col-12 py-2 h-100">
									<div class="keep-details-section">
										<div class="top-section">
											<div class="d-flex align-items-center pt-1 w-100">
												<div v-if="account?.id === activeKeep.creatorId" class="dropdown">
													<i class="mdi mdi-dots-horizontal fs-2" title="Click to expand options" type="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></i>
													<div class="dropdown-menu" aria-labelledby="triggerId">
														<p @click="deleteKeep" role="button" title="Delete this keep" class="dropdown-item d-flex align-items-center mb-0 oxygen fw-bold" href="#">
															<i class="mdi mdi-trash-can fs-5 me-1"></i>
															Delete Keep
														</p>
													</div>
												</div>
												<button type="button" class="btn-close ms-auto" data-bs-dismiss="modal" aria-label="Close"></button>
											</div>
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
												<div v-if="account.id">
													<button @click="removeKeepVault" v-if="activeVault?.creatorId == account.id" class="remove-button oxygen fw-bold fs-5" title="Remove Keep from Vault">
														<i class="mdi mdi-cancel"></i> 
														Remove
													</button>	
													<SaveToVaultSelector v-else/>
												</div>
												<RouterLink class="d-flex ms-auto align-items-center profile-name" @click="routeToProfile" :title="`Go to ${activeKeep.creator.name}'s Profile!`" :to="{name: 'Profile', params: { userId: activeKeep.creatorId }}">
													<div>
														<img class="profile-picture me-2" :src="activeKeep.creator.picture" :alt="activeKeep.creator.name">
														<span class="oxygen fw-bold ">{{ activeKeep.creator.name }}</span>
													</div>
												</RouterLink>
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
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { keepsService } from '../services/KeepsService';
import { Modal } from 'bootstrap';
import SaveToVaultSelector from './SaveToVaultSelector.vue';
import { vaultKeepsService } from '../services/VaultKeepsService';

export default {
    setup() {
        const activeKeep = computed(() => AppState.activeKeep);
        function clearActiveKeep() {
            try {
              keepsService.clearActiveKeep();
            }
            catch (error) {
              Pop.error(error.message);
              logger.error(error);
            }
        }
        onMounted(() => {
            const modalElem = document.getElementById('keepDetailsModal');
            modalElem.addEventListener('hidden.bs.modal', () => {
              clearActiveKeep();
            });
        });

        return {
            activeKeep,
						activeVault: computed(() => AppState.activeVault),
            accountVaults: computed(() => AppState.accountVaults),
            account: computed(() => AppState.account),
            routeToProfile() {
							try {
								Modal.getOrCreateInstance('#keepDetailsModal').hide();
							}
							catch (error) {
								Pop.error(error.message);
								logger.error(error);
							}
            },
						async removeKeepVault() {
							try {
								const wantsToDelete = await Pop.confirm('Are you sure you want to delete this keep from your vault?', '', 'Yes, delete this keep.', 'warning'); 
								if (wantsToDelete) {
									await vaultKeepsService.removeKeepVault();
									Modal.getOrCreateInstance('#keepDetailsModal').hide();
								}
							} catch (error) {
								Pop.error(error.message);
								logger.error(error);
							}
						},
						async deleteKeep() {
							try {
								const wantsToDelete = await Pop.confirm('Are you sure you want to delete this keep?', '', 'Yes, delete this keep.', 'warning');
								if (wantsToDelete) {
									await keepsService.deleteKeep();
									Modal.getOrCreateInstance('#keepDetailsModal').hide();
								}
							} catch (error) {
								Pop.error(error.message);
								logger.error(error);
							}
						}
        };
    },
    components: { SaveToVaultSelector }
}
</script>

<style lang="scss" scoped>

@mixin inner-flexbox {
	flex: 1;
	display: flex;
	justify-content: center;
}	

a {
	color: #212529;
}

section {
	height: 75%;
	@media (min-width: 768px) {
		height: 80dvh;
	}
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
		border-top-left-radius: .25rem;
		border-bottom-left-radius: .25rem;
	}
}

.profile-picture {
	height: 2.5rem;
	width: 2.5rem;
	border-radius: 50%;
	@media (min-width:768px) {
		height: 3.5rem;
		width: 3.5rem;
	}
}

.profile-name {
	white-space: nowrap;
}

.modal-body {
	background-color: #fef6f0;
	@media (min-width:768px) {
		border-radius: 1rem;
	}
}

.remove-button {
	border: none;
	background-color: transparent;
	border-bottom: 2px solid #877a8f;
	border-radius: .15rem;
	color: #877a8f;
	transition: all 0.25s ease;

	&:hover {
		color: white;
		border: none;
		background-color: red;
		border-radius: .15rem;
	}
}

</style>