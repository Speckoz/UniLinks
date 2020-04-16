using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<StudentModel> FindByEmailTaskAsync(string email);
        Task<StudentModel> FindByIdTaskAsync(int id);
        Task<StudentModel> AddTaskAsync(StudentModel student);
        Task DeleteTaskAsync(StudentModel student);
    }
}
