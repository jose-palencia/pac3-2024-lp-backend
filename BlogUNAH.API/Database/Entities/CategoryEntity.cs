using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities
{
    [Table("categories", Schema = "dbo")]
    public class CategoryEntity : BaseEntity
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} de la categoría es requerido.")]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MinLength(10, ErrorMessage = "La {0} debe tener al menos {1} caracteres.")]
        [StringLength(250)]
        [Column("description")]
        public string Description { get; set; }

        public virtual IEnumerable<PostEntity> Posts { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
