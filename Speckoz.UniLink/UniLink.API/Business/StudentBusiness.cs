using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.API.Business.Interfaces;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IStudentRepository _studentRepository;

        public StudentBusiness(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentModel> AddTaskAsync(StudentModel student) =>
            await _studentRepository.AddTaskAsync(student);

        public async Task DeleteTaskAsync(int id)
        {
            StudentModel student = await _studentRepository.FindByIdTaskAsync(id);
            if (student != null)
            {
                await _studentRepository.DeleteTaskAsync(student);
            }
        }

        public async Task<StudentModel> FindByEmailTaskAsync(string email) =>
            await _studentRepository.FindByEmailTaskAsync(email);
    }
}
