using System.ComponentModel.DataAnnotations.Schema;

namespace Databases.Entities
{
    public class Version : BaseEntity<Guid>
    {
        public required Guid ConfigurationId {  get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UpdateDate { get; set; }
    }
}
