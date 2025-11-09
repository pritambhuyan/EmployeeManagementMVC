using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models;

public partial class EmployeeMaster
{
    public int EmployeeId { get; set; }
    [Required]
    public string? EmployeeCode { get; set; }
    [Required]
    public string? EmployeeName { get; set; }
    [Required]
    public string? Department { get; set; }
    [Required]
    public string? Designation { get; set; }
    [Required]
    public DateTime? JoiningDate { get; set; }
    [Required]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Enter valid numbers only.")]
    public decimal? Salary { get; set; }
    public bool? Status { get; set; }
    public DateTime? CreatedDate { get; set; }
}
