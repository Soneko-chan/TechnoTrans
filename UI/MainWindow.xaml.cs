using System.Windows;
using System.Windows.Controls;
using Domain;
using Data.Interfaces;
using Data.InMemory;

namespace UI
{
    public partial class MainWindow : Window
    {
        private readonly IRepairRequestRepository _requestRepository;
        private RepairRequest _selectedRequest;
        private IEnumerable<RepairRequest> _allreqs;

        public MainWindow()
        {
            InitializeComponent();
            _requestRepository = new RepairRequestRepository();
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            _allreqs = _requestRepository.GetAll();
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
            _selectedRequest = requestsGrid.SelectedItem as RepairRequest;
        }
    }
}