namespace bcw_2023summer_keepr.Models
{
    public class Keep : RepoItem<int>
    {
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public int Kept { get; set; }
        public Profile Creator { get; set; }
    }

    public class KeepRelationship : Keep
    {
        public int VaultKeepId { get; set; }
    }
}