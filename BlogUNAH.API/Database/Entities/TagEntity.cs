using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities
{
    [Table("tags", Schema = "dbo")]
    public class TagEntity : BaseEntity
    {
        [StringLength(50)]
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [StringLength(250)]
        [Column("description")]
        public string Description { get; set; }

        public virtual IEnumerable<PostTagEntity> Posts { get; set; }
        public virtual IdentityUser CreatedByUser { get; set; }
        public virtual IdentityUser UpdatedByUser { get; set; }
    }
}
