﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using UniLinks.Dependencies.Attributes;

namespace UniLinks.Client.Web.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index() => View();

		[HttpGet("noauth")]
		public IActionResult NoAuth() => View();

		[HttpGet("logout")]
		[Authorizes]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("");
		}
	}
}