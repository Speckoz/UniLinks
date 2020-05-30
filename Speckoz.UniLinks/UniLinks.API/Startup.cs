using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using System.Text;

using UniLinks.API.Business;
using UniLinks.API.Business.Interfaces;
using UniLinks.API.Data;
using UniLinks.API.Filters;
using UniLinks.API.Repository;
using UniLinks.API.Repository.Interfaces;
using UniLinks.API.Services;
using UniLinks.API.Services.Email;
using UniLinks.API.Services.Email.Interfaces;
using UniLinks.API.Utils;

using static System.Environment;

namespace UniLinks.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			// JWT Authentication
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = Configuration["JWT:Issuer"],

					ValidateAudience = true,
					ValidAudience = Configuration["JWT:Audience"],

					ValidateLifetime = true,

					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
				});

			// MySQL Database
			string host = GetEnvironmentVariable("DBHOST") ?? "localhost";
			string password = GetEnvironmentVariable("DBPASSWORD") ?? "numsey";
			string port = GetEnvironmentVariable("DBPORT") ?? "3306";
			services.AddDbContext<DataContext>
			(
				options => options.UseMySql($"server={host};userid=root;pwd={password};port={port};database=unilinks",
				builder => builder.MigrationsAssembly("UniLinks.API"))
			);

			// Injecoes do smtp/email
			services.Configure<ConfigEmailModel>(Configuration.GetSection("ConfigEmailModel"));
			services.AddTransient<ISendEmailService, SendEmailService>();

			// Seed
			services.AddScoped<DataSeeder>();

			services.AddControllers(x => x.InputFormatters.Insert(x.InputFormatters.Count, new TextPlainInputFormatter()));

			// Services
			services.AddScoped<GenerateTokenService>();
			services.AddScoped<SecurityService>();
			services.AddScoped<CollabAPIService>();

			// Repositories
			services.AddScoped<ICoordinatorRepository, CoordinatorRepository>();
			services.AddScoped<ILessonRepository, LessonRepository>();
			services.AddScoped<IStudentRepository, StudentRepository>();
			services.AddScoped<ICourseRepository, CourseRepository>();
			services.AddScoped<IDisciplineRepository, DisciplineRepository>();
			services.AddScoped<IClassRepository, ClassRepository>();

			// Business
			services.AddScoped<ICoordinatorBusiness, CoordinatorBusiness>();
			services.AddScoped<ILessonBusiness, LessonBusiness>();
			services.AddScoped<IStudentBusiness, StudentBusiness>();
			services.AddScoped<ICourseBusiness, CourseBusiness>();
			services.AddScoped<IDisciplineBusiness, DisciplineBusiness>();
			services.AddScoped<IClassBusiness, ClassBusiness>();

			// Filter
			services.AddMvc(options => options.Filters.Add(typeof(ErrorResponseFilter)));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder dataSeeder)
		{
			using (IServiceScope scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				DataContext dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
				dbContext.Database.Migrate();
			}

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				dataSeeder.Init();
			}

			//app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}