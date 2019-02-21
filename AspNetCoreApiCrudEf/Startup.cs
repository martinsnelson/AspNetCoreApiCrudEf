using AspNetCoreApiCrudEf.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApiCrudEf
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
            services.AddDbContext<TarefaContext>(options =>
            options.UseInMemoryDatabase("ListaTarefas"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Chamar a API com o jQuery
            ///habilitar o mapeamento de arquivo padrão:
            app.UseDefaultFiles();
            /// Configure o aplicativo para fornecer arquivos estáticos
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
