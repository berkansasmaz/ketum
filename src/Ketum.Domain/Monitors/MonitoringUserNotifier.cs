using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;

namespace Ketum.Monitors
{
    public class MonitoringUserNotifier : ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly IIdentityUserRepository _identityUserRepository;

        public MonitoringUserNotifier(IEmailSender emailSender, IIdentityUserRepository identityUserRepository)
        {
            _emailSender = emailSender;
            _identityUserRepository = identityUserRepository;
        }

        public async Task NotifyAsync(Monitor monitor)
        {
            if (monitor.CreatorId == null)
            {
                return;
            }

            if (!monitor.MonitorStatus.IsIn(MonitorStatusTypes.Down, MonitorStatusTypes.Warning))
            {
                return;
            }

            var creatorUser = await _identityUserRepository.FindAsync(monitor.CreatorId.Value);
            if (creatorUser == null)
            {
                return;
            }

            await _emailSender.SendAsync(
                creatorUser.Email,
                GetEmailSubject(monitor),
                GetEmailBody(monitor)
            );
        }

        private string GetEmailSubject(Monitor monitor)
        {
            var prefix = "ALERT " + monitor.MonitorStatus + ": ";

            if (monitor.MonitorStatus == MonitorStatusTypes.Down)
            {
                return prefix + monitor.Name + " ( " +monitor.MonitorStep.Url + " ) " + "is DOWN!";
            }

            if (monitor.MonitorStatus == MonitorStatusTypes.Warning)
            {
                return prefix + monitor.Name + " ( " +monitor.MonitorStep.Url + " ) " + "is WARNING!";
            }

            throw new ApplicationException("Unexpected status: " + monitor.MonitorStatus);
        }

        private string GetEmailBody(Monitor monitor)
        {
            if (monitor.MonitorStatus == MonitorStatusTypes.Down)
            {
                return "Hello!<br/><br/>" +
                       $"<strong>Uptime monitor {monitor.Name} {monitor.MonitorStep.Url} is down.</strong><br/>" +
                       $"Time: {monitor.LastModificationTime!.Value.ToLongDateString() + " " + monitor.LastModificationTime!.Value.ToLongTimeString()} <br/>" + 
                       "We will inform you when it's up again! <br/></br>" + 
                       "Regards, <br/> Ketum";
            }

            if (monitor.MonitorStatus == MonitorStatusTypes.Warning)
            {
                return "Hello!<br/><br/>" +
                       $"<strong>Uptime monitor {monitor.Name} {monitor.MonitorStep.Url} is warning.</strong><br/>" +
                       $"Time: {monitor.LastModificationTime!.Value.ToLongDateString() + " " + monitor.LastModificationTime!.Value.ToLongTimeString()} <br/>" + 
                       "We will inform you when it's up again! <br/></br>" + 
                       "Regards, <br/> Ketum";
            }

            throw new ApplicationException("Unexpected status: " + monitor.MonitorStatus);
        }
    }
}