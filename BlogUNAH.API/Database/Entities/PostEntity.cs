using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities
{
    [Table("posts", Schema = "dbo")]
    public class PostEntity : BaseEntity
    {
        [StringLength(100)]
        [Required]
        [Column("title")]
        public string Title { get; set; }

        // TODO: Definir la relación entre usuario y post
        [StringLength(100)]
        [Column("author_id")]
        public string AuthorId { get; set; }

        [Column("publication_date")]
        public DateTime PublicationDate { get; set; }

        [Column("category_id")]
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual CategoryEntity Category { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("overview")]
        [StringLength(250)]
        [Required]
        public string Overview { get; set; }

        public virtual IEnumerable<PostTagEntity> Tags { get; set; }
        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
