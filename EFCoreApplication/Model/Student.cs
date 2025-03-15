using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreApplication.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        public virtual List<Course> Courses { get; set; } = new();

        [Timestamp] // Enables concurrency control
        public byte[] RowVersion { get; set; } = new byte[0];
    }
}
