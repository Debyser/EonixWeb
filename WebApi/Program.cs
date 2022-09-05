using AutoMapper;
using EonixWebApi.WebApi.Extensions;
using WebApi.Mappings;
using NLog;
using WebApi.Extensions;
using ApplicationCore.Services;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new PersonMappingProfile());
});
builder.Services.AddSingleton<IMapper>(sp => config.CreateMapper());
builder.Services.ConfigureLoggerService();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigurePersonRepository();
builder.Services.ConfigurePersonService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); //the Build method builds the WebApplication and registers all the services added with IOC
var logger = app.Services.GetRequiredService<ILoggerService>(); 
app.ConfigureExceptionHandler(logger);

app.UseSwaggerUI();
app.UseSwagger(x => x.SerializeAsV2 = true);
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
