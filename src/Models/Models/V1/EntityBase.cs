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
    public class EntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; set;}
        public Date CreatedDate {get; set;}
        public Date UpdatedDate { get; set;}

    }

}