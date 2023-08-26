export class Keep {
	constructor(data) {
		this.id = data.id;
		this.createdAt = data.createdAt;
		this.updatedAt = data.updatedAt;
		this.creatorId = data.creatorId;
		this.name = data.name;
		this.description = data.description;
		this.img = data.img;
		this.views = data.views;
		this.kept = data.kept;
		this.creator = data.creator;
		this.vaultkeepid = data.vaultkeepid;
	}
}