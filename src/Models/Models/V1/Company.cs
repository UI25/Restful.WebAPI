using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPIModels.Models.V1
{
<<<<<<< HEAD
    public class Company : BaseEntity 
    {

=======
    public class Company : EntityBase
    {        
>>>>>>> 18e5f3668b7ada15b32d5851ad6617ce0bb3c487
        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 characte")]
<<<<<<< HEAD
        public string Address { get; set; }
        public string Country { get; set; }
        public ICollection<Employee> Employees { get; set; }
=======
        public string? Address { get; set; }
        public string? Country { get; set; }
        
        public ICollection<Employee>? Employees { get; set; }
>>>>>>> 18e5f3668b7ada15b32d5851ad6617ce0bb3c487
    }

}
