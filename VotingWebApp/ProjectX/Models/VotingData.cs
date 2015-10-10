using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectX.Models
{
    public class VotingData
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Clarity")]
        public string Clarity { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Delivery")]
        public string Delivery { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        
        public string UserProfileId { get; set; }

        [Required]
        public string CompanyName { get; set; }

    }
}