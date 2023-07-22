using ServerYGO.Data;
using ServerYGO.Interfaces;
using ServerYGO.Repositories;
using ServerYGO.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

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
builder.Services.AddTransient<DbContextClass>();
builder.Services
  .AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseGrpcWeb();
app.UseCors();
app.UseEndpoints(endpoints =>
{
    app.MapGrpcService<YGOService>().RequireCors("AllowAll").EnableGrpcWeb();


    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    });
});

app.Run();

