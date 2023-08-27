import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class KeepsService {
	async getAllKeeps() {
		const res = await api.get('api/keeps');
		AppState.keeps = res.data.map(k => new Keep(k));
	}

	async clearAllKeeps() {
		AppState.keeps = []
	}

	async createKeep(keepData) {
		const res = await api.post('api/keeps', keepData);
		AppState.keeps.push(new Keep(res.data));
	}

}

export const keepsService = new KeepsService();