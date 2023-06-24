using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MusicalQuiz.Main.Dependencies;
using MusicalQuiz.Main.Middleware;
using MusicalQuiz.Main.Modules.Infrastructure.Jwt;

namespace MusicalQuiz.Main
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenSetting = Configuration.GetSection("TokenSetting").Get<TokenSetting>();
            services.AddSingleton(tokenSetting);

            services.AddSingleton(provider =>
            {
                var rsa = RSA.Create();
                rsa.ImportRSAPublicKey(
                    Convert.FromBase64String(tokenSetting.PublicKey),
                    out _
                );

                return new RsaSecurityKey(rsa);
            });

            services.AddAuthorization();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", x =>
            {
                SecurityKey rsa = services.BuildServiceProvider()
                    .GetRequiredService<RsaSecurityKey>();
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = rsa,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidIssuer = tokenSetting.Issuer,
                    ValidAudience = tokenSetting.Audience,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicalQuiz", Version = "v1" });
            });

#if DEBUG
            services.AddCors(options =>
            {
                options.AddPolicy("AllowEverything",
                    builder =>
                    {
                        builder.SetIsOriginAllowed((arg) => arg == "http://localhost");
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
#endif

            services.AddCors(options =>
            {
                options.AddPolicy("AllowProduction",
                    builder =>
                    {
                        builder
                            .WithOrigins("https://musical-quiz-web.web.app")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            new DependenciesRegistrar(services, Configuration).Register();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicalQuiz v1"));
            }

#if DEBUG
            app.UseMiddleware<RequestLogger>();
#endif

            app.UseMiddleware<ScopedLogger>();

            app.UseMiddleware<ExceptionsLogger>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

#if DEBUG
            app.UseCors("AllowEverything");
#endif
#if !DEBUG
            app.UseCors("AllowProduction");
#endif
            //app.UseCors(builder => { builder.WithOrigins("http://localhost").WithMethods("GET").AllowAnyHeader(); });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}