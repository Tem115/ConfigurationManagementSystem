namespace Databases.Entities
{
    public class Command : BaseEntity<Guid>
    {
        public required string Name { get; set; }
    }
}