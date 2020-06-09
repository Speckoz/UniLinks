using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services;
using UniLinks.Client.Site.Services.Coordinator;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.Client.Site.Controllers
{
	public class CoordinatorController : Controller
	{
		[HttpGet]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> Index([FromServices] StatusService statusService, [FromServices] CourseService courseService)
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<CourseVO> course = await courseService.GetCourseByCoordIdTaskAsync(token);

			if (course.StatusCode == HttpStatusCode.NotFound)
				return View("/Views/Coordinator/Course/CourseNotFound.cshtml", course.Object);

			ResultModel<StatusVO> status = await statusService.GetStatusDataTaskAsync(token);

			return View(status.Object);
		}

		[HttpGet]
		public IActionResult Auth()
		{
			return (User.FindFirst(ClaimTypes.Role)?.Value) switch
			{
				nameof(UserTypeEnum.Coordinator) => RedirectToAction("Index", "Coordinator"),
				nameof(UserTypeEnum.Student) => RedirectToAction("Index", "Student"),
				_ => View(),
			};
		}

		[HttpPost]
		public async Task<IActionResult> Auth([FromServices] AuthService authService, ResultModel<LoginRequestModel> request)
		{
			if (ModelState.IsValid)
			{
				ResultModel<AuthCoordinatorVO> response = await authService.AuthAccountTaskAsync(request.Object);

				if (response.StatusCode != HttpStatusCode.OK)
					return View(new ResultModel<LoginRequestModel>
					{
						Object = request.Object,
						Message = response.Message,
						StatusCode = response.StatusCode
					});

				await GenerateClaims(response.Object);
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