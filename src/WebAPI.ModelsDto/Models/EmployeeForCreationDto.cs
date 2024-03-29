﻿using WebAPIModels.Models.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPIModels.Models.V1
{
        public class EmployeeForCreationDto
        {
                [Required(ErrorMessage = "Employee name is a required field.")]
                [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
                public string Name { get; set; }
                [Required(ErrorMessage = "Age is a required field.")]
                public int Age { get; set; }
                [Required(ErrorMessage = "Position is a required field.")]
                [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
                public string Position { get; set; }
                public Guid CompanyId { get; set; }
                public Company Company { get; set; }
         }
}
