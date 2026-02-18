using Microsoft.OpenApi.Models;
using ServerYGO;
using System.Reflection;
using UseCaseCore.UseCases;
using YGOClient.Interfaces;
using YGOClient.QueriesHandler;
using YGOClient.Services;
using YGOClient.Aggregator;
using YGOClient.Aggregator.Interfaces;

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
//builder.Services.AddMediatR(a => a.RegisterServicesFromAssembly(typeof(Program).Assembly));
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

builder.Services.AddTransient<AllAttributesPageQueryHandler>();
builder.Services.AddTransient<AllAttributesQueryHandler>();
builder.Services.AddTransient<AllBanlistPageQueryHandler>();
builder.Services.AddTransient<AllBanlistQueryHandler>();
builder.Services.AddTransient<AllMonsterCardsQueryHandler>();
builder.Services.AddTransient<AllMonsterTypesPageQueryHandler>();
builder.Services.AddTransient<AllMonsterCardsPageQueryHandler>();
builder.Services.AddTransient<AllMonsterTypesQueryHandler>();
builder.Services.AddTransient<AllRaritiesPageQueryHandler>();
builder.Services.AddTransient<AllRaritiesQueryHandler>();
builder.Services.AddTransient<AllSpecialMonsterCardsPageQueryHandler>();
builder.Services.AddTransient<AllSpecialMonsterCardsQueryHandler>();
builder.Services.AddTransient<AllSpellsPageQueryHandler>();
builder.Services.AddTransient<AllSpellsQueryHandler>();
builder.Services.AddTransient<AllTrapsPageQueryHandler>();
builder.Services.AddTransient<AllTrapsQueryHandler>();
builder.Services.AddTransient<AllTypeCardsPageQueryHandler>();
builder.Services.AddTransient<AllTypeCardsQueryHandler>();
builder.Services.AddTransient<AttributeByIdQueryHandler>();
builder.Services.AddTransient<BanlistByIdQueryHandler>();
builder.Services.AddTransient<MonsterCardByIdQueryHandler>();
builder.Services.AddTransient<MonsterTypeByIdQueryHandler>();
builder.Services.AddTransient<RarityByIdQueryHandler>();
builder.Services.AddTransient<SpecialMonsterCardByIdQueryHandler>();
builder.Services.AddTransient<SpellByIdQueryHandler>();
builder.Services.AddTransient<TrapByIdQueryHandler>();
builder.Services.AddTransient<TypeCardByIdQueryHandler>();
builder.Services.AddTransient<UseCaseDispatcher>();
// Aggregator registrations
builder.Services.AddTransient<IAttributeAggregator, AttributeAggregator>();
builder.Services.AddTransient<IBanlistAggregator, BanlistAggregator>();
builder.Services.AddTransient<IMonsterCardAggregator, MonsterCardAggregator>();
builder.Services.AddTransient<IMonsterTypeAggregator, MonsterTypeAggregator>();
builder.Services.AddTransient<IRarityAggregator, RarityAggregator>();
builder.Services.AddTransient<ISpecialMonsterAggregator, SpecialMonsterAggregator>();
builder.Services.AddTransient<ISpellAggregator, SpellAggregator>();
builder.Services.AddTransient<ITrapAggregator, TrapAggregator>();
builder.Services.AddTransient<ITypeCardAggregator, TypeCardAggregator>();

builder.Services.AddScoped(typeof(IPaginationService<>), typeof(PaginationService<>));
builder.Services.AddScoped<ICacheService, CacheService>();

AppContext.SetSwitch(
    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport",
    true
);

builder.Services.AddGrpcClient<YGOWiki.YGOWikiClient>(o =>
{
    o.Address = new Uri(builder.Configuration.GetValue<string>("YGOServer"));
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new SocketsHttpHandler
    {
        EnableMultipleHttp2Connections = false
    };
});

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
