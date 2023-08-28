namespace bcw_2023summer_keepr.Models;

public class Account : Profile
{
  public string Email { get; set; }  
}

public class Profile : RepoItem<string>
{
  public string Name { get; set; }
  public string Picture { get; set; }
  public string CoverImg { get; set; }
}
