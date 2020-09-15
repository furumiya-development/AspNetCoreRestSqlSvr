using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRestSqlSvr.Models;

namespace AspNetCoreRestSqlSvr
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // このメソッドはランタイムによって呼び出されます。このメソッドを使用して、コンテナーにサービスを追加します。
        // アプリケーションの構成方法の詳細については、https：//go.microsoft.com/fwlink/？LinkID = 398940にアクセスしてください。
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShohinContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ShohinContext")));
            services.AddControllers();
        }

        // このメソッドはランタイムによって呼び出されます。このメソッドを使用して、HTTP要求パイプラインを構成します。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
