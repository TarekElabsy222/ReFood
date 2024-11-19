using Microsoft.AspNetCore.Identity;
using ReFood.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReFood.Entities.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public IsVegan IsVegan {get; set; }
        public Gender Gender { get; set; }
        public HasChildren HasChildren { get; set; }
        public MedicalConditions MedicalConditions { get; set; }
        public PreferredCuisine PreferredCuisine { get; set; }
    }
}
