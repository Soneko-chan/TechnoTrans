using Domain;
using System.Text.RegularExpressions;
using System.Windows;
using UI.Helpers;
using System.Linq;

namespace UI
{
    public partial class AddEditRequestWindow : Window
    {
        public RepairRequest Request { get; private set; }
        private bool isEditMode = false;

        private RepairRequestTypeComboBoxItem[] _statuses = System.Enum.GetValues<RepairRequestStatus>()
                .Select(requestType => new RepairRequestTypeComboBoxItem
                {
                    Value = requestType,
                    Text = requestType.ToPrettyString(),
                })
                .ToArray();

        public AddEditRequestWindow()
        {
            InitializeComponent();
            Request = new RepairRequest();
            InitializeStatusComboBox();
            Title = "Добавление новой заявки";
        }

        public AddEditRequestWindow(RepairRequest requestToEdit)
        {
            InitializeComponent();
            Request = requestToEdit;
            
            InitializeStatusComboBox();
            Title = "Редактирование заявки №" + requestToEdit.Id;
            LoadRequestData();
        }

        private void InitializeStatusComboBox()
        {
            statusComboBox.ItemsSource = _statuses;
            statusComboBox.SelectedIndex = 0;
        }

        private void LoadRequestData()
        {
            carTypeTextBox.Text = Request.CarType;
            carModelTextBox.Text = Request.CarModel;
            clientNameTextBox.Text = Request.ClientName;
            phoneTextBox.Text = Request.PhoneNumber;
            problemTextBox.Text = Request.ProblemDescription;

            
            var selectedItem = _statuses.FirstOrDefault(item => item.Value == Request.Status);
            statusComboBox.SelectedItem = selectedItem ?? statusComboBox.Items[0];

            mechanicTextBox.Text = Request.ResponsibleMechanic;
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
            // В режиме редактирования НЕ создаем новый объект,
            // а обновляем существующий

            Request.CarType = carTypeTextBox.Text.Trim();
            Request.CarModel = carModelTextBox.Text.Trim();
            Request.ClientName = clientNameTextBox.Text.Trim();
            Request.PhoneNumber = phoneTextBox.Text.Trim();
            Request.ProblemDescription = problemTextBox.Text.Trim();
            Request.Status = ((RepairRequestTypeComboBoxItem)statusComboBox.SelectedItem).Value;
            Request.ResponsibleMechanic = mechanicTextBox.Text.Trim();

            // ID уже есть в Request, его менять не нужно!
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