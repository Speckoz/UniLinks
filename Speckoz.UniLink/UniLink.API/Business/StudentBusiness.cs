using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Data.Converters.Student;
using UniLink.API.Repository.Interfaces;
using UniLink.API.Services;
using UniLink.API.Services.Email.Interfaces;
using UniLink.API.Utils;
using UniLink.Dependencies.Data.VO;
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

        public StudentBusiness(
            IStudentRepository studentRepository,
            ISendEmailService sendEmailService,
            GenerateTokenService tokenService,
            IDisciplineRepository disciplineRepository
        )
        {
            _studentRepository = studentRepository;
            _emailSender = sendEmailService;
            _tokenService = tokenService;
            _converter = new StudentConverter();
            _disciplineRepository = disciplineRepository;
            _studentDisciplineConverter = new StudentDisciplineConverter();
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

        public async Task<StudentVO> AddTaskAsync(StudentVO student)
        {
            StudentModel studentEntity = _converter.Parse(student);
            await _studentRepository.AddTaskAsync(studentEntity);
            await _emailSender.SendEmailTaskAsync(studentEntity.Email);
            return _converter.Parse(studentEntity);
        }

        public async Task<StudentVO> FindByIdTaskAsync(Guid id) =>
            _converter.Parse(await _studentRepository.FindByIdTaskAsync(id));

        public async Task<IList<StudentDisciplineVO>> FindAllByCoordIdAndCourseId(Guid coordId, Guid courseId)
        {
            var studentDisciplines = new List<(StudentModel student, IList<DisciplineModel> discipline)>();
            var students = await _studentRepository.FindAllByCourseTaskAsync(coordId, courseId);
            foreach (var student in students)
            {
                if (GuidFormat.TryParseList(student.Disciplines, ';', out IList<Guid> result))
                {
                    studentDisciplines.Add((student, await _disciplineRepository.FindByRangeIdTaskAsync(result)));
                }
                else
                    return null;
            }

            return _studentDisciplineConverter.ParseList(studentDisciplines);
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            if (await _studentRepository.FindByIdTaskAsync(id) is StudentModel student)
                await _studentRepository.DeleteTaskAsync(student);
        }
    }
}