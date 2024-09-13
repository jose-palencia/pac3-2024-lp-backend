using BlogUNAH.API.Database;
using BlogUNAH.API.Helpers;
using BlogUNAH.API.Services;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogUNAH.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var name = Configuration.GetConnectionString("DefaultConnection");

            // Add DbContext
            services.AddDbContext<BlogUNAHContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add custom services
            services.AddTransient<ICategoriesService, CategoriesSQLService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IPostsService, PostsService>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // CORS Configuration
            services.AddCors(opt => 
            {
                var allowURLS = Configuration.GetSection("AllowURLS").Get<string[]>();
                
                opt.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins(allowURLS)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });

        }

    }
}
