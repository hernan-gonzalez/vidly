using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        //DataAnnotations
        [Required(ErrorMessage ="Please enter customer's name.")]
        [StringLength(255)]
        public string name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }
        
        public MembershipType MembershipType { get; set; }


        [Display(Name ="Membership Type")]
        public byte MembershipTypeID { get; set; }

        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }


    }
}