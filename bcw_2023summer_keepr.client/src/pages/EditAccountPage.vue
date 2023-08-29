<template>
  <div class="main-section oxygen fw-bold text-center">
    <div v-if="account.id" class="row m-0">
      <div class="col-md-10 col-12 py-4 mt-4 mx-auto">
        <h1 class="mb-4 fw-bold">Edit your account</h1>
        <div class="row">
          <div class="col-12 p-3 mb-3 position-relative">
            <img class="img-fluid cover-img" :src="account.coverImg" alt="Cover Image">
            <img class="avatar" :src="account.picture" :alt="account.name">
          </div>
          <div class="col-12 mt-5">
            <form class="d-block d-md-flex" id="accountForm" @submit.prevent="editAccount">
							<div class="flex-grow-1">
								<label for="name" class="form-label">Name</label>
								<input required minlength="3" maxlength="40" id="name" v-model="editable.name" class="form-control" type="text">
							</div>
							<div class="flex-grow-1 my-3 my-md-0 mx-md-4">
								<label for="profilePicture" class="form-label">Profile Picture URL</label>
								<input type="url" min="50" maxlength="300" id="profilePicture" v-model="editable.picture" class="form-control">
							</div>
							<div class="flex-grow-1">
								<label for="coverImg" class="form-label">Cover Image URL</label>
								<input type="url" min="50" maxlength="300" id="coverImg" v-model="editable.coverImg" class="form-control">
							</div>
            </form>
            <div class="d-flex justify-content-end mt-3">
              <button class="save-button" form="accountForm" type="submit">Submit Changes <i class="ms-1 mdi mdi-content-save"></i></button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, ref, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { accountService } from '../services/AccountService';

export default {
	setup() {
		const account = computed(() => AppState.account);
		const editable = ref({});

		watchEffect(() => {
			editable.value = {...account.value}
		})

		return {
			account,
			editable,
			async editAccount() {
				try {
					await accountService.editAccount(editable.value);
					Pop.toast('Account Edited!', 'success', 'bottom-end');
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

label {
	display: block;
}

.save-button {
	border: none;
	background-color: #6b6171;
	padding: 0.25rem 0.5rem;
	color: white;
	font-size: 1.2rem;
	display: flex;
	align-items: center;
	transition: all 0.15s ease;
	border-radius: 0.5rem;
	&:hover {
		filter: brightness(1.2);
		transform: translateY(-3px);
	}
}

.cover-img {
	height: 15rem;
	width: 100%;
	object-fit: cover;
	object-position:center;
	border-radius: 1rem;
}

.avatar {
	height: 10rem;
	width: 10rem;
	border-radius: 50%;
	object-fit: cover;
	object-position: center;
	position: absolute;
	border: 2px solid rgb(255, 255, 255);
	box-shadow: 0px 5px 10px rgba(0, 0, 0, 0.699);
	bottom: -15%;
	left: 5%;
}

</style>