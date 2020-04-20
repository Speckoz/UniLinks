using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Repository.Interfaces;
using UniLink.API.Services.Email.Interfaces;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class StudentBusiness : IStudentBusiness
	{
		private readonly IStudentRepository _studentRepository;
		private readonly ISendEmailService _emailSender;

		public StudentBusiness(IStudentRepository studentRepository, ISendEmailService sendEmailService)
		{
			_studentRepository = studentRepository;
			_emailSender = sendEmailService;
		}

		public async Task<StudentModel> AddTaskAsync(StudentModel student)
		{
			StudentModel createdStudent =  await _studentRepository.AddTaskAsync(student);
			await _emailSender.SendEmailTaskAsync(createdStudent.User.Email);
			return createdStudent;
		}

		public async Task<StudentModel> FindByIdTaskAsync(int id) =>
			await _studentRepository.FindByIdTaskAsync(id);

		public async Task<IList<StudentModel>> FindAllByCoordIdAndCourseId(Guid coordId, Guid courseId) =>
			await _studentRepository.FindAllByCourseTaskAsync(coordId, courseId);

		public async Task DeleteTaskAsync(int id)
		{
			if (await _studentRepository.FindByIdTaskAsync(id) is StudentModel student)
				await _studentRepository.DeleteTaskAsync(student);
		}
	}
}