import { AppState } from "../AppState";
import { Vault } from "../models/Vault";
import { api } from "./AxiosService";

class VaultsService {

	async createVault(vaultData, route) {
		const uid = AppState.account.id;
		const res = await api.post('api/vaults', vaultData);
		AppState.accountVaults.push(new Vault(res.data));
		if (route.name === 'Profile' && route.params.userId === uid) {
			AppState.vaults.push(new Vault(res.data));
		}
	}

	async getAccountVaults() {
		const res = await api.get('account/vaults');
		AppState.accountVaults = res.data.map(v => new Vault(v));
	}

	async getVaultsByUid(uid) {
		const res = await api.get(`api/profiles/${uid}/vaults`);
		AppState.vaults = res.data.map(v => new Vault(v));
	}

	async getVaultById(vaultId) {
		const res = await api.get(`api/vaults/${vaultId}`);
		AppState.activeVault = new Vault(res.data);
	}

	async deleteVault() {
		const vaultId = AppState.activeVault.id;
		await api.delete(`api/vaults/${vaultId}`);
		AppState.accountVaults = AppState.accountVaults.filter(vault => vault.id != vaultId);
	}

	clearAllVaults() {
		AppState.vaults = [];
	}

	clearActiveVault() {
		AppState.activeVault = {};
	}
}

export const vaultsService = new VaultsService();