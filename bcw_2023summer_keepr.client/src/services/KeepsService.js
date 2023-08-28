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

	async createKeep(keepData) {
		const res = await api.post('api/keeps', keepData);
		// TODO Add conditional pushing to AppState depending on route and userId
		AppState.keeps.push(new Keep(res.data));
	}

	async getActiveKeep(keepId) {
		const res = await api.get(`api/keeps/${keepId}`);
		AppState.activeKeep = new Keep(res.data);
	}

	async getKeepsByUid(uid) {
		const res = await api.get(`api/profiles/${uid}/keeps`);
		AppState.keeps = res.data.map(k => new Keep(k));
	}

}

export const keepsService = new KeepsService();