using System.ComponentModel.DataAnnotations;

namespace Databases.Entities
{
    public class BaseEntity<TId> where TId : struct
    {
        [Key]
        public TId Id { get; set; }
    }
}
