using ApplicationCore.Services;
using EonixWebApi.ApplicationCore.Repositories;
using EonixWebApi.Infrastructure.Data;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EonixWebApi.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //public static MvcOptions AddCustomInputFormatters(this MvcOptions options)
        //{
        //    var xmlInputFormatter = new XmlDataContractSerializerInputFormatter(options);
        //    options.InputFormatters.Add(xmlInputFormatter);

        //    return options;
        //}
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
       services.AddDbContext<EonixWebApiDbContext>(opts =>
           opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"),b =>b.MigrationsAssembly("WebApi")));
        public static void ConfigurePersonService(this IServiceCollection services) =>
            services.AddScoped<IPersonService, PersonService>();
        public static void ConfigurePersonRepository(this IServiceCollection services) =>
            services.AddScoped<IPersonRepository, PersonRepository>();
        //public static MvcOptions AddCustomOutputFormatters<TJsonOutputFormatter>(this MvcOptions options) where TJsonOutputFormatter : OutputFormatter
        //{
        //    var xmlOutputFormatter = new XmlDataContractSerializerOutputFormatter();
        //    options.OutputFormatters.Add(xmlOutputFormatter);
        //    var jsonOutputFormatter = options.OutputFormatters
        //                                     .OfType<TJsonOutputFormatter>()
        //                                     .First();
        //    var customMediaType = new string[] { };
        //    customMediaType.Execute(e => jsonOutputFormatter.SupportedMediaTypes.Add(e));
        //    return options;
        //}

    }

}
