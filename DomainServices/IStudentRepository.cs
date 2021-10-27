using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IStudentRepository
    {
        public Student GetWhereIdStudent(int id);
        public IEnumerable<Student> GetAllStudents();

    }
}
