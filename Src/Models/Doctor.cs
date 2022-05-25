using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoneClinic.Models
{
    public class Doctor
    {
        [key]
        public int DoctorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [Required(ErrorMessage ="* required")]
        //public Gender Sex { get; set; }
        public string Sex { get; set; }
        [Required(ErrorMessage = "* required")]
        public string Specialization { get; set; }
        [Required(ErrorMessage = "* required")]
        public string VisitingHours { get; set; }
    }
}
