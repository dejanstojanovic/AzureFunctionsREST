using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AzureFunctionsREST.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings =false)]
        [MinLength(3)]
        [MaxLength(25)]
        public String FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(4)]
        [MaxLength(50)]
        public String LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Range(50, 300)]
        public double Height { get; set; }
    }
}
