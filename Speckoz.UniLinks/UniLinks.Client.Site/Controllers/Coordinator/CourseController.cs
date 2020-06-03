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
	[Route("coordinator/course")]
	[Authorizes(UserTypeEnum.Coordinator)]
	public class CourseController : Controller
	{
		private readonly CourseService _courseService;

		public CourseController(CourseService courseService)
		{
			_courseService = courseService;
		}

		[HttpGet]
		public async Task<IActionResult> Course()
		{
			string token = User.FindFirst("Token").Value;

			ResponseModel<CourseVO> response = await _courseService.GetCourseByCoordIdTaskAsync(token);

			return response.StatusCode switch
			{
				HttpStatusCode.OK => View("/Views/Coordinator/Course/Course.cshtml", response.Object),
				HttpStatusCode.NotFound => View("/Views/Coordinator/Course/CourseNotFound.cshtml", response.Object),
				_ => View("/Views/Coordinator/Course/AddCourse.cshtml", response.Object),
			};
		}

		[HttpGet("add")]
		public async Task<IActionResult> AddCourse()
		{
			string token = User.FindFirst("Token").Value;

			ResponseModel<CourseVO> response = await _courseService.GetCourseByCoordIdTaskAsync(token);

			return View("/Views/Coordinator/Course/AddCourse.cshtml", response);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddCourse(ResponseModel<CourseVO> newCourse)
		{
			string token = User.FindFirst("Token").Value;

			ResponseModel<CourseVO> response = await _courseService.AddCourseTaskAsync(newCourse.Object, token);

			return response.StatusCode switch
			{
				HttpStatusCode.Created => View("/Views/Coordinator/Course/Course.cshtml", response.Object),
				_ => View("/Views/Coordinator/Course/AddCourse.cshtml", response),
			};
		}

		[HttpGet("update")]
		public async Task<IActionResult> UpdateCourse()
		{
			string token = User.FindFirst("Token").Value;

			ResponseModel<CourseVO> response = await _courseService.GetCourseByCoordIdTaskAsync(token);

			return response.StatusCode switch
			{
				HttpStatusCode.Created => View("/Views/Coordinator/Course/Course.cshtml", response.Object),
				_ => View("/Views/Coordinator/Course/UpdateCourse.cshtml", response),
			};
		}

		[HttpPost("update")]
		public async Task<IActionResult> UpdateCourse(ResponseModel<CourseVO> newCourse)
		{
			string token = User.FindFirst("Token").Value;
			newCourse.Object.CoordinatorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			newCourse.Object.CourseId = Guid.Parse(User.FindFirst("CourseId").Value);

			ResponseModel<CourseVO> response = await _courseService.UpdateCourseTaskAsync(newCourse.Object, token);

			return response.StatusCode switch
			{
				HttpStatusCode.OK => View("/Views/Coordinator/Course/Course.cshtml", response.Object),
				_ => View("/Views/Coordinator/Course/UpdateCourse.cshtml", response),
			};
		}
	}
}