import { AppState } from "../AppState";
import { Vault } from "../models/Vault";
import { api } from "./AxiosService";

class VaultsService {

	async createVault(vaultData, route, userId) {
		const res = await api.post('api/vaults', vaultData);
		// TODO Add conditional pushing to AppState depending on route and userId
		AppState.accountVaults.push(new Vault(res.data));
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

	clearAllVaults() {
		AppState.vaults = [];
	}
}

export const vaultsService = new VaultsService();