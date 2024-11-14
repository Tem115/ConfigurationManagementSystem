using System.ComponentModel.DataAnnotations.Schema;

namespace Databases.Entities
{
    public class User : BaseEntity<Guid>
    {
        public required string Login { get; set; }

        public required string Password { get; set; }
    }
}
