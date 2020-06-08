using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services;
using UniLinks.Client.Site.Services.Student;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO.Class;
using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.Client.Site.Controllers
{
	public class StudentController : Controller
	{
		private readonly AuthService _authService;
		private readonly LessonService _lessonService;
		private readonly ClassService _classService;

		public StudentController(AuthService authService, LessonService lessonService, ClassService classService)
		{
			_authService = authService;
			_lessonService = lessonService;
			_classService = classService;
		}

		[HttpGet]
		[Authorizes(UserTypeEnum.Student)]
		public async Task<IActionResult> Index()
		{
			string token = User.FindFirst("Token").Value;
			string disciplines = User.FindFirst("Disciplines").Value;

			ResultModel<List<LessonDisciplineVO>> model = await _lessonService.GetAllLessonsTaskAync(token, disciplines.Split(';').ToList());

			return View(model.Object);
		}

		[HttpGet]
		[Authorizes(UserTypeEnum.Student)]
		public async Task<IActionResult> Classes()
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<List<ClassVO>> response = await _classService.GetAllClassesTaskAsync(token);

			return View("/Views/Student/Classes.cshtml", response.Object);
		}

		[HttpGet]
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

		[HttpPost]
		public async Task<IActionResult> Auth(ResultModel<LoginStudentRequestModel> request)
		{
			if (ModelState.IsValid)
			{
				ResultModel<AuthStudentVO> response = await _authService.AuthAccountTaskAsync(request.Object.Email);
				if (response.StatusCode != HttpStatusCode.OK)
					return View(new ResultModel<LoginStudentRequestModel>
					{
						Object = request.Object,
						Message = response.Message,
						StatusCode = response.StatusCode
					});

				await GenerateClaims(response.Object);
				return RedirectToAction("Index", "Student");
			}

			return View();
		}

		public async Task GenerateClaims(AuthStudentVO student)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, student.StudentId.ToString()),
				new Claim(ClaimTypes.Name, student.Name),
				new Claim(ClaimTypes.Email, student.Email),
				new Claim(ClaimTypes.Role, nameof(UserTypeEnum.Student)),
				new Claim("Disciplines", student.Disciplines),
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