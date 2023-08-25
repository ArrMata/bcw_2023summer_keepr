namespace bcw_2023summer_keepr.Models
{
    public abstract class RepoItem<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}