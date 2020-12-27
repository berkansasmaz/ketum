using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Charts;
using Ketum.Monitors;
using Microsoft.AspNetCore.Components;
using BreadcrumbItem = Volo.Abp.BlazoriseUI.BreadcrumbItem;

namespace Ketum.Blazor.Pages
{
    public partial class MonitorDetails
    {
        [Parameter] 
        public string Id { get; set; }

        private MonitorWithDetailsDto MonitorWithDetails { get; set; }
        private LineChart<double> MonitorUpTimeChart { get; set; }
        private LineChart<double> MonitorLoadTimeChart { get; set; }

        private List<BreadcrumbItem> BreadcrumbItems;

        // TODO: Delete or edit lines below
        private string[] Labels;
        List<string> backgroundColors = new List<string> { ChartColor.FromRgba( 255, 99, 132, 0.2f ), ChartColor.FromRgba( 54, 162, 235, 0.2f ), ChartColor.FromRgba( 255, 206, 86, 0.2f ), ChartColor.FromRgba( 75, 192, 192, 0.2f ), ChartColor.FromRgba( 153, 102, 255, 0.2f ), ChartColor.FromRgba( 255, 159, 64, 0.2f ) };
        List<string> borderColors = new List<string> { ChartColor.FromRgba( 255, 99, 132, 1f ), ChartColor.FromRgba( 54, 162, 235, 1f ), ChartColor.FromRgba( 255, 206, 86, 1f ), ChartColor.FromRgba( 75, 192, 192, 1f ), ChartColor.FromRgba( 153, 102, 255, 1f ), ChartColor.FromRgba( 255, 159, 64, 1f ) };
        
        public MonitorDetails()
        {
            MonitorUpTimeChart = new LineChart<double>();
            MonitorLoadTimeChart = new LineChart<double>();
            BreadcrumbItems = new List<BreadcrumbItem>();
            Labels = new string[20];
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
            MonitorWithDetails = await MonitorAppService.GetAsync(Guid.Parse(Id));
        }
        
        private async Task HandleRedrawChartAsync()
        {
            if (MonitorWithDetails is not null && MonitorWithDetails.UpTimes.Any() && MonitorWithDetails.LoadTimes.Any())
            {
                await MonitorUpTimeChart.Clear();
                await MonitorLoadTimeChart.Clear();

                FillLabels();

                await MonitorUpTimeChart.AddLabelsDatasetsAndUpdate(Labels, GetUpTimeLineChartDataset());
                await MonitorLoadTimeChart.AddLabelsDatasetsAndUpdate(Labels, GetLoadTimeLineChartDataset());
            }
        }
        
        private void FillLabels()
        {
            if (MonitorWithDetails.DateTimes.Any())
            {
                Labels = MonitorWithDetails.DateTimes.ToArray();
            }
        }

        private LineChartDataset<double> GetUpTimeLineChartDataset()
        {
            return new LineChartDataset<double>
            {
                Label = "UpTimes",
                Data = MonitorWithDetails.UpTimes,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                Fill = true,
                PointRadius = 2,
                BorderDash = new List<int> { }
            };
        }

        private LineChartDataset<double> GetLoadTimeLineChartDataset()
        {
            return new LineChartDataset<double>
            {
                Label = "LoadTimes",
                Data = MonitorWithDetails.LoadTimes,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                Fill = true,
                PointRadius = 2,
                BorderDash = new List<int> { }
            };
        }
    }
}