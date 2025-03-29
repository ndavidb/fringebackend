﻿using System.ComponentModel.DataAnnotations;

namespace Fringe.Domain.DTOs.AuthDTOs;

public class ResetPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
        
    [Required]
    public string Token { get; set; } = string.Empty;
        
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; } = string.Empty;
        
    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = string.Empty;
}