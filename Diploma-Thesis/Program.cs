using AutoMapper;
using Diploma_Thesis;
using Diploma_Thesis.Repositories;
using Diploma_Thesis.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddCors(o => o.AddPolicy("NUXT", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IExpertisesRepository, ExpertisesRepository>();
builder.Services.AddScoped<IExpertisesService, ExpertisesService>();
builder.Services.AddScoped<IVectorsRepository, VectorsRepository>();
builder.Services.AddScoped<IDiapasonService, DiapasonService>();
builder.Services.AddScoped<IDiapasonsRepository, DiapasonsRepository>();

builder.Services.AddControllers();
builder.Services.AddMvc().AddXmlSerializerFormatters();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("NUXT");

app.Run();
