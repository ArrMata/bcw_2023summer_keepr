<template>
	<!-- Modal Body -->
	<!-- if you want to close by clicking outside the modal, delete the last endpoint:data-bs-backdrop and data-bs-keyboard -->
	<div class="modal fade" id="createKeepModal" tabindex="-1" data-bs-keyboard="false" role="dialog" aria-labelledby="modalTitleId" aria-hidden="true">
		<div class="modal-dialog modal-dialog-scrollable modal-dialog-centered" role="document">
			<div class="modal-content">
				<div class="modal-body">
					<div class="d-flex justify-content-end">
						<button id="keep-close" type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
					</div>
					<p class="mb-0 oxygen fw-bold fs-2">Add your keep</p>
					<form @submit.prevent="createKeep">
						<div class="form-floating mb-3">
							<input v-model="keepData.name" type="text" class="form-control inter" id="keepNameInput" placeholder="" minlength="5" maxlength="100" required>
							<label class="inter floating-label" for="keepNameInput">Name...</label>
						</div>
						<div class="form-floating mb-3">
							<input v-model="keepData.img" type="url" class="form-control inter" id="keepImageInput" placeholder="" minlength="25" maxlength="400" required>
							<label class="inter floating-label" for="keepImageInput">Image URL...</label>
						</div>
						<div class="form-floating mb-3">
							<textarea v-model="keepData.description" class="form-control inter" id="keepDescInput" placeholder="" minlength="5" maxlength="500"></textarea>
							<label class="inter floating-label" for="keepDescInput">Keep Description...</label>
						</div>
						<div class="d-flex justify-content-end">
							<button type="submit" class="create-button oxygen fw-bold">Create</button>
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
import { ref } from 'vue';
import { keepsService } from '../services/KeepsService';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';

export default {
	setup() {
		const keepData = ref({});

		return {
			keepData,
			async createKeep() {
				try {
					keepsService.createKeep(keepData.value);
					keepData.value = {};
					Modal.getOrCreateInstance('#createKeepModal').hide();
				} catch (error) {
					Pop.error(error.message);
					logger.error(error.message);
				}
			}
		}
	}
}
</script>

<style lang="scss" scoped>

#keep-close {
	opacity: 1;
}

p {
	color: #636e72;
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
}

form:invalid button[type="submit"] {
	background-color: #6e6e6e;
}

</style>