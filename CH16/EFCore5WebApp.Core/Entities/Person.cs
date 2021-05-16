﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore5WebApp.Core.Entities {
    [Table("Persons", Schema = "dbo")]
    public class Person {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        public List<Address> Addresses { get; set; }
        [NotMapped]
        [Display(Name = "Name")]

        public string FullName => $"{FirstName} {LastName}";
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public List<Person> Parents { get; set; } = new List<Person>();
        public List<Person> Children { get; set; } = new List<Person>();

        public int Age { get; set; }
    }
}