using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.API.Data.Converters.Student;
using UniLinks.API.Repository.Interfaces;
using UniLinks.API.Services;
using UniLinks.API.Services.Email.Interfaces;
using UniLinks.API.Utils;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Business
{
	public class StudentBusiness : IStudentBusiness
	{
		private readonly IStudentRepository _studentRepository;
		private readonly ISendEmailService _emailSender;
		private readonly GenerateTokenService _tokenService;
		private readonly StudentConverter _studentConverter;
		private readonly StudentDisciplineConverter _studentDisciplineConverter;
		private readonly IDisciplineRepository _disciplineRepository;

		public StudentBusiness(IStudentRepository studentRepository, ISendEmailService sendEmailService, GenerateTokenService tokenService, IDisciplineRepository disciplineRepository)
		{
			_studentRepository = studentRepository;
			_emailSender = sendEmailService;
			_tokenService = tokenService;
			_disciplineRepository = disciplineRepository;
			_studentConverter = new StudentConverter();
			_studentDisciplineConverter = new StudentDisciplineConverter();
		}

		public async Task<StudentDisciplineVO> AddTaskAsync(StudentVO student)
		{
			StudentModel studentEntity = _studentConverter.Parse(student);

			if (GuidFormat.TryParseList(studentEntity.Disciplines, ';', out List<Guid> result))
			{
				//checando se nao existe nenhuma disciplina repetida.
				foreach (Guid disc in result)
					if (result.Count(x => x.Equals(disc)) > 1)
						return null;

				List<DisciplineModel> disciplines = await _disciplineRepository.FindAllByRangeDisciplinesIdTaskASync(result);

				if (!disciplines.Contains(null))
				{
					StudentModel addedstudent = await _studentRepository.AddTaskAsync(studentEntity);

					await _emailSender.SendEmailTaskAsync(addedstudent.Email);

					return _studentDisciplineConverter.Parse((addedstudent, disciplines));
				}
			}

			return null;
		}

		public async Task<AuthStudentVO> AuthUserTaskAsync(string email)
		{
			if (await _studentRepository.FindByEmailTaskAsync(email) is StudentModel user)
			{
				var authStudentConverter = new AuthStudentConverter();
				AuthStudentVO userVO = authStudentConverter.Parse(user);
				userVO.Token = _tokenService.Generate(user.StudentId, UserTypeEnum.Student);

				return userVO;
			}

			return default;
		}

		public async Task<bool> ExistsByEmailTaskAsync(string email) =>
			await _studentRepository.ExistsByEmailTaskAsync(email);

		public async Task<StudentDisciplineVO> FindByStudentIdTaskAsync(Guid studentId)
		{
			StudentModel studentModel = await _studentRepository.FindByStudentIdTaskAsync(studentId);
			if (!GuidFormat.TryParseList(studentModel.Disciplines, ';', out List<Guid> result))
				return null;

			List<DisciplineModel> disciplines = await _disciplineRepository.FindAllByRangeDisciplinesIdTaskASync(result);

			return _studentDisciplineConverter.Parse((studentModel, disciplines));
		}

		public async Task<List<StudentDisciplineVO>> FindAllByCourseIdTaskAsync(Guid courseId)
		{
			if (await _studentRepository.FindAllByCourseIdTaskAsync(courseId) is List<StudentModel> students)
			{
				var studentDisciplines = new List<(StudentModel student, List<DisciplineModel> discipline)>();

				foreach (StudentModel student in students)
				{
					if (!GuidFormat.TryParseList(student.Disciplines, ';', out List<Guid> result))
						return null;

					if (await _disciplineRepository.FindAllByRangeDisciplinesIdTaskASync(result) is List<DisciplineModel> disciplines)
						studentDisciplines.Add((student, disciplines));
					else
						return null;
				}

				return _studentDisciplineConverter.ParseList(studentDisciplines);
			}

			return null;
		}

		public async Task<StudentDisciplineVO> UpdateTaskAsync(StudentVO newStudent)
		{
			if (!(await _studentRepository.FindByStudentIdTaskAsync(newStudent.StudentId) is StudentModel studentModel))
				return null;

			if (!(await _studentRepository.UpdateTaskAsync(studentModel, _studentConverter.Parse(newStudent)) is StudentModel newStudentModel))
				return null;

			if (!GuidFormat.TryParseList(newStudentModel.Disciplines, ';', out List<Guid> disciplines))
				return null;

			return _studentDisciplineConverter.Parse((newStudentModel, await _disciplineRepository.FindAllByRangeDisciplinesIdTaskASync(disciplines)));
		}

		public async Task DeleteTaskAsync(Guid id)
		{
			if (await _studentRepository.FindByStudentIdTaskAsync(id) is StudentModel student)
				await _studentRepository.DeleteAsync(student);
		}
	}
}