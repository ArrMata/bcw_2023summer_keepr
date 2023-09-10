namespace bcw_2023summer_keepr.Services;

public class AccountService
{
	private readonly AccountsRepository _repo;

	public AccountService(AccountsRepository repo)
	{
		_repo = repo;
	}

	internal Account GetProfileByEmail(string email)
	{
		return _repo.GetByEmail(email);
	}

	internal Account GetOrCreateProfile(Account userInfo)
	{
		Account profile = _repo.GetById(userInfo.Id);
		if (profile == null)
		{
			_repo.Create(userInfo);
			profile = _repo.GetById(userInfo.Id);
		}
		return profile;
	}

	internal Profile GetProfileById(string userId)
	{
		Profile profile = _repo.GetProfileById(userId);
		if (profile == null)
		{
			throw new Exception("Profile with id not found");
		}
		return profile;
	}

	internal Account Edit(Account editData, string userEmail)
	{
		Account original = GetProfileByEmail(userEmail);
		original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
		original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
		original.CoverImg = editData.CoverImg.Length > 0 ? editData.CoverImg : original.CoverImg;
		return _repo.Edit(original);
	}
}
