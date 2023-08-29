import { AppState } from "../AppState";
import { api } from "./AxiosService";

class VaultKeepsService {
	async saveToVault(vaultId) {
		const activeKeep = AppState.activeKeep;
		activeKeep.kept++;
		await api.post('api/vaultkeeps', { vaultId, keepId: activeKeep.id });
	}

	async removeKeepVault() {
		const keepVaultId = AppState.activeKeep.vaultKeepId;
		await api.delete(`api/vaultkeeps/${keepVaultId}`);
		AppState.keeps = AppState.keeps.filter(keep => keep.vaultKeepId != keepVaultId);
	}
}

export const vaultKeepsService = new VaultKeepsService();