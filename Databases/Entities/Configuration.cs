using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Databases.Entities
{
    public class Configuration : BaseEntity<Guid>
    {
        public required string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        public List<HotKey> HotKeys { get; set; } = [];
    }
}
