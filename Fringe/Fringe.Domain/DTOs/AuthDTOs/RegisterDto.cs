﻿using System.ComponentModel.DataAnnotations;

namespace Fringe.Domain.DTOs.AuthDTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
        
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
        
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
        
    [Required]
    public string FirstName { get; set; } = string.Empty;
        
    [Required]
    public string LastName { get; set; } = string.Empty;
        
    public string? PhoneNumber { get; set; }
}