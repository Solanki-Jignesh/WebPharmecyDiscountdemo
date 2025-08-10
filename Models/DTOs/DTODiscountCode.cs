using System;
using System.ComponentModel.DataAnnotations;

namespace WebPharmecyDiscountdemo.Models.DTOs
{
    public class DTODiscountCode
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Discount code is required.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Discount value is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Value type is required.")]
        public string ValueType { get; set; }

        public int? TotalUsage { get; set; }

        public int? PerCustomerUsage { get; set; }

        public bool AppliesToAll { get; set; }

        public string[] AppliesToUserIds { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Minimum cart value must be greater than zero.")]
        [Required]
        public decimal? MinimumCartValue { get; set; }
        [Required]
        public DateTime? StartAt { get; set; }
        [Required]
        public DateTime? EndAt { get; set; }

        public int UseCount { get; set; }

        public bool IsActive { get; set; }
    }
}
