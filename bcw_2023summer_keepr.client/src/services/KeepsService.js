import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { api } from "./AxiosService";

class KeepsService {
	async getAllKeeps() {
		const res = await api.get('api/keeps');
		AppState.keeps = res.data.map(k => new Keep(k));
	}

	clearAllKeeps() {
		AppState.keeps = []
	}

	clearActiveKeep() {
		AppState.activeKeep = null;
	}

	async createKeep(keepData, route) {
		const uid = AppState.account.id;
		const res = await api.post('api/keeps', keepData);
		if (route.name === 'Home') {
			AppState.keeps.push(new Keep(res.data));
			return
		}

		if (route.name === 'Profile' && route.params.userId === uid) {
			AppState.keeps.push(new Keep(res.data));
			return
		}
	}

	async getActiveKeep(keepId, vaultKeepId = null) {
		const res = await api.get(`api/keeps/${keepId}`);
		AppState.activeKeep = new Keep(res.data);
		if (vaultKeepId) {
			AppState.activeKeep.vaultKeepId = vaultKeepId;
		}
	}

	async getKeepsByUid(uid) {
		const res = await api.get(`api/profiles/${uid}/keeps`);
		AppState.keeps = res.data.map(k => new Keep(k));
	}

	async getKeepsByVaultId(vaultId) {
		const res = await api.get(`api/vaults/${vaultId}/keeps`)
		AppState.keeps = res.data.map(k => new Keep(k));
	}

	async deleteKeep() {
		const keepId = AppState.activeKeep.id;
		await api.delete(`api/keeps/${keepId}`);
		AppState.keeps = AppState.keeps.filter(k => k.id != keepId);
	}

}

export const keepsService = new KeepsService();