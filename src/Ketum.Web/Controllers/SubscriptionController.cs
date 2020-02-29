using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ketum.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace Ketum.Web.Controllers
{
    public class SubscriptionController : ApiController
    {
        [HttpGet("current")]
        public async Task<IActionResult> Current()
        {
            var subscription = await Db.Subscriptions.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (subscription == null)
            {
                return Error("Subscription not found.", code: 404);
            }
            var featureList = await Db.SubscriptionFeatures.Where(x => x.SubscriptionId == subscription.SubscriptionId).ToListAsync();
            var features = new List<dynamic>();
            foreach (var feature in featureList)
            {
                features.Add(new
                {
                    feature.Title,
                    feature.Description,
                    feature.Value,
                    feature.ValueUsed,
                    feature.ValueRemained
                });
            }
            return Success(data: new
            {
                id = subscription.SubscriptionId,
                typeId = subscription.SubscriptionTypeId,
                subscription.StartDate,
                subscription.EndDate,
                subscription.PaymentPeriod,
                PaymentPeriodText = subscription.PaymentPeriod.ToString(),
                features
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subscriptionTypes = await Db.SubscriptionTypes.OrderBy(x => x.Price).ToListAsync();
            var list = new List<dynamic>();

            foreach (var subscription in subscriptionTypes)
            {
                var featureList = await Db.SubscriptionTypeFeatures
                    .Where(x => x.SubscriptionTypeId == subscription.SubscriptionTypeId)
                    .OrderBy(x => x.Sort)
                    .ToListAsync();

                var features = new List<dynamic>();
                foreach (var feature in featureList)
                {
                    features.Add(new
                    {
                        feature.Title,
                        feature.Description,
                        feature.IsFeature,
                        feature.Value
                    });
                }

                list.Add(new
                {
                    id = subscription.SubscriptionTypeId,
                    subscription.IsPaid,
                    subscription.Description,
                    subscription.Name,
                    subscription.Price,
                    subscription.Title,
                    features
                });
            }
            return Success(null, list);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromRoute]Guid id)
        {
            if (id != Guid.Empty)
            {
                var subscriptionType = await Db.SubscriptionTypes.FirstOrDefaultAsync(x => x.SubscriptionTypeId == id);
                if (subscriptionType == null)
                {
                    return Error("Subscription type not found", code: 404);
                }

                var subscription = await Db.Subscriptions.FirstOrDefaultAsync(x => x.UserId == UserId);

                if (subscription != null)
                {
                    if (subscription.SubscriptionTypeId == subscriptionType.SubscriptionTypeId)
                    {
                        return Error("You have already this subscription.");
                    }

                    return Success("We will add this feature.");
                }
                else
                {
                    subscription = new KTDSubscription
                    {
                        SubscriptionId = Guid.NewGuid(),
                        SubscriptionTypeId = subscriptionType.SubscriptionTypeId,
                        UserId = UserId,
                        StartDate = DateTime.UtcNow,
                        EndDate = subscriptionType.IsPaid ? DateTime.UtcNow.AddMonths(1) : DateTime.MinValue,
                        PaymentPeriod = KTDPaymentPeriodTypes.Monthly
                    };
                    await Db.AddAsync(subscription);

                    var features = await Db.SubscriptionTypeFeatures.Where(x => x.SubscriptionTypeId == subscriptionType.SubscriptionTypeId).ToListAsync();
                    foreach (var feature in features)
                    {
                        await Db.AddAsync(new KTDSubscriptionFeature
                        {
                            SubscriptionFeatureId = Guid.NewGuid(),
                            SubscriptionId = subscription.SubscriptionId,
                            SubscriptionTypeId = subscriptionType.SubscriptionTypeId,
                            SubscriptionTypeFeatureId = feature.SubscriptionTypeFeatureId,
                            Description = feature.Description,
                            Name = feature.Name,
                            Value = feature.Value,
                            Title = feature.Title,
                            ValueUsed = string.Empty,
                            ValueRemained = string.Empty
                        });
                    }
                }

                if (await Db.SaveChangesAsync() > 0)
                {
                    return Success("Your subscription has been updated.");
                }
                return Error("There is nothing to save.");
            }
            return Error("You must send subscription id.");
        }
    }
}
