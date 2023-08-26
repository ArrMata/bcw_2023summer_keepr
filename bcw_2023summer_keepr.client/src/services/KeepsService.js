import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { api } from "./AxiosService";

class KeepsService {
	async getAllKeeps() {
		const res = await api.get('api/keeps');
		AppState.keeps = res.data.map(k => new Keep(k));
	}

	async clearAllKeeps() {
		AppState.keeps = []
	}

}

export const keepsService = new KeepsService();