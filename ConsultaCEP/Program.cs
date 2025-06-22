using Microsoft.EntityFrameworkCore;
using ConsultaCEP.Data;
using ConsultaCEP.Mappings;
using ConsultaCEP.Services.Interfaces;
using ConsultaCEP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<ConsultaCEPDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddHttpClient<IViaCEPService, ViaCEPService>(client =>
{
    client.BaseAddress = new Uri("https://viacep.com.br/ws/");
});

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IContatoService, ContatoService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();


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

app.Run();
