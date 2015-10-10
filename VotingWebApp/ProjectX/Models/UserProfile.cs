using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectX.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Name { get { return String.Format("{0} {1}", this.FirstName, this.LastName); } }

        public int Points { get; set; }

        public string CanvasColor;

        [Required]
        public string ApplicationUserId { get; set; }

       




    }
}