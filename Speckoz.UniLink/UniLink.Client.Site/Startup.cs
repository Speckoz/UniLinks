using Blazored.SessionStorage;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using UniLink.Client.Site.Services;
using UniLink.Client.Site.Services.Coordinator;

namespace UniLink.Client.Site
{
	public class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages(x => x.RootDirectory = "/");
			services.AddServerSideBlazor();

			services.AddBlazoredSessionStorage();
			services.AddSyncfusionBlazor();

			services.AddScoped<AuthenticationStateProvider, AuthenticationStateProviderService>();

			// Services
			services.AddScoped<AccountService>();
			services.AddScoped<DisciplineService>();
			services.AddScoped<CourseService>();
			services.AddScoped<StudentService>();
			services.AddScoped<ThemeService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			//app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}