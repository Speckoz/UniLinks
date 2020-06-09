using Microsoft.AspNetCore.Mvc;

using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services.Coordinator;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Controllers.Coordinator
{
	[Route("Coordinator/[Controller]")]
	[Authorizes(UserTypeEnum.Coordinator)]
	public class CourseController : Controller
	{
		private readonly CourseService _courseService;

		public CourseController(CourseService courseService)
		{
			_courseService = courseService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<CourseVO> response = await _courseService.GetCourseByCoordIdTaskAsync(token);

			return response.StatusCode switch
			{
				HttpStatusCode.OK => View("/Views/Coordinator/Course/Index.cshtml", response),
				HttpStatusCode.NotFound => View("/Views/Coordinator/Course/CourseNotFound.cshtml", response.Object),
				_ => View("/Views/Coordinator/Course/AddCourse.cshtml", response),
			};
		}

		[HttpGet("Add")]
		public async Task<IActionResult> AddCourse()
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<CourseVO> response = await _courseService.GetCourseByCoordIdTaskAsync(token);

			return View("/Views/Coordinator/Course/AddCourse.cshtml", response);
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddCourse(ResultModel<CourseVO> newCourse)
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<CourseVO> response = await _courseService.AddCourseTaskAsync(newCourse.Object, token);

			return response.StatusCode switch
			{
				HttpStatusCode.Created => View("/Views/Coordinator/Course/Index.cshtml", response),
				_ => View("/Views/Coordinator/Course/AddCourse.cshtml", response),
			};
		}

		[HttpGet("Update")]
		public async Task<IActionResult> UpdateCourse()
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<CourseVO> response = await _courseService.GetCourseByCoordIdTaskAsync(token);

			return View("/Views/Coordinator/Course/UpdateCourse.cshtml", response);
		}

		[HttpPost("Update")]
		public async Task<IActionResult> UpdateCourse(ResultModel<CourseVO> request)
		{
			string token = User.FindFirst("Token").Value;
			request.Object.CoordinatorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			request.Object.CourseId = Guid.Parse(User.FindFirst("CourseId").Value);

			ResultModel<CourseVO> response = await _courseService.UpdateCourseTaskAsync(request.Object, token);

			if (response.StatusCode != HttpStatusCode.Created)
				return View("/Views/Coordinator/Course/UpdateCourse.cshtml", new ResultModel<CourseVO>
				{
					Object = request.Object,
					Message = response.Message,
					StatusCode = response.StatusCode
				});

			ResultModel<CourseVO> courseResponse = await _courseService.GetCourseByCoordIdTaskAsync(token);

			if (courseResponse.StatusCode == HttpStatusCode.OK)
			{
				courseResponse.Message = response.Message;
				courseResponse.StatusCode = response.StatusCode;
			}

			return View("/Views/Coordinator/Course/Index.cshtml", courseResponse);
		}
	}
}