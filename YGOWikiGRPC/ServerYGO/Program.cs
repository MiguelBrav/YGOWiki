using Microsoft.AspNetCore.Server.Kestrel.Core;
using ServerYGO.Automapper;
using ServerYGO.Data;
using ServerYGO.Interfaces;
using ServerYGO.Repositories;
using ServerYGO.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80, o =>
    {
        o.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

builder.Services.AddTransient<ITranslatedCardTypesService, TranslatedCardTypesService>();
builder.Services.AddTransient<ITranslatedAttributeService, TranslatedAttributeService>();
builder.Services.AddTransient<ITranslatedBanlistTypeService, TranslatedBanlistTypeService>();
builder.Services.AddTransient<ITranslatedMonsterCardTypeService, TranslatedMonsterCardTypeService>();
builder.Services.AddTransient<ITranslatedMonsterTypeService, TranslatedMonsterTypeService>();
builder.Services.AddTransient<ITranslatedRarityTypeService, TranslatedRarityTypeService>();
builder.Services.AddTransient<ITranslatedSpecialMonsterTypeService, TranslatedSpecialMonsterTypeService>();
builder.Services.AddTransient<ITranslatedSpellCardTypeService, TranslatedSpellCardTypeService>();
builder.Services.AddTransient<ITranslatedTrapCardTypeService, TranslatedTrapCardTypeService>();
builder.Services.AddTransient<DbContextClass>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(YugiMapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
//app.UseGrpcWeb();
app.UseCors("AllowAll");
app.UseEndpoints(endpoints =>
{
    app.MapGrpcService<YGOService>().RequireCors("AllowAll");

    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client");
    });
});

app.Run();

public partial class Program { }