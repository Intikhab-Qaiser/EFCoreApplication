using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreApplication.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int StudentId { get; set; } // Foreign Key
        public virtual Student Student { get; set; } // Navigation Property
    }
}
