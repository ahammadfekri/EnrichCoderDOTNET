using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WCRM.Models;

public partial class Employee
{
    [Key]
    public Int64 ID { get; set; }
    public string? Code { get; set; }

    [Required(ErrorMessage = "Enter Your Address")]
    [StringLength(10, ErrorMessage = "Name should be less than or equal to ten characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Enter Your Address")]
    [StringLength(10, ErrorMessage = "Name should be less than or equal to ten characters.")]
    public string? Address { get; set; }
}
