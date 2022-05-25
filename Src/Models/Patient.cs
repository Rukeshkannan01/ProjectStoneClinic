using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoneClinic.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public Gender Sex { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string DateOfBirth { get; set; }
    }
}
