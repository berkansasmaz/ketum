using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Ketum.Monitors;
using Ketum.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using BreadcrumbItem = Volo.Abp.BlazoriseUI.BreadcrumbItem;

namespace Ketum.Blazor.Pages
{
    public partial class Monitors
    {
        private IReadOnlyList<MonitorDto> MonitorList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private GetMonitorsRequestInput Filter { get; set; }
        private bool CanCreateMonitor { get; set; }
        private CreateMonitorDto NewMonitor { get; set; }
        private Modal CreateMonitorModal { get; set; }
        private Guid EditingMonitorId { get; set; }
        private UpdateMonitorDto EditingMonitor { get; set; }
        private Modal EditMonitorModal { get; set; }

        private List<BreadcrumbItem> BreadcrumbItems = new List<BreadcrumbItem>();

        public Monitors()
        {
            NewMonitor = new CreateMonitorDto 
            {
                Interval = 1
            };
  
            EditingMonitor = new UpdateMonitorDto
            {
                Interval = 1
            };
            
            Filter = new GetMonitorsRequestInput
            {
                MaxResultCount = PageSize ,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            };
        }
        
        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            BreadcrumbItems.Add(new BreadcrumbItem(L["Monitor"].Value));
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateMonitor = await AuthorizationService
                .IsGrantedAsync(KetumPermissions.Monitors.Create);
        }
        
        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<MonitorDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;
            await GetMonitorAsync();
            StateHasChanged();
        }
        
        private async Task GetMonitorAsync()
        {
            var result = await MonitorAppService.GetListAsync(Filter);
            MonitorList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private void OpenDetailMonitor(MonitorDto input)
        {
            NavigationManager.NavigateTo($"monitor-details/{input.Id}");
        }
        
        private void OpenCreateMonitorModal()
        {
            NewMonitor = new CreateMonitorDto();
            NewMonitor.Interval = 1;
            CreateMonitorModal.Show();
        }
        
        private async Task CreateMonitorAsync()
        {
            await MonitorAppService.CreateAsync(NewMonitor);
            await GetMonitorAsync();
            CreateMonitorModal.Hide();
        }
        
        private void OpenEditMonitorModal(MonitorDto input)
        {
            EditingMonitorId = input.Id;
            EditingMonitor = ObjectMapper.Map<MonitorDto, UpdateMonitorDto>(input);
            EditMonitorModal.Show();
        }
        
        private async Task UpdateMonitorAsync()
        {
            await MonitorAppService.UpdateAsync(EditingMonitorId, EditingMonitor);
            await GetMonitorAsync();
            EditMonitorModal.Hide();
        }
        
        private async Task DeleteMonitorAsync(MonitorDto input)
        {
            await MonitorAppService.DeleteAsync(input.Id);
            await GetMonitorAsync();
        }
    }
}