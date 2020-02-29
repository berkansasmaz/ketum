using System;
using System.Threading.Tasks;
using Ketum.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ketum.Web.Controllers
{
    public class SetupController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!await Db.SubscriptionTypes.AnyAsync())
            {
                //Free
                var subscriptionTypeFree = new KTDSubscriptionType
                {
                    SubscriptionTypeId = Guid.NewGuid(),
                    IsPaid = false,
                    Name = "free",
                    Price = 0,
                    Title = "Free",
                    Description = "Free for starter users."
                };
                await Db.SubscriptionTypes.AddAsync(subscriptionTypeFree);
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, 1, "monitor", "MONITOR", "1");
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, 2, "monitor step", "MONITOR_Step", "1");
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, 3, "user", "USER", "1");
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, 4, "interval", "INTERVAL", "300");
                await AddFeature(subscriptionTypeFree.SubscriptionTypeId, 5, "alert channel", "ALERT_CHANNEL", "1");

                // Startup
                var subscriptionTypeStartup = new KTDSubscriptionType
                {
                    SubscriptionTypeId = Guid.NewGuid(),
                    IsPaid = true,
                    Name = "startup",
                    Price = 9.99m,
                    Title = "Startup",
                    Description = "For Startup."
                };
                await Db.SubscriptionTypes.AddAsync(subscriptionTypeStartup);
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, 1,"monitor", "MONITOR", "5");
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, 2,"monitor step", "MONITOR_Step", "10");
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, 3,"user", "USER", "1");
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, 4,"interval", "INTERVAL", "300");
                await AddFeature(subscriptionTypeStartup.SubscriptionTypeId, 5,"alert channel", "ALERT_CHANNEL", "2");


                // Premium
                var subscriptionTypePremium = new KTDSubscriptionType
                {
                    SubscriptionTypeId = Guid.NewGuid(),
                    IsPaid = true,
                    Name = "premium",
                    Price = 20.99m,
                    Title = "Premium",
                    Description = "For growing companies."
                };
                await Db.SubscriptionTypes.AddAsync(subscriptionTypePremium);
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, 1, "monitor", "MONITOR", "25");
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, 2, "monitor step", "MONITOR_Step", "100");
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, 3, "user", "USER", "5");
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, 4, "interval", "INTERVAL", "60");
                await AddFeature(subscriptionTypePremium.SubscriptionTypeId, 5, "alert channel", "ALERT_CHANNEL", "5");


                // enterprise
                var subscriptionTypeEnterprise = new KTDSubscriptionType
                {
                    SubscriptionTypeId = Guid.NewGuid(),
                    IsPaid = true,
                    Name = "enterprise",
                    Price = 80.99m,
                    Title = "Enterprise",
                    Description = "For enterprise companies."
                };
                await Db.SubscriptionTypes.AddAsync(subscriptionTypeEnterprise);
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, 1, "monitor", "MONITOR", "100");
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, 2, "monitor step", "MONITOR_Step", "250");
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, 3, "user", "USER", "25");
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, 4, "interval", "INTERVAL", "60");
                await AddFeature(subscriptionTypeEnterprise.SubscriptionTypeId, 5, "alert channel", "ALERT_CHANNEL", "*");


                if (await Db.SaveChangesAsync() > 0) return Success("I have bootstrapped successfully.");
            }

            return BadRequest("Done.");
        }

        [NonAction]
        private async Task AddFeature(Guid subscriptionTypeId, short sort, string title, string name, string value)
        {
            await Db.AddAsync(new KTDSubscriptionTypeFeature
            {
                SubscriptionTypeFeatureId = Guid.NewGuid(),
                SubscriptionTypeId = subscriptionTypeId,
                Description = string.Empty,
                IsFeature = true,
                Name = name,
                Title = title,
                Value = value,
                Sort = sort
            });
        }
    }
}
