using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Domain;

namespace UI
{
    public partial class MainWindow : Window
    {
        private List<RepairRequest> requests = new List<RepairRequest>();
        private RepairRequest selectedRequest;

        public MainWindow()
        {
            InitializeComponent();
            LoadSampleData();
            RefreshDataGrid();
        }

        private void LoadSampleData()
        {
            // Тестовые данные
            requests.Add(new RepairRequest("Toyota", "Camry", "Замена масла", "Иванов Иван", "+79991234567")
            {
                Id = 1,
                Status = "В процессе ремонта",
                ResponsibleMechanic = "Петров А.С."
            });

            requests.Add(new RepairRequest("Honda", "Civic", "Ремонт тормозов", "Сидоров Петр", "+79997654321")
            {
                Id = 2,
                Status = "Ожидание запчастей"
            });
        }

        private void RefreshDataGrid()
        {
            requestsGrid.ItemsSource = null;
            requestsGrid.ItemsSource = requests;
            totalRequestsText.Text = requests.Count.ToString();
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEditRequestWindow();
            if (addWindow.ShowDialog() == true)
            {
                var newRequest = addWindow.NewRequest;
                newRequest.Id = requests.Count > 0 ? requests[^1].Id + 1 : 1;
                requests.Add(newRequest);
                RefreshDataGrid();
                statusText.Text = "Заявка добавлена успешно";
            }
        }

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRequest == null)
            {
                MessageBox.Show("Выберите заявку для редактирования", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editWindow = new AddEditRequestWindow(selectedRequest);
            if (editWindow.ShowDialog() == true)
            {
                RefreshDataGrid();
                statusText.Text = "Заявка обновлена успешно";
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
            statusText.Text = "Данные обновлены";
        }

        private void RequestsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRequest = requestsGrid.SelectedItem as RepairRequest;
        }
    }
}