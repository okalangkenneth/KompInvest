using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KompInvest.Models;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser 
{
    

    [Required]
    public DateTime RegistrationDate { get; set; } 

    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; } // (Member/Admin)

    public bool IsVerified { get; set; } 

    [StringLength(200, ErrorMessage = "Profile Picture URL cannot be longer than 200 characters.")]
    public string ProfilePictureUrl { get; set; }
    
    public virtual ICollection<Blog> Blogs { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Event> Events { get; set; }
    public virtual ICollection<ForumPost> ForumPosts { get; set; }
    public virtual ICollection<Investment> Investments { get; set; }
    public virtual ICollection<Resource> Resources { get; set; }
    public virtual UserProfile UserProfile { get; set; }


    // Add error handling and further validation logic here.
}
