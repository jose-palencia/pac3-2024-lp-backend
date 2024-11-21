using BlogUNAH.API.Dtos.Posts;

namespace BlogUNAH.API.Dtos.Dashboard
{
    public class DashboardDto
    {
        public int UsersCount { get; set; }
        public int TagsCount { get; set; }
        public int PostsCount { get; set; }
        public int CommentsCount { get; set; }

        public List<DashboardPostDto> Posts { get; set; }
        public List<DashboardCategoryDto> Categories { get; set; }
        public List<DashboardTagDto> Tags { get; set; }

    }
}
