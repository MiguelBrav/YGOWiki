using Grpc.Net.Client.Web;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ServerYGO;
using System.Reflection;
using YGOClient.Interfaces;
using YGOClient.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddMediatR(a => a.RegisterServicesFromAssembly(typeof(Program).Assembly));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "YGOClient", Version = "v1" });
    // Include the XML comments in the Swagger documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped(typeof(IPaginationService<>), typeof(PaginationService<>));
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddGrpcClient<YGOWiki.YGOWikiClient>(o =>
{
    o.Address = new Uri(builder.Configuration.GetValue<string>("YGOServer"));
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
});

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
