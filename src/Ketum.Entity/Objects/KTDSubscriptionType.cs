
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ketum.Entity
{
    [Table("SubscriptionType")]
    public class KTDSubscriptionType     
    {
        [Key]
        public Guid SubscriptionTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool IsPaid { get; set; }
        public decimal Price { get; set; }
    }
}