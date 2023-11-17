using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KompInvest.Models;
using Microsoft.AspNetCore.Identity;

public class User
{
    [Key]
    public int UserID { get; set; }

    [Required, StringLength(50)]
    public string Username { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; }

    [Required, StringLength(50)]
    public string FirstName { get; set; }

    [Required, StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public DateTime DateJoined { get; set; }

    [Required]
    public bool IsAdmin { get; set; }

    // Navigation Property for MemberProfile
    public virtual MemberProfile MemberProfile { get; set; }
}
