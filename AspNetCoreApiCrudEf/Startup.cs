using AspNetCoreApiCrudEf.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Tarefas Api", Version = "v1" });
            });
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

            // Ative o middleware para servir o Swagger gerado como um endpoint JSON.
            app.UseSwagger();

            // Habilitar o middleware para servir swagger-ui (HTML, JS, CSS, etc.)
            // especificando o terminal JSON do Swagger.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tarefas Api");
                //  Para atender à interface do usuário do Swagger na raiz do aplicativo
                //c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
