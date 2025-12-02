using Data.Interfaces;
using Domain;
using Domain.Statistics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Services;
using System;
using System.Windows;
using UI.Converters;

namespace UI
{
    public partial class StatisticsWindow : Window
    {
        private readonly StatisticsService _statisticsService;
        private readonly StatusConverter _statusConverter;

        public StatisticsWindow(StatisticsService statisticsService)
        {
            InitializeComponent();
            _statisticsService = statisticsService;
            _statusConverter = new StatusConverter();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            var filter = CreateFilter();
            LoadStatusChart(filter);
            LoadMechanicChart(filter);
            LoadMonthChart(filter);
            LoadCarTypeChart(filter); // ДОБАВЛЯЕМ ЗАГРУЗКУ СТАТИСТИКИ ПО ТИПАМ АВТО
        }

        private RepairRequestFilter CreateFilter()
        {
            return new RepairRequestFilter
            {
                StartDate = StartDatePicker.SelectedDate,
                EndDate = EndDatePicker.SelectedDate
            };
        }

        private void LoadStatusChart(RepairRequestFilter filter)
        {
            var data = _statisticsService.GetByStatus(filter);
            var plotModel = new PlotModel { Title = "Распределение по статусам" };

            var pieSeries = new PieSeries();
            foreach (var item in data)
            {
                string statusText = _statusConverter.Convert(item.Status, typeof(string), null, System.Globalization.CultureInfo.CurrentCulture) as string;
                pieSeries.Slices.Add(new PieSlice(statusText, item.Count));
            }

            plotModel.Series.Add(pieSeries);
            StatusPlotView.Model = plotModel;
        }

        private void LoadMechanicChart(RepairRequestFilter filter)
        {
            var data = _statisticsService.GetByMechanic(filter);
            var plotModel = new PlotModel { Title = "Заявки по механикам" };

            var barSeries = new BarSeries();
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "Количество" };

            foreach (var item in data)
            {
                barSeries.Items.Add(new BarItem { Value = item.Count });
                categoryAxis.Labels.Add(item.MechanicName);
            }

            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);
            plotModel.Series.Add(barSeries);
            MechanicPlotView.Model = plotModel;
        }

        private void LoadMonthChart(RepairRequestFilter filter)
        {
            var data = _statisticsService.GetByMonth(filter);
            var plotModel = new PlotModel { Title = "Динамика по месяцам" };

            var lineSeries = new LineSeries { MarkerType = MarkerType.Circle };
            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom, Title = "Месяцы" };
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Количество" };

            for (int i = 0; i < data.Count; i++)
            {
                lineSeries.Points.Add(new DataPoint(i, data[i].Count));
                categoryAxis.Labels.Add(data[i].GetMonthName());
            }

            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);
            plotModel.Series.Add(lineSeries);
            MonthPlotView.Model = plotModel;
        }

        // НОВЫЙ МЕТОД ДЛЯ СТАТИСТИКИ ПО ТИПАМ АВТОМОБИЛЕЙ
        private void LoadCarTypeChart(RepairRequestFilter filter)
        {
            var data = _statisticsService.GetByCarType(filter);
            var plotModel = new PlotModel { Title = "Заявки по типам автомобилей" };

            var barSeries = new BarSeries
            {
                FillColor = OxyColor.FromRgb(76, 175, 80) // Зеленый цвет
            };

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left, Title = "Типы автомобилей" };
            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "Количество заявок" };

            foreach (var item in data)
            {
                barSeries.Items.Add(new BarItem { Value = item.Count });
                categoryAxis.Labels.Add(item.CarType);
            }

            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);
            plotModel.Series.Add(barSeries);
            CarTypePlotView.Model = plotModel;
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadStatistics();
        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            LoadStatistics();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}