using Domain;
using System.Text.RegularExpressions;
using System.Windows;
using UI.Helpers;
using System.Linq;

namespace UI
{
    public partial class AddEditRequestWindow : Window
    {
        public RepairRequest NewRequest { get; private set; }
        private bool isEditMode = false;

        public AddEditRequestWindow()
        {
            InitializeComponent();
            NewRequest = new RepairRequest();
            InitializeStatusComboBox();
            Title = "Добавление новой заявки";
        }

        public AddEditRequestWindow(RepairRequest requestToEdit)
        {
            InitializeComponent();
            NewRequest = requestToEdit;
            
            InitializeStatusComboBox();
            Title = "Редактирование заявки №" + requestToEdit.Id;
            LoadRequestData();
        }

        private void InitializeStatusComboBox()
        {
            statusComboBox.ItemsSource = System.Enum.GetValues<RepairRequestType>()
                .Select(requestType => new RepairRequestTypeComboBoxItem {
                    Value = requestType,
                    Text = requestType.ToPrettyString(),
                });
            statusComboBox.SelectedIndex = 0;
        }

        private void LoadRequestData()
        {
            carTypeTextBox.Text = NewRequest.CarType;
            carModelTextBox.Text = NewRequest.CarModel;
            clientNameTextBox.Text = NewRequest.ClientName;
            phoneTextBox.Text = NewRequest.PhoneNumber;
            problemTextBox.Text = NewRequest.ProblemDescription;

            
            var selectedItem = statusComboBox.Items.Cast<RepairRequestTypeComboBoxItem>()
                .FirstOrDefault(item => item.Value == NewRequest.Status);
            statusComboBox.SelectedItem = selectedItem ?? statusComboBox.Items[0];

            mechanicTextBox.Text = NewRequest.ResponsibleMechanic;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(carTypeTextBox.Text))
            {
                MessageBox.Show("Введите марку автомобиля", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                carTypeTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(carModelTextBox.Text))
            {
                MessageBox.Show("Введите модель автомобиля", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                carModelTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(clientNameTextBox.Text))
            {
                MessageBox.Show("Введите ФИО клиента", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                clientNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(phoneTextBox.Text))
            {
                MessageBox.Show("Введите номер телефона", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                phoneTextBox.Focus();
                return false;
            }

            if (!Regex.IsMatch(phoneTextBox.Text, @"^[\d\s\-\+\(\)]+$"))
            {
                MessageBox.Show("Некорректный номер телефона", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                phoneTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(problemTextBox.Text))
            {
                MessageBox.Show("Введите описание проблемы", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                problemTextBox.Focus();
                return false;
            }

            return true;
        }

        private void SaveDataToRequest()
        {
            NewRequest.CarType = carTypeTextBox.Text.Trim();
            NewRequest.CarModel = carModelTextBox.Text.Trim();
            NewRequest.ClientName = clientNameTextBox.Text.Trim();
            NewRequest.PhoneNumber = phoneTextBox.Text.Trim();
            NewRequest.ProblemDescription = problemTextBox.Text.Trim();
            NewRequest.Status = ((RepairRequestTypeComboBoxItem)statusComboBox.SelectedItem).Value;
            NewRequest.ResponsibleMechanic = mechanicTextBox.Text.Trim();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            SaveDataToRequest();
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}