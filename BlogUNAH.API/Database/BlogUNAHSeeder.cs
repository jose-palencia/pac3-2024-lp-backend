using BlogUNAH.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BlogUNAH.API.Database
{
    public class BlogUNAHSeeder
    {
        public static async Task LoadDataAsync(
            BlogUNAHContext context,
            ILoggerFactory loggerFactory
            ) 
        {
            try
            {
                await LoadCategoriesAsync(loggerFactory, context);
                await LoadPostsAsync(loggerFactory, context);
                await LoadTagsAsync(loggerFactory, context);
                await LoadPostsTagsAsync(loggerFactory, context);
            }
            catch (Exception e) 
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error inicializando la data del API");
            }
        }

        // TODO: Seed de usuarios

        public static async Task LoadCategoriesAsync(ILoggerFactory loggerFactory, BlogUNAHContext context) 
        {
            try
            {
                var jsonFilePath = "SeedData/categories.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(jsonContent);

                if (!await context.Categories.AnyAsync()) 
                {
                    for (int i = 0; i < categories.Count; i++) 
                    {
                        categories[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        categories[i].CreatedDate = DateTime.Now;
                        categories[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        categories[i].UpdatedDate = DateTime.Now;
                    }
                    
                    context.AddRange(categories);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e) 
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de categorias");
            }
        }

        public static async Task LoadPostsAsync(ILoggerFactory loggerFactory, BlogUNAHContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/posts.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var posts = JsonConvert.DeserializeObject<List<PostEntity>>(jsonContent);

                if (!await context.Posts.AnyAsync())
                {
                    for (int i = 0; i < posts.Count; i++)
                    {
                        posts[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        posts[i].CreatedDate = DateTime.Now;
                        posts[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        posts[i].UpdatedDate = DateTime.Now;
                    }

                    context.AddRange(posts);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Posts");
            }
        }

        public static async Task LoadTagsAsync(ILoggerFactory loggerFactory, BlogUNAHContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/tags.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var tags = JsonConvert.DeserializeObject<List<TagEntity>>(jsonContent);

                if (!await context.Tags.AnyAsync())
                {
                    for (int i = 0; i < tags.Count; i++)
                    {
                        tags[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        tags[i].CreatedDate = DateTime.Now;
                        tags[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        tags[i].UpdatedDate = DateTime.Now;
                    }

                    context.AddRange(tags);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de Tags");
            }
        }

        public static async Task LoadPostsTagsAsync(ILoggerFactory loggerFactory, BlogUNAHContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/posts_tags.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var postTags = JsonConvert.DeserializeObject<List<PostTagEntity>>(jsonContent);

                if (!await context.PostsTags.AnyAsync())
                {
                    for (int i = 0; i < postTags.Count; i++)
                    {
                        postTags[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        postTags[i].CreatedDate = DateTime.Now;
                        postTags[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        postTags[i].UpdatedDate = DateTime.Now;
                    }

                    context.AddRange(postTags);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<BlogUNAHSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de PostsTags");
            }
        }
    }
}
