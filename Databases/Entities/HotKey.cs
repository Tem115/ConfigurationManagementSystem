namespace Databases.Entities
{
    public class HotKey : BaseEntity<Guid>
    {
        public Guid ConfigurationId { get; set; }

        public required string Hex { get; set; }

        public Guid CommandId { get; set; }

        public Command Command { get; set; } = null!;
    }
}