using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters.Student;
using UniLink.API.Repository.Interfaces;
using UniLink.API.Services;
using UniLink.API.Services.Email.Interfaces;
using UniLink.API.Utils;
using UniLink.Dependencies.Data.VO.Student;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class StudentBusiness : IStudentBusiness
	{
		private readonly IStudentRepository _studentRepository;
		private readonly ISendEmailService _emailSender;
		private readonly GenerateTokenService _tokenService;
		private readonly StudentConverter _converter;
		private readonly StudentDisciplineConverter _studentDisciplineConverter;
		private readonly IDisciplineRepository _disciplineRepository;

		public StudentBusiness(IStudentRepository studentRepository, ISendEmailService sendEmailService, GenerateTokenService tokenService, IDisciplineRepository disciplineRepository)
		{
			_studentRepository = studentRepository;
			_emailSender = sendEmailService;
			_tokenService = tokenService;
			_converter = new StudentConverter();
			_disciplineRepository = disciplineRepository;
			_studentDisciplineConverter = new StudentDisciplineConverter();
		}

		public async Task<StudentDisciplineVO> AddTaskAsync(StudentVO student)
		{
			StudentModel studentEntity = _converter.Parse(student);

			if (GuidFormat.TryParseList(studentEntity.Disciplines, ';', out IList<Guid> result))
			{
				//checando se nao existe nenhuma disciplina repetida.
				foreach (Guid disc in result)
					if (result.Count(x => x.Equals(disc)) > 1)
						return null;

				IList<DisciplineModel> disciplines = await _disciplineRepository.FindByRangeIdTaskAsync(result);

				if (!disciplines.Contains(null))
				{
					StudentModel addedstudent = await _studentRepository.AddTaskAsync(studentEntity);

					//await _emailSender.SendEmailTaskAsync(addedstudent.Email);

					return _studentDisciplineConverter.Parse((addedstudent, disciplines));
				}
			}

			return null;
		}

		public async Task<StudentVO> AuthUserTaskAsync(string email)
		{
			if (await _studentRepository.FindByEmailTaskAsync(email) is StudentModel user)
			{
				StudentVO userVO = _converter.Parse(user);
				userVO.Token = _tokenService.Generate(user.StudentId, UserTypeEnum.Student);

				return userVO;
			}

			return default;
		}

		public async Task<bool> ExistsByEmailTaskAsync(string email) =>
			await _studentRepository.ExistsByEmailTaskAsync(email);

		public async Task<StudentVO> FindByIdTaskAsync(Guid id) =>
			_converter.Parse(await _studentRepository.FindByIdTaskAsync(id));

		public async Task<IList<StudentDisciplineVO>> FindAllByCoordIdAndCourseId(Guid coordId, Guid courseId)
		{
			if (await _studentRepository.FindAllByCourseTaskAsync(coordId, courseId) is IList<StudentModel> students)
			{
				var studentDisciplines = new List<(StudentModel student, IList<DisciplineModel> discipline)>();

				foreach (StudentModel student in students)
				{
					if (!GuidFormat.TryParseList(student.Disciplines, ';', out IList<Guid> result))
						return null;

					if (await _disciplineRepository.FindByRangeIdTaskAsync(result) is IList<DisciplineModel> disciplines)
						studentDisciplines.Add((student, disciplines));
					else
						return null;
				}

				return _studentDisciplineConverter.ParseList(studentDisciplines);
			}

			return null;
		}

		public async Task DeleteTaskAsync(Guid id)
		{
			if (await _studentRepository.FindByIdTaskAsync(id) is StudentModel student)
				await _studentRepository.DeleteTaskAsync(student);
		}

		public async Task<StudentVO> UpdateTaskAsync(StudentVO newStudent)
		{
			if (await _studentRepository.FindByIdTaskAsync(newStudent.StudentId) is StudentModel oldStudent)
				return _converter.Parse(await _studentRepository.UpdateTaskAsync(oldStudent, _converter.Parse(newStudent)));

			return null;
		}
	}
}