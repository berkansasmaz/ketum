using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Charts;
using Ketum.Monitors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using BreadcrumbItem = Volo.Abp.BlazoriseUI.BreadcrumbItem;

namespace Ketum.Blazor.Pages
{
    public partial class MonitorDetails
    {
        [Parameter] 
        public string Id { get; set; }

        private MonitorWithDetailsDto MonitorWithDetails { get; set; }
        private int TotalMonitorStepLogCount { get; set; }
        private GetMonitorRequestInput Filter { get; set; }
        private LineChart<double> MonitorUpTimeChart { get; set; }
        private LineChart<double> MonitorLoadTimeChart { get; set; }
        private List<string> Labels;

        private List<BreadcrumbItem> BreadcrumbItems;

        public MonitorDetails()
        {
            MonitorWithDetails = new MonitorWithDetailsDto();
            MonitorUpTimeChart = new LineChart<double>();
            MonitorLoadTimeChart = new LineChart<double>();
            Filter = new GetMonitorRequestInput
            {
                SkipCount = 0,
                MaxResultCount = 20
            };
            BreadcrumbItems = new List<BreadcrumbItem>();
            Labels = new List<string>();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetMonitorAsync();
            BreadcrumbItems.Add(new BreadcrumbItem(L["Monitor"].Value, "monitors"));
            BreadcrumbItems.Add(new BreadcrumbItem(L["Details"].Value));
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await HandleRedrawChartAsync();
        }
        
        private async Task GetMonitorAsync()
        {
            MonitorWithDetails = await MonitorAppService.GetAsync(Guid.Parse(Id), Filter);
            TotalMonitorStepLogCount = await MonitorAppService.GetMonitorStepLogCountAsync(MonitorWithDetails.MonitorStep.Id);
        }
        
        private async ValueTask<ItemsProviderResult<MonitorStepLogDto>> LoadMonitorAsync(ItemsProviderRequest request)
        {
            var numStepLogs = Math.Min(request.Count, TotalMonitorStepLogCount - request.StartIndex);
            Filter.SkipCount = request.StartIndex;
            Filter.MaxResultCount = numStepLogs;
            MonitorWithDetails = await MonitorAppService.GetAsync(Guid.Parse(Id), Filter);
            
            return new ItemsProviderResult<MonitorStepLogDto>(MonitorWithDetails.MonitorStep.MonitorStepLogs, TotalMonitorStepLogCount);
        }
        
        private async Task HandleRedrawChartAsync()
        {
            if (MonitorWithDetails is not null && MonitorWithDetails.UpTimes.Any() && MonitorWithDetails.LoadTimes.Any())
            {
                await MonitorUpTimeChart.Clear();
                await MonitorLoadTimeChart.Clear();

                if (MonitorWithDetails.DateTimes.Any())
                {
                    Labels = MonitorWithDetails.DateTimes;
                }

                await MonitorUpTimeChart.AddLabelsDatasetsAndUpdate(Labels, GetUpTimeLineChartDataset());
                await MonitorLoadTimeChart.AddLabelsDatasetsAndUpdate(Labels, GetLoadTimeLineChartDataset());
            }
        }

        private LineChartDataset<double> GetUpTimeLineChartDataset()
        {
            return new LineChartDataset<double>
            {
                Label = L["Uptimes"].Value,
                Data = MonitorWithDetails.UpTimes,
                BackgroundColor = new List<string> { ChartColor.FromRgba( 97, 1, 238, 0.2f ) },
                BorderColor = new List<string> { ChartColor.FromRgba( 97, 1, 238, 1f ) },
                Fill = true,
                PointRadius = 2,
                BorderDash = new List<int> { }
            };
        }

        private LineChartDataset<double> GetLoadTimeLineChartDataset()
        {
            return new LineChartDataset<double>
            {
                Label = L["LoadTime"].Value,
                Data = MonitorWithDetails.LoadTimes,
                BackgroundColor =  new List<string> { ChartColor.FromRgba( 0, 218, 197, 0.2f ) },
                BorderColor =  new List<string> { ChartColor.FromRgba( 0, 218, 197, 1f ) },
                Fill = true,
                PointRadius = 2,
                BorderDash = new List<int> { }
            };
        }
    }
}