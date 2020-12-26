using System;
using System.Collections.Generic;
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

        private List<BreadcrumbItem> BreadcrumbItems = new List<BreadcrumbItem>();

        // TODO: Delete or edit lines below
        private string[] Labels;
        List<string> backgroundColors = new List<string> { ChartColor.FromRgba( 255, 99, 132, 0.2f ), ChartColor.FromRgba( 54, 162, 235, 0.2f ), ChartColor.FromRgba( 255, 206, 86, 0.2f ), ChartColor.FromRgba( 75, 192, 192, 0.2f ), ChartColor.FromRgba( 153, 102, 255, 0.2f ), ChartColor.FromRgba( 255, 159, 64, 0.2f ) };
        List<string> borderColors = new List<string> { ChartColor.FromRgba( 255, 99, 132, 1f ), ChartColor.FromRgba( 54, 162, 235, 1f ), ChartColor.FromRgba( 255, 206, 86, 1f ), ChartColor.FromRgba( 75, 192, 192, 1f ), ChartColor.FromRgba( 153, 102, 255, 1f ), ChartColor.FromRgba( 255, 159, 64, 1f ) };
        
        public MonitorDetails()
        {
            MonitorUpTimeChart = new LineChart<double>();
            MonitorLoadTimeChart = new LineChart<double>();
            Labels = new string[20];
        }
        
        protected override async Task OnInitializedAsync()
        {
            MonitorWithDetails = await MonitorAppService.GetAsync(Guid.Parse(Id));
            BreadcrumbItems.Add(new BreadcrumbItem(L["Monitor"].Value, "monitors"));
            BreadcrumbItems.Add(new BreadcrumbItem(L["Details"].Value));
            FillLabels();
            await HandleRedrawChartAsync();
        }

        private void FillLabels()
        {
            if (MonitorWithDetails.DateTimes.Count > 0)
            {
                Labels = MonitorWithDetails.DateTimes.ToArray();
            }
        }

        private async Task HandleRedrawChartAsync()
        {
            if (MonitorWithDetails.MonitorStep.MonitorStepLogs.Count > 1)
            {
                await MonitorUpTimeChart.Clear();
                await MonitorLoadTimeChart.Clear();

                await MonitorUpTimeChart.AddLabelsDatasetsAndUpdate(Labels, GetUpTimeLineChartDataset());
                await MonitorLoadTimeChart.AddLabelsDatasetsAndUpdate(Labels, GetLoadTimeLineChartDataset());
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