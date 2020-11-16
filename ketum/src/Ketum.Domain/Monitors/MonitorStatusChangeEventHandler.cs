using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Ketum.Monitors
{
    public class MonitorStatusChangeEventHandler : IDistributedEventHandler<MonitorEto>, ITransientDependency
    {
        private readonly MonitorUserNotifier _monitorUserNotifier;

        public MonitorStatusChangeEventHandler(MonitorUserNotifier monitorUserNotifier)
        {
            _monitorUserNotifier = monitorUserNotifier;
        }

        public async Task HandleEventAsync(MonitorEto monitorEto)
        {
            await _monitorUserNotifier.NotifyAsync(monitorEto);
        }
    }
}