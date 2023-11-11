using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public partial class Member : IdentityUser<int>
    {
        public Member()
        {
            Orders = new HashSet<Order>();
        }

        [StringLength(15)]
        public string FirstName { get; set; } = null!;
        [StringLength(15)]
        public string LastName { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
