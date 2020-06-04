using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.Client.Site.Controllers
{
	public class CoordinatorController : Controller
	{
		[HttpGet]
		[Authorizes(UserTypeEnum.Coordinator)]
		public IActionResult Index() => View();

		[HttpGet("Auth")]
		public IActionResult Auth()
		{
			switch (User.FindFirst(ClaimTypes.Role)?.Value)
			{
				case nameof(UserTypeEnum.Coordinator):
					return RedirectToAction("Index", "Coordinator");

				case nameof(UserTypeEnum.Student):
					return RedirectToAction("Index", "Student");
			}

			return View();
		}

		[HttpPost("Auth")]
		public async Task<IActionResult> Auth([FromServices] AuthService authService, LoginRequestModel login)
		{
			if (ModelState.IsValid)
			{
				if (!(await authService.AuthAccountTaskAsync(login) is AuthCoordinatorVO authCoordinator))
					return View();

				await GenerateClaims(authCoordinator);
				return RedirectToAction("Index", "Coordinator");
			}

			return View();
		}

		public async Task GenerateClaims(AuthCoordinatorVO authCoordinator)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, authCoordinator.CoordinatorId.ToString()),
				new Claim(ClaimTypes.Name, authCoordinator.Name),
				new Claim(ClaimTypes.Email, authCoordinator.Email),
				new Claim(ClaimTypes.Role, nameof(UserTypeEnum.Coordinator)),
				new Claim("CourseId", authCoordinator.CourseId.ToString()),
				new Claim("Token", authCoordinator.Token)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme
			);

			var authProp = new AuthenticationProperties
			{
				IssuedUtc = DateTime.Now,
				ExpiresUtc = DateTimeOffset.Now.AddDays(2),
				IsPersistent = true
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProp);
		}
	}
}