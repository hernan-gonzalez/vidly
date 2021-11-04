using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        //DataAnnotations
        [StringLength(255)]
        public string name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }


        public MembershipTypeDto MembershipType { get; set; }

      
        public byte MembershipTypeID { get; set; }

      //  [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}