using DemoFolderStructure.DataAccess;
using DemoFolderStructure.DataAccess.Repositories;
using DemoFolderStructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoFolderStructure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            const string connectionString =
                @"Server=localhost;Database=FileSystemDb;User=sa;Password=Password1";
            
            services.AddDbContext<FileSystemDbContext>(x =>
                x.UseSqlServer(connectionString)
            );

            services.AddTransient<IFileSystemRepository, FileSystemRepository>();
            services.AddTransient<IFileSystemService, FileSystemService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}