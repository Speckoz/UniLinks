using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

using UniLinks.Dependencies.Attributes;

namespace UniLinks.Client.Site.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index() => View();

		[HttpGet("Problem/404")]
		public IActionResult PageNotFound() => View();

		[HttpGet("Problem/500")]
		public IActionResult PageInternalError() => View();

		[HttpGet]
		public IActionResult NoAuth() => View();

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			Response.Cookies.Delete("theme");
			return RedirectToAction("Index", "Home");
		}
	}
}