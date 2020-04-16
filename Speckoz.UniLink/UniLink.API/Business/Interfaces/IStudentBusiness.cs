using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business.Interfaces
{
    public interface IStudentBusiness
    {
        Task<StudentModel> FindByEmailTaskAsync(string email);
        Task<StudentModel> AddTaskAsync(StudentModel student);
        Task DeleteTaskAsync(int id);
    }
}
