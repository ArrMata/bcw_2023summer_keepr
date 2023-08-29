<template>
	<form class="d-flex" @submit.prevent="saveToVault">
		<select v-model="selected" required class="form-select form-select-md">
			<option value="">Select to a Vault</option>
			<option v-for="v in accountVaults" :key="v.id" :value="v.id">{{ v.name }}</option>
		</select>
		<button :disabled="selected === ''" class="save-button oxygen fw-bold ms-2">save</button>
	</form>
</template>

<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import { vaultKeepsService } from '../services/VaultKeepsService';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';

export default {
	setup() {
		const selected = ref("");

		return {
			account: computed(() => AppState.account),
			accountVaults: computed(() => AppState.accountVaults),
			selected,
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
		}
	}
}
</script>

<style lang="scss" scoped>

option, select {
	font-family: 'Oxygen', sans-serif;
	font-weight: bold;
}

select {

	max-width: 50%;

	@media (min-width:768px) {
		max-width: 75%;
	}
}

.save-button {
  background-color: #877a8f;
  color: #fef6f0;
  cursor: pointer;
  padding: 0.25rem .8rem;
  border: none;
  border-radius: .75rem;
	transition: all 0.25s ease;
	&:disabled:hover{
		cursor: default;
		filter: brightness(1);
	}
	&:hover{
		filter: brightness(1.15);
	}
}

</style>