import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultKeepsService {
	async saveToVault(vaultId) {
		const activeKeep = AppState.activeKeep;
		const res = await api.post('api/vaultkeeps', { vaultId, keepId: activeKeep.id });
	}
}

export const vaultKeepsService = new VaultKeepsService();