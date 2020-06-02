using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using UniLinks.Client.Web.Services;
using UniLinks.Client.Web.Services.Coordinator;

namespace UniLinks.Client.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
					.AddCookie(o =>
					{
						o.LoginPath = "/noauth";
						o.AccessDeniedPath = "/noauth";
					});

			//Services
			services.AddScoped<AuthService>();
			services.AddScoped<CourseService>();
			services.AddScoped<DisciplineService>();
			services.AddScoped<ClassService>();
			services.AddScoped<StudentsService>();
			services.AddScoped<LessonService>();
			services.AddScoped<Site.Services.Student.LessonService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/problem/500");
				app.UseHsts();
			}

			app.UseStatusCodePagesWithReExecute("/problem/{0}");

			// app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}");
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Coordinator}/{action=Index}");
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Student}/{action=Index}");
			});
		}
	}
}