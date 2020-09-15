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

        // ���̃��\�b�h�̓����^�C���ɂ���ČĂяo����܂��B���̃��\�b�h���g�p���āA�R���e�i�[�ɃT�[�r�X��ǉ����܂��B
        // �A�v���P�[�V�����̍\�����@�̏ڍׂɂ��ẮAhttps�F//go.microsoft.com/fwlink/�HLinkID = 398940�ɃA�N�Z�X���Ă��������B
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShohinContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ShohinContext")));
            services.AddControllers();
        }

        // ���̃��\�b�h�̓����^�C���ɂ���ČĂяo����܂��B���̃��\�b�h���g�p���āAHTTP�v���p�C�v���C�����\�����܂��B
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
