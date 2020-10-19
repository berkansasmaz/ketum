using System;
using System.Threading.Tasks;
using Ketum.Monitors;
using NSubstitute;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Users;

namespace Ketum
{
    public class KetumTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IMonitorRepository _monitorRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly KetumTestData _testData;
        private readonly ICurrentUser _currentUser;

        public KetumTestDataSeedContributor(
            IMonitorRepository monitorRepository, 
            IGuidGenerator guidGenerator,
            KetumTestData testData)
        {
            _monitorRepository = monitorRepository;
            _guidGenerator = guidGenerator;
            _testData = testData;
            _currentUser = Substitute.For<ICurrentUser>();
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await BuildMonitorsAsync();
        }

        private async Task BuildMonitorsAsync()
        {
            Login(_testData.UserId1);

            var newMonitorStep = new MonitorStep(
                _testData.MonitorStepId1,
                _testData.MonitorId1,
                _testData.MonitorUrl1,
                _testData.MonitorInterval1,
                MonitorStepTypes.Request,
                MonitorStepStatusTypes.Unknown);

            var monitor = new Monitor(
                _testData.MonitorId1,
                _testData.MonitorName1,
                MonitorStatusTypes.Unknown,
                newMonitorStep
            );

            await _monitorRepository.InsertAsync(monitor, true);

            var newMonitorStep2 = new MonitorStep(
                _testData.MonitorStepId2,
                _testData.MonitorId2,
                _testData.MonitorUrl2,
                _testData.MonitorInterval2,
                MonitorStepTypes.Request,
                MonitorStepStatusTypes.Unknown);

            var monitor2 = new Monitor(
                _testData.MonitorId2,
                _testData.MonitorName2,
                MonitorStatusTypes.Unknown,
                newMonitorStep2
            );


            await _monitorRepository.InsertAsync(monitor2, true);
        }

        private void Login(Guid userId)
        {
            _currentUser.Id.Returns(userId);
            _currentUser.IsAuthenticated.Returns(true);
        }
    }
}