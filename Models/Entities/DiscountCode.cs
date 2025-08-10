using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPharmecyDiscountdemo.Models.Entities
{
    public class DiscountCode
    {
        [Key]
        public int Id { get; set; }

       
        public string Code { get; set; }               

        
        public decimal Value { get; set; }            

      
        public string ValueType { get; set; }

        public int? TotalUsage { get; set; }            
        public int? PerCustomerUsage { get; set; }      

        public bool AppliesToAll { get; set; }

       
        public string AppliesToUserIds { get; set; }

        
        public decimal? MinimumCartValue { get; set; }
        public DateTime StartAt { get; set; }          
        public DateTime EndAt { get; set; }            
        public int UseCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; }



        public bool IsActive { get; set; } = true;

    }
}
