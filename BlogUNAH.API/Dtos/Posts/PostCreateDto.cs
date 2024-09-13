using BlogUNAH.API.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogUNAH.API.Dtos.Posts
{
    public class PostCreateDto
    {
        [Display(Name = "Titulo")]
        [StringLength(100, ErrorMessage = "El {0} debe tener menos de {1}.")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Title { get; set; }

        [Display(Name = "Resumen")]
        [StringLength(250, ErrorMessage = "El {0} no puede tener mas de {1} caracteres.")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string Overview { get; set; }

        // TODO: Definir la relación entre usuario y post
        [Display(Name = "Autor")]
        [StringLength(100, ErrorMessage = "El {0} no puede tener mas de {1}.")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        public string AuthorId { get; set; }

        public DateTime PublicationDate { get; set; }

        public Guid CategoryId { get; set; }

        public string Content { get; set; }

        public virtual List<string> TagList { get; set; }
    }
}
