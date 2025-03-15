using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreRelationship.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        // Foreign Key
        //public int StudentId { get; set; }
        //public Student Student { get; set; }

        public List<StudentCourse> StudentCourses { get; set; } = new();
    }
}
