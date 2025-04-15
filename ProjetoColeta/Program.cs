using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoColeta.Data.Contexts;
using ProjetoColeta.Models;
using ProjetoColeta.Repository;
using ProjetoColeta.Services;

var builder = WebApplication.CreateBuilder(args);
#region Inicializando o Banco de dados
var connectionString = builder.Configuration.GetConnectionString("DataBaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true));
#endregion
builder.Services.AddScoped<IColetorService, ColetorService>();

builder.Services.AddScoped<IResiduoRepository, ResiduoRepository>();
builder.Services.AddScoped<IResiduoService, ResiduoService>();

builder.Services.AddScoped<IPontoRepository, PontoRepository>();
builder.Services.AddScoped<IPontoService, PontoService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IColetaService, ColetaService>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IRepository<ClienteModel>, Repository<ClienteModel>>();

#region Autenticação
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.Run();
    