using Domain;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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
            statusComboBox.SelectedIndex = 0;
            Title = "Добавление новой заявки";
        }

        public AddEditRequestWindow(RepairRequest requestToEdit)
        {
            InitializeComponent();
            NewRequest = requestToEdit;
            isEditMode = true;
            Title = "Редактирование заявки №" + requestToEdit.Id;
            LoadRequestData();
        }

        private void LoadRequestData()
        {
            carTypeTextBox.Text = NewRequest.CarType;
            carModelTextBox.Text = NewRequest.CarModel;
            clientNameTextBox.Text = NewRequest.ClientName;
            phoneTextBox.Text = NewRequest.PhoneNumber;
            problemTextBox.Text = NewRequest.ProblemDescription;

            // Установка статуса
            switch (NewRequest.Status)
            {
                case "Новая заявка": statusComboBox.SelectedIndex = 0; break;
                case "В процессе ремонта": statusComboBox.SelectedIndex = 1; break;
                case "Ожидание запчастей": statusComboBox.SelectedIndex = 2; break;
                case "Завершена": statusComboBox.SelectedIndex = 3; break;
                default: statusComboBox.SelectedIndex = 0; break;
            }

            mechanicTextBox.Text = NewRequest.ResponsibleMechanic;
            commentsTextBox.Text = NewRequest.Comments;
            sparePartsTextBox.Text = NewRequest.SpareParts;
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

            // Простая валидация телефона
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

            if (statusComboBox.SelectedItem is ComboBoxItem selectedStatus)
            {
                NewRequest.Status = selectedStatus.Content.ToString();
            }

            NewRequest.ResponsibleMechanic = mechanicTextBox.Text.Trim();
            NewRequest.Comments = commentsTextBox.Text.Trim();
            NewRequest.SpareParts = sparePartsTextBox.Text.Trim();
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