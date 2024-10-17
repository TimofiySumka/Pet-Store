using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MySite.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }


}

