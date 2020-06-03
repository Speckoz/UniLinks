using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services.Student;
using UniLinks.Client.Web.Services;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.Client.Web.Controllers
{
	public class StudentController : Controller
	{
		private readonly AuthService _authService;
		private readonly LessonService _lessonService;

		public StudentController(AuthService authService, LessonService lessonService)
		{
			_authService = authService;
			_lessonService = lessonService;
		}

		[HttpGet]
		[Authorizes(UserTypeEnum.Student)]
		public async Task<IActionResult> Index()
		{
			string token = User.FindFirst("Token").Value;
			string disciplines = User.FindFirst("Disciplines").Value;

			List<LessonDisciplineVO> model = await _lessonService.GetAllLessonsTaskAync(token, disciplines);
			return View(model);
		}

		[HttpGet("auth/student")]
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

		[HttpPost("auth/student")]
		public async Task<IActionResult> Auth(LoginStudentRequestModel login)
		{
			if (ModelState.IsValid)
			{
				if (!(await _authService.AuthAccountTaskAsync(login.Email) is StudentVO student))
					return View();

				await GenerateClaims(student);
				return RedirectToAction("Index", "Student");
			}

			return View();
		}

		public async Task GenerateClaims(StudentVO student)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, student.StudentId.ToString()),
				new Claim(ClaimTypes.Name, student.Name),
				new Claim(ClaimTypes.Email, student.Email),
				new Claim(ClaimTypes.Role, nameof(UserTypeEnum.Student)),
				new Claim("Disciplines", string.Join(';', student.Disciplines.Select(x => x.DisciplineId).ToList())),
				new Claim("Token", student.Token)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

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