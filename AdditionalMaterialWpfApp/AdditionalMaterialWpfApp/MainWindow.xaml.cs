﻿using System.Collections.ObjectModel;

using System.Windows;
using System.Windows.Controls;

namespace AdditionalMaterialWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Worker currentWorker;
        private readonly Bank bank;
        private ObservableCollection<Client> clients;
        private string currentRole;
        private ObservableCollection<LogEntry> logEntries;

        public MainWindow()
        {
            InitializeComponent();
            bank = new Bank();
            clients = new ObservableCollection<Client>(bank.Clients);
            ClientsGrid.ItemsSource = clients;
            logEntries = new ObservableCollection<LogEntry>();
            LogDataGrid.ItemsSource = logEntries;

            // Подписка на события
            bank.OnOperationPerformed += AddLogEntry;

            if (currentWorker != null)
            {
                currentWorker.OnOperationPerformed += AddLogEntry;
            }
        }


        // Обработчик изменения роли
        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string role = selectedItem.Content.ToString();
                if (role == "Консультант")
                    currentWorker = new Consultant();
                else if (role == "Менеджер")
                    currentWorker = new Manager();

                UpdateInterfaceBasedOnRole();
            }
        }

        // Обновление интерфейса в зависимости от роли
        private void UpdateInterfaceBasedOnRole()
        {
            if (currentWorker is Manager)
            {
                AddClientButton.IsEnabled = true;
                ManageAccountsButton.IsEnabled = true;
                TransferFundsButton.IsEnabled = true;
            }
            else if (currentWorker is Consultant)
            {
                AddClientButton.IsEnabled = false;
                ManageAccountsButton.IsEnabled = false;
                TransferFundsButton.IsEnabled = true;
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var addClientWindow = new AddClientWindow();
            if (addClientWindow.ShowDialog() == true)
            {
                var newClient = new Client(
                    clients.Count + 1,
                    addClientWindow.FirstName, 
                    addClientWindow.LastName,
                    addClientWindow.MiddleName,
                    addClientWindow.PhoneNumber
                );
                bank.AddClient(newClient);
                clients.Add(newClient);
            }
        }

        private void ManageAccounts_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsGrid.SelectedItem is Client selectedClient)
            {
                var manageAccountsWindow = new ManageAccountsWindow(selectedClient);
                manageAccountsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите клиента для управления счетами.");
            }
        }

        private void TransferFunds_Click(object sender, RoutedEventArgs e)
        {
            var transferWindow = new TransferFundsWindow(bank);
            transferWindow.ShowDialog();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bank.SaveClientsToFile("clients.txt");
            MessageBox.Show("Данные успешно сохранены.");
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            bank.LoadClientsFromFile("clients.txt");
            clients = new ObservableCollection<Client>(bank.Clients);
            ClientsGrid.ItemsSource = clients;
            MessageBox.Show("Данные успешно загружены.");
        }


        private void AddLogEntry(LogEntry entry)
        {
            Dispatcher.Invoke(() =>
            {
                logEntries.Add(entry);
                MessageBox.Show(entry.ToString(), "Оповещение");
            });
        }
    }
}
