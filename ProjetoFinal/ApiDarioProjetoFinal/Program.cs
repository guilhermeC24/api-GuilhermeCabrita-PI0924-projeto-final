using Microsoft.AspNetCore.Authentication.JwtBearer;
using ApiDarioProjetoFinal.Resilience;
using ApiDarioProjetoFinal.External;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ApiDarioProjetoFinal.Cache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<CacheServico>();

// HttpClient com Polly Retry 
builder.Services.AddHttpClient("PagamentoAPI")
    .AddPolicyHandler(ConfiguracaoPolly.CriarFallback())
    .AddPolicyHandler(ConfiguracaoPolly.CriarCircuitBreaker())
    .AddPolicyHandler(ConfiguracaoPolly.CriarPoliticaRetry());

// Registar serviço externo
builder.Services.AddScoped<ServicoPagamentoExterno>();

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("chave-super-secreta-api-dario-123")
        ),

        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Coloca: Bearer {token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });


    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();