using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIModels.Models.V1
{
    public class EmployeesView
    {
        [Column("EmployeeId")]
        [Required]
        public Guid EmployeeId { get; set; }
        [Column("EmployeeName")]
        public string? EmployeeName { get; set; }
        public int Age { get; set; }
        public string? Position { get; set; }
        [Column("CompanyName")]
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
