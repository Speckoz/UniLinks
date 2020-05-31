using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.Client.Web.Services;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.Client.Web.Controllers
{
	public class CoordinatorController : Controller
	{
		public readonly AuthService _authService;

		public CoordinatorController(AuthService authService) =>
			_authService = authService;

		[Authorizes(UserTypeEnum.Coordinator)]
		public IActionResult IndexCoordinator() => View();

		[HttpGet("auth/coordinator")]
		public IActionResult AuthCoordinator() => View();

		[HttpPost("auth/coordinator")]
		public async Task<IActionResult> AuthCoordinator([FromBody] LoginRequestModel login)
		{
			if (ModelState.IsValid)
			{
				if (!(await _authService.AuthAccountTaskAsync(login) is AuthCoordinatorVO authCoordinator))
					return View();

				await GenerateClaims(authCoordinator);
				return RedirectToAction("IndexCoordinator", "Coordinator");
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
				new Claim("Token", authCoordinator.Token)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme
			);

			var authProp = new AuthenticationProperties
			{
				IssuedUtc = DateTime.UtcNow,
				ExpiresUtc = DateTimeOffset.UtcNow.AddHours(5),
				IsPersistent = true
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProp);
		}
	}
}