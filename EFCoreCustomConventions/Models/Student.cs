using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreInterceptor.Models
{
    public enum StudentStatus
    {
        Active,
        Inactive,
        Graduated
    }

    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal HeightInCm { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsEnrolled { get; set; }
        public StudentStatus Status { get; set; }
        public string SSN { get; set; } = string.Empty;
    }
}
