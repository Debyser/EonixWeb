using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class EntityBase : IEntityBase
    {
        [Column("Id")]
        public long Id { get; set; }
    }
}
