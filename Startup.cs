﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoStoreApi.Models;

namespace VideoStoreApi
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
            services.AddDbContext<SessionContext>(opt => opt.UseInMemoryDatabase("Session"));
            services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("Customers"));
            services.AddDbContext<EmployeeContext>(opt => opt.UseInMemoryDatabase("Employees"));
            services.AddDbContext<MovieContext>(opt => opt.UseInMemoryDatabase("Movies"));
            services.AddDbContext<PermissionContext>(opt => opt.UseInMemoryDatabase("Permissions"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                      IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                          name: "default",
                          template: "{ controller = Home}/{ action = Index}/{ id ?}");
                }
              );
        }
    }
}
