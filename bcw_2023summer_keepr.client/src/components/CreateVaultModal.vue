<template>
	<!-- Modal Body -->
	<div class="modal fade" id="createVaultModal" tabindex="-1" data-bs-keyboard="false" role="dialog" aria-labelledby="createVaultModal" aria-hidden="true">
		<div class="modal-dialog modal-dialog-scrollable modal-dialog-centered" role="document">
			<div class="modal-content">
				<div class="modal-body">
					<div class="d-flex justify-content-end">
						<button id="vault-close" type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
					</div>
					<p class="mb-0 oxygen fw-bold fs-2">Add your vault</p>
					<form @submit.prevent="createVault">
						<div class="form-floating mb-3">
							<input v-model="vaultData.name" type="text" class="form-control inter" id="vaultNameInput" placeholder="" minlength="5" maxlength="40" required>
							<label class="inter floating-label" for="keepNameInput">Title...</label>
						</div>
						<div class="form-floating mb-3">
							<input v-model="vaultData.img" type="url" class="form-control inter" id="vaultImageInput" placeholder="" minlength="25" maxlength="400" required>
							<label class="inter floating-label" for="keepImageInput">Cover Image URL...</label>
						</div>
						<div class="d-flex justify-content-end mb-2">
							<div>
								<p class="mb-0 inter small-info">Private vaults can only be seen by you</p>
								<div class="form-check d-flex align-items-center">
									<input v-model="vaultData.isPrivate" class="form-check-input me-2" type="checkbox" id="vaultPrivate">
									<label class="form-check-label private-check-label oxygen fw-bold" for="vaultPrivate">Make Vault Private?</label>
								</div>
								<button type="submit" class="create-button oxygen fw-bold">Create Vault</button>
							</div>
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
import { ref } from 'vue';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { Modal } from 'bootstrap';
import { vaultsService } from '../services/VaultsService';
import { useRoute } from 'vue-router';

export default {
	setup() {
		const vaultData = ref({
			isPrivate : false,
		});
		const route = useRoute()
		
		return {
			vaultData,
			async createVault() {
				try {
					// FIXME await
					await vaultsService.createVault(vaultData.value, route);
					// FIXME set default
					vaultData.value = {
						isPrivate: false,
					};
					Modal.getOrCreateInstance('#createVaultModal').hide();
				} catch (error) {
					Pop.error(error.message);
					logger.error(error)
				}
			},
		}
	}
}
</script>

<style lang="scss" scoped>

form:invalid button[type="submit"] {
	background-color: #6e6e6e;
}

p {
	color: #636e72;
}

#vault-close {
	opacity: 1;
}

.modal-body {
	background-color: #f9f6fa;
}

.form-control {
	background-color: #f9f6fa;
	border: none;
	border-bottom: solid 2px #7a8186;
	border-radius: 3px;

	&:focus {
		background-color: #f9f6fa;
		box-shadow: none;
	} 
}

.floating-label {
	color: #7a8186;;
}

.create-button {
	border: none;
	border-radius: .35rem;
	background-color: #2d2d2d;
	color: white;
	font-size: 1rem;
	padding: .25rem .65rem;
	width: 100%;
}

.checkbox-section {
	width: 60%;
}

.small-info {
	font-size: .76rem;
}
.private-check-label {
	font-size: 1.3rem;
}


</style>