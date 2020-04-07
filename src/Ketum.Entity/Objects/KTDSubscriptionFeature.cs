using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ketum.Entity
{
    [Table("SubscriptionFeature")]
    public class KTDSubscriptionFeature
    {
        [Key] public Guid SubscriptionFeatureId { get; set; }

        public Guid SubscriptionId { get; set; }
        public Guid SubscriptionTypeFeatureId { get; set; }
        public Guid SubscriptionTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ValueUsed { get; set; }
        public string ValueRemained { get; set; }
        
        public virtual KTDSubscriptionTypeFeature SubscriptionTypeFeature { get; set; }
    }
}