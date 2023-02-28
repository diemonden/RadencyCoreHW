using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RadencyTask2.Data;
using RadencyTask2.Models;
using RadencyTask2.Services;

namespace RadencyTask2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext with in-memory database provider
            services.AddDbContext<LibraryContext>(options =>
                options.UseInMemoryDatabase(databaseName: "Library")
            );
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IDbContext, LibraryContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, LibraryContext context)
        {
            context.Database.EnsureCreated();
            context.SeedBooks();
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
