using AutoMapper;
using EonixWebApi.WebApi.Extensions;
using Microsoft.OpenApi.Models;
using WebApi.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new PersonMappingProfile());
});
builder.Services.AddSingleton<IMapper>(sp => config.CreateMapper());

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigurePersonRepository();
builder.Services.ConfigurePersonService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eonix API", Version = "v1" }));
var app = builder.Build();
app.UseSwaggerUI();
app.UseSwagger(x => x.SerializeAsV2 = true);
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
