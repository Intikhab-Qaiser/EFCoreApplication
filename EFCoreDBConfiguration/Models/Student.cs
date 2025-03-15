using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDBConfiguration.Models
{
    public class Student
    {
        [Key]  // Primary Key
        public int Id { get; set; }

        [Required]  // NOT NULL constraint
        [MaxLength(100)]  // NVARCHAR(100)
        public string Name { get; set; } = string.Empty;

        [Range(18, 60)]  // Age must be between 18 and 60
        public int Age { get; set; }

        [EmailAddress]  // Email validation
        public string Email { get; set; } = string.Empty;

        [ForeignKey("Address")]  // Foreign Key
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
