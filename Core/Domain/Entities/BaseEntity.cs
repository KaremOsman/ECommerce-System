namespace Domain.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
