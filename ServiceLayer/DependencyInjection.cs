using InfrastructureLayer.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceLayer.Features.CommandHandlers.BrandHandlers;
using ServiceLayer.Interfaces;
using ServiceLayer.Interfaces.FinaInterfaces;
using ServiceLayer.Mapping;
using ServiceLayer.Services;
using ServiceLayer.Services.FinaServices.FinaHelpers;
using ServiceLayer.Services.FinaServices;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace ServiceLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateBrandCommandHandler).Assembly);
        });

        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        services.AddTransient<IAuthService, AuthService>();

        services.AddSingleton<TokenStorageService>();
        services.AddHttpClient<FinaApiClient>();
        services.AddHttpClient<AuthenticationService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IProductGroupService, ProductGroupService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ICharacteristicService, CharacteristicService>();
        services.AddTransient<IFinaIntegrationService, FinaIntegrationService>();

        services.AddTransient<IFileService, FileService>();

        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ECommerceDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,
                ValidAudience = configuration.GetSection("Jwt:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(configuration.GetSection("Jwt:Key").Value!))
            };
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
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
                    new string[]{ }
                }
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        return services;
    }
}
