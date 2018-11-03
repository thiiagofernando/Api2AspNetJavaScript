using System.Web.Http;
using WebActivatorEx;
using WebApp01;
using Swashbuckle.Application;

//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApp01
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Empresa")
                .License(l =>
                {
                    l.Name("MIT");
                    l.Url("htt://empresa.com/license");
                })
                .Contact(ct =>
                {
                    ct.Name("Desenvolvido Por Thiago Fernando");
                    ct.Email("thiago.fernando@msn.com");
                    ct.Url("htt://empresa.com/nome-produto");
                })
                .Description("Api Aluno")
                .TermsOfService("Nenhuma");
            })
            .EnableSwaggerUi(c =>
            {

            });
        }
    }
}
