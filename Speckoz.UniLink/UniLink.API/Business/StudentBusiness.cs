using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Data.VO;
using UniLink.API.Repository.Interfaces;
using UniLink.API.Services.Email.Interfaces;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISendEmailService _emailSender;
        private readonly StudentConverter _converter;

        public StudentBusiness(IStudentRepository studentRepository, ISendEmailService sendEmailService)
        {
            _studentRepository = studentRepository;
            _emailSender = sendEmailService;
            _converter = new StudentConverter();
        }

        public async Task<StudentVO> AddTaskAsync(StudentVO student)
        {
            var studentEntity = _converter.Parse(student);
            await _studentRepository.AddTaskAsync(studentEntity);
            await _emailSender.SendEmailTaskAsync(studentEntity.Email);
            return _converter.Parse(studentEntity);
        }

        public async Task<StudentVO> FindByIdTaskAsync(Guid id) =>
            _converter.Parse(await _studentRepository.FindByIdTaskAsync(id));

        public async Task<IList<StudentVO>> FindAllByCoordIdAndCourseId(Guid coordId, Guid courseId)
        {
            var list = (List<StudentModel>) await _studentRepository.FindAllByCourseTaskAsync(coordId, courseId);
            return _converter.ParseList(list);
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            if (await _studentRepository.FindByIdTaskAsync(id) is StudentModel student)
                await _studentRepository.DeleteTaskAsync(student);
        }
    }
}