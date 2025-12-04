using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using Domain;
using Data.Interfaces;
using Services;
using Microsoft.EntityFrameworkCore; // ДОБАВИТЬ ЭТУ СТРОЧКУ
using Data.SqlServer; 

namespace UI
{
    public partial class MainWindow : Window
    {
        private readonly IRepairRequestRepository _requestRepository;
        private RepairRequest _selectedRequest;
        private IEnumerable<RepairRequest> _allreqs;

        
        public MainWindow(IRepairRequestRepository _repairRequestRepository)
        {
            InitializeComponent();

            _requestRepository = _repairRequestRepository;
            RefreshDataGrid();
        }

        

        private void RefreshDataGrid()
        {
            
            _allreqs = _requestRepository.GetAll(new RepairRequestFilter());
            requestsGrid.ItemsSource = _allreqs;
            totalRequestsText.Text = _allreqs.Count().ToString();
            statusText.Text = "Данные обновлены";
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEditRequestWindow();
            if (addWindow.ShowDialog() == true)
            {
                var newRequest = addWindow.Request;
                _requestRepository.Add(newRequest);
                RefreshDataGrid();
            }
        }

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRequest == null)
            {
                MessageBox.Show("Выберите заявку для редактирования", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            

            var editWindow = new AddEditRequestWindow(_selectedRequest);
            if (editWindow.ShowDialog() == true)
            {
                
                MessageBox.Show($"Editing ID: {editWindow.Request.Id}, Type: {editWindow.Request.Id.GetType().Name}",
                    "Отладка", MessageBoxButton.OK, MessageBoxImage.Information);

                _requestRepository.Update(editWindow.Request);
                RefreshDataGrid();
            }
        }

        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRequest == null)
            {
                MessageBox.Show("Выберите заявку для удаления", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Вы уверены, что хотите удалить заявку №{_selectedRequest.Id}?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _requestRepository.Delete(_selectedRequest.Id);
                RefreshDataGrid();
                MessageBox.Show("Заявка успешно удалена", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RequestsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (requestsGrid.SelectedItem is RepairRequest request)
            {
                _selectedRequest = request;
            }
            else
            {
                _selectedRequest = null;
            }
        }


        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var statisticsService = new StatisticsService(_requestRepository);
                var statisticsWindow = new StatisticsWindow(statisticsService);
                statisticsWindow.Owner = this;
                statisticsWindow.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии статистики: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}