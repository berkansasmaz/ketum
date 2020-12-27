using System;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;
using Xunit;

namespace Ketum.Monitors
{
   public class MonitorAppService_Tests : KetumApplicationTestBase
    {
        private readonly IMonitorAppService _monitorAppService;
        private readonly KetumTestData _testData;
        private readonly ICurrentUser _currentUser;


        public MonitorAppService_Tests()
        {
            _currentUser = Substitute.For<ICurrentUser>();
            _testData = GetRequiredService<KetumTestData>();
            _monitorAppService = GetRequiredService<IMonitorAppService>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            Login(_testData.UserId1);

            var monitors = await _monitorAppService.GetListAsync(new GetMonitorsRequestInput());

            monitors.ShouldNotBeNull();
            monitors.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetAsync()
        {
            Login(_testData.UserId1);

            var monitor = await _monitorAppService.GetAsync(_testData.MonitorId1, new GetMonitorRequestInput());

            monitor.ShouldNotBeNull();
            monitor.Id.ShouldBe(_testData.MonitorId1);
            monitor.Name.ShouldNotBeNullOrWhiteSpace();
            monitor.Name.ShouldBe(_testData.MonitorName1);
        }

        [Fact]
        public async Task GetAsync_Should_Throw_An_Exception_If_Can_Not_Find()
        {
            Login(_testData.UserId1);

            var randomMonitorId = Guid.NewGuid();

            await Assert.ThrowsAnyAsync<InvalidOperationException>(async () =>
            {
                await _monitorAppService.GetAsync(randomMonitorId, new GetMonitorRequestInput());
            });
        }

        [Fact]
        public async Task CreateAsync()
        {
            Login(_testData.UserId1);

             var newMonitor = new CreateMonitorDto
             {
                Name = "monitor-name",
                Url =   "monitor-url",
                Interval = 1
             };

             await _monitorAppService.CreateAsync(newMonitor);
        }

        [Fact]
        public async Task CreateAsync_Should_Throw_An_Exception_If_Same_Name()
        {
            Login(_testData.UserId1);

            var newMonitor = new CreateMonitorDto
            {
                Name = _testData.MonitorName1,
                Url =   "monitor-url",
                Interval = 1
            };

            await Assert.ThrowsAnyAsync<BusinessException>(async () =>
            {
                await _monitorAppService.CreateAsync(newMonitor);
            });
        }

        [Fact]
        public async Task UpdateAsync()
        {
            Login(_testData.UserId1);

            var monitor = new UpdateMonitorDto
            {
                Name = "updated-monitor-name",
                Url =   "updated-monitor-url",
                Interval = 1
            };

            await _monitorAppService.UpdateAsync(_testData.MonitorId1, monitor);
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_An_Exception_If_Same_Name()
        {
            Login(_testData.UserId1);

            var monitor = new UpdateMonitorDto
            {
                Name = _testData.MonitorName2,
                Url =   "updated-monitor-url",
                Interval = 1
            };

            await Assert.ThrowsAnyAsync<BusinessException>(async () =>
            {
                await _monitorAppService.UpdateAsync(_testData.MonitorId1, monitor);
            });
        }
        
        private void Login(Guid userId)
        {
            _currentUser.Id.Returns(userId);
            _currentUser.IsAuthenticated.Returns(true);
        }
    }
}