using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySqlConnector;

namespace bcw_2023summer_keepr
{

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			ConfigureCors(services);
			ConfigureAuth(services);
			_ = services.AddControllers();
			_ = services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "bcw_2023summer_keepr", Version = "v1" });
			});
			_ = services.AddSingleton<Auth0Provider>();
			_ = services.AddScoped(x => CreateDbConnection());

			_ = services.AddScoped<AccountsRepository>();
			_ = services.AddScoped<AccountService>();

			_ = services.AddScoped<KeepsRepository>();
			_ = services.AddScoped<KeepsService>();

			_ = services.AddScoped<VaultsRepository>();
			_ = services.AddScoped<VaultsService>();

			_ = services.AddScoped<VaultKeepsRepository>();
			_ = services.AddScoped<VaultKeepsService>();
		}

		private static void ConfigureCors(IServiceCollection services)
		{
			_ = services.AddCors(options =>
			{
				options.AddPolicy("CorsDevPolicy", builder =>
							{
								_ = builder
									.AllowAnyMethod()
									.AllowAnyHeader()
									.AllowCredentials()
									.WithOrigins(new string[]{
								"http://localhost:8080", "http://localhost:8081"
							});
							});
			});
		}

		private void ConfigureAuth(IServiceCollection services)
		{
			_ = services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.Authority = $"https://{Configuration["AUTH0_DOMAIN"]}/";
				options.Audience = Configuration["AUTH0_AUDIENCE"];
			});

		}

		private IDbConnection CreateDbConnection()
		{
			string connectionString = Configuration["CONNECTION_STRING"];
			return new MySqlConnection(connectionString);
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				_ = app.UseDeveloperExceptionPage();
				_ = app.UseSwagger();
				_ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jot v1"));
				_ = app.UseCors("CorsDevPolicy");
			}
			else
			{
				_ = app.UseMiddleware<GlobalRoutePrefixMiddleware>("/keepr");
				_ = app.UsePathBase(new PathString("/keepr"));
			}

			_ = app.UseHttpsRedirection();

			_ = app.UseDefaultFiles();
			_ = app.UseStaticFiles();

			_ = app.UseRouting();

			_ = app.UseAuthentication();

			_ = app.UseAuthorization();


			_ = app.UseEndpoints(endpoints =>
			{
				_ = endpoints.MapControllers();
			});
		}
	}
}