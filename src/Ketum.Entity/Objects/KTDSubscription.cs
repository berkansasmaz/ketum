using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ketum.Entity
{
    [Table("Subscription")]
    public class KTDSubscription
    {
        [Key]
        public Guid SubscriptionId { get; set; }
        public Guid SubscriptionTypeId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public KTDPaymentPeriodTypes PaymentPeriod { get; set; }
    }


    public enum KTDPaymentPeriodTypes : short
    {
        Unknown = 0,
        Monthly = 1,
        Yearly = 12,
        
    }
}