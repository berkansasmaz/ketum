using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;

namespace Ketum.Monitors
{
    public class MonitorUserNotifier : ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly IIdentityUserRepository _identityUserRepository;

        public MonitorUserNotifier(IEmailSender emailSender, IIdentityUserRepository identityUserRepository)
        {
            _emailSender = emailSender;
            _identityUserRepository = identityUserRepository;
        }

        public async Task NotifyAsync(MonitorEto monitorEto)
        {
            if (monitorEto.CreatorId == null)
            {
                return;
            }

            if (!monitorEto.MonitorStatus.IsIn(MonitorStatusTypes.Down, MonitorStatusTypes.Warning))
            {
                return;
            }

            var creatorUser = await _identityUserRepository.FindAsync(monitorEto.CreatorId.Value);
            if (creatorUser == null)
            {
                return;
            }

            await _emailSender.SendAsync(
                creatorUser.Email,
                GetEmailSubject(monitorEto),
                GetEmailBody(monitorEto)
            );
        }

        private string GetEmailSubject(MonitorEto monitorEto)
        {
            var prefix = "ALERT " + monitorEto.MonitorStatus + ": ";

            if (monitorEto.MonitorStatus == MonitorStatusTypes.Down)
            {
                return prefix + monitorEto.Name + " ( " + monitorEto.Url + " ) " + "is DOWN!";
            }

            if (monitorEto.MonitorStatus == MonitorStatusTypes.Warning)
            {
                return prefix + monitorEto.Name + " ( " + monitorEto.Url + " ) " + "is WARNING!";
            }

            throw new ApplicationException("Unexpected status: " + monitorEto.MonitorStatus);
        }

        private string GetEmailBody(MonitorEto monitorEto)
        {
            if (monitorEto.MonitorStatus == MonitorStatusTypes.Down)
            {
                return "Hello!<br/><br/>" +
                       $"<strong>Uptime monitor {monitorEto.Name} {monitorEto.Url} is down.</strong><br/>" +
                       $"Time: {monitorEto.LastModificationTime!.Value.ToLongDateString() + " " + monitorEto.LastModificationTime!.Value.ToLongTimeString()} <br/>" +
                       "We will inform you when it's up again! <br/></br>" +
                       "Regards, <br/> Ketum";
            }

            if (monitorEto.MonitorStatus == MonitorStatusTypes.Warning)
            {
                return "Hello!<br/><br/>" +
                       $"<strong>Uptime monitor {monitorEto.Name} {monitorEto.Url} is warning.</strong><br/>" +
                       $"Time: {monitorEto.LastModificationTime!.Value.ToLongDateString() + " " + monitorEto.LastModificationTime!.Value.ToLongTimeString()} <br/>" +
                       "We will inform you when it's up again! <br/></br>" +
                       "Regards, <br/> Ketum";
            }

            throw new ApplicationException("Unexpected status: " + monitorEto.MonitorStatus);
        }
    }
}