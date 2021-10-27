using Domain;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class StudentRepository : IStudentRepository
    {
        private DbFysioContext _context { get; set; }

        public StudentRepository(DbFysioContext dbFysioContext)
        {
            _context = dbFysioContext;
        }
        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Student GetWhereIdStudent(int id)
        {
            return _context.Students.Find(id);
        }
    }
}
