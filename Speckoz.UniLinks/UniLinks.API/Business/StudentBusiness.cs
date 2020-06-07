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

			if (!(GuidFormat.TryParseList(studentEntity.Disciplines, ';', out List<Guid> result)))
				return null;

			//checando se nao existe nenhuma disciplina repetida.
			foreach (Guid disc in result)
				if (result.Count(x => x.Equals(disc)) > 1)
					return null;

			if (!(await _disciplineRepository.FindAllByRangeDisciplinesIdTaskASync(result) is List<DisciplineModel> disciplines))
				return null;

			if (disciplines.Contains(null))
				return null;

			if (!(await _studentRepository.AddTaskAsync(studentEntity) is StudentModel addedStudent))
				return null;

			await _emailSender.SendEmailTaskAsync(addedStudent.Email);

			return _studentDisciplineConverter.Parse((addedStudent, disciplines));
		}

		public async Task<AuthStudentVO> AuthUserTaskAsync(string email)
		{
			if (!(await _studentRepository.FindByEmailTaskAsync(email) is StudentModel user))
				return null;

			AuthStudentVO userVO = new AuthStudentConverter().Parse(user);
			userVO.Token = _tokenService.Generate(user.StudentId, UserTypeEnum.Student);

			return userVO;
		}

		public async Task<bool> ExistsByEmailTaskAsync(string email) =>
			await _studentRepository.ExistsByEmailTaskAsync(email);

		public async Task<StudentDisciplineVO> FindByStudentIdTaskAsync(Guid studentId)
		{
			if (!(await _studentRepository.FindByStudentIdTaskAsync(studentId) is StudentModel studentModel))
				return null;

			if (!GuidFormat.TryParseList(studentModel.Disciplines, ';', out List<Guid> result))
				return null;

			if (!(await _disciplineRepository.FindAllByRangeDisciplinesIdTaskASync(result) is List<DisciplineModel> disciplines))
				return null;

			return _studentDisciplineConverter.Parse((studentModel, disciplines));
		}

		public async Task<List<StudentDisciplineVO>> FindAllByCourseIdTaskAsync(Guid courseId)
		{
			if (!(await _studentRepository.FindAllByCourseIdTaskAsync(courseId) is List<StudentModel> students))
				return null;

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