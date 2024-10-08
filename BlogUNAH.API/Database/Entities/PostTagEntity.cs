using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities
{
    [Table("posts_tags", Schema = "dbo")]
    public class PostTagEntity : BaseEntity
    {
        [Column("post_id")]
        public Guid PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public virtual PostEntity Post { get; set; }

        [Column("tag_id")]
        public Guid TagId { get; set; }
        
        [ForeignKey(nameof(TagId))]
        public virtual TagEntity Tag { get; set; }
        public virtual IdentityUser CreatedByUser { get; set; }
        public virtual IdentityUser UpdatedByUser { get; set; }

    }
}
