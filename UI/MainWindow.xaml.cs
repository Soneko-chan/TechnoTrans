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
            RefreshDataGrid();
        }

       

        private void RefreshDataGrid()
        {
            requestsGrid.ItemsSource = null;
            requestsGrid.ItemsSource = requests;
            totalRequestsText.Text = requests.Count.ToString();
            statusText.Text = "Данные обновлены";
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
            }
        }

        private void RequestsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRequest = requestsGrid.SelectedItem as RepairRequest;
        }
    }
}