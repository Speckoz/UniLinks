using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
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
	public class DisciplinesController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index([FromServices] DisciplineService disciplineService)
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<List<DisciplineVO>> response = await disciplineService.GetDisciplinesByCoordIdTaskAsync(token);

			return View("/Views/Coordinator/Disciplines/Index.cshtml", response);
		}

		[HttpGet("Add")]
		public IActionResult Add() => View("/Views/Coordinator/Disciplines/Add.cshtml");

		[HttpPost]
		public async Task<IActionResult> AddDiscipline([FromServices] DisciplineService disciplineService, ResultModel<DisciplineVO> request)
		{
			string token = User.FindFirst("Token").Value;

			request.Object.CourseId = Guid.Parse(User.FindFirst("CourseId").Value);

			ResultModel<DisciplineVO> response = await disciplineService.AddDisciplineTaskAsync(request.Object, token);

			if (response.StatusCode != HttpStatusCode.Created)
				return View("/Views/Coordinator/Disciplines/Add.cshtml", new ResultModel<DisciplineVO>
				{
					Object = request.Object,
					Message = response.Message,
					StatusCode = response.StatusCode
				});

			ResultModel<List<DisciplineVO>> disciplinesResponse = await disciplineService.GetDisciplinesByCoordIdTaskAsync(token);

			if (disciplinesResponse.StatusCode == HttpStatusCode.OK)
			{
				disciplinesResponse.Message = response.Message;
				disciplinesResponse.StatusCode = response.StatusCode;
			}

			return View("/Views/Coordinator/Disciplines/Index.cshtml", disciplinesResponse);
		}

		[HttpGet("Update/{disciplineId}")]
		public async Task<IActionResult> Update([FromServices] DisciplineService disciplineService, [Required] Guid disciplineId)
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<DisciplineVO> response = await disciplineService.GetDisciplineByDisciplineIdTaskAsync(disciplineId, token);

			return View("/Views/Coordinator/Disciplines/Update.cshtml", response);
		}

		[HttpPost("Update")]
		public async Task<IActionResult> UpdateDiscipline([FromServices] DisciplineService disciplineService, ResultModel<DisciplineVO> request)
		{
			string token = User.FindFirst("Token").Value;

			request.Object.CourseId = Guid.Parse(User.FindFirst("CourseId").Value);

			ResultModel<DisciplineVO> response = await disciplineService.UpdateDisciplineTaskAsync(request.Object, token);

			if (response.StatusCode != HttpStatusCode.Created)
				return View("/Views/Coordinator/Disciplines/Update.cshtml", new ResultModel<DisciplineVO>
				{
					Object = request.Object,
					Message = response.Message,
					StatusCode = response.StatusCode
				});

			ResultModel<List<DisciplineVO>> disciplinesResponse = await disciplineService.GetDisciplinesByCoordIdTaskAsync(token);

			if (disciplinesResponse.StatusCode == HttpStatusCode.OK)
			{
				disciplinesResponse.Message = response.Message;
				disciplinesResponse.StatusCode = response.StatusCode;
			}

			return View("/Views/Coordinator/Disciplines/Index.cshtml", disciplinesResponse);
		}

		[HttpPost("Delete/{disciplineId}")]
		public async Task<IActionResult> Delete([FromServices] DisciplineService disciplineService, [Required] Guid disciplineId)
		{
			if (ModelState.IsValid)
			{
				string token = User.FindFirst("Token").Value;

				ResultModel<bool> response = await disciplineService.DeleteDisciplineTaskAsync(disciplineId, token);

				ResultModel<List<DisciplineVO>> disciplinesResponse = await disciplineService.GetDisciplinesByCoordIdTaskAsync(token);

				if (disciplinesResponse.StatusCode == HttpStatusCode.OK)
				{
					disciplinesResponse.StatusCode = response.StatusCode;
					disciplinesResponse.Message = response.Message;
				}

				return View("/Views/Coordinator/Disciplines/Index.cshtml", disciplinesResponse);
			}

			return BadRequest();
		}
	}
}