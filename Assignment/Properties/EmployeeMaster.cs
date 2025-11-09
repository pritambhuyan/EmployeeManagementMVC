using System.ComponentModel.DataAnnotations;

namespace Assignment.Properties
{
    public class EmployeeMaster
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(10)]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public decimal Salary { get; set; }

        public bool Status { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
