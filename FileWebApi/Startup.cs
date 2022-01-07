using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //services.AddCors(options => {

            //    options.AddPolicy("FileWebApiPolicy",
            //    builder => {
            //        builder.WithOrigins("https://localhost:44305",
            //                              "http://localhost:44305",
            //                              "https://localhost:44367/",
            //                              "http://localhost:44367/",
            //                              "http://localhost:63763",
            //                              "http://10.68.33.190",
            //                              "http://10.68.33.190/",
            //                              "https://localhost",
            //                              "http://localhost",
            //                              "file://")
            //                            .AllowAnyHeader()
            //                            .AllowCredentials()
            //                            .AllowAnyMethod();
            //    });

            //});

            services.AddCors();

            services.AddControllers();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials


            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors("FileWebApiPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            

            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
