using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Dtos.Categories;

namespace BlogUNAH.API.Dtos.Posts
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string AuthorId { get; set; }
        
        // Cambiar AuthorId por UserDto
        
        public DateTime PublicationDate { get; set; }

        public virtual CategoryDto Category { get; set; }

        public string Content { get; set; }

        public List<string> Tags { get; set; }
    }
}
