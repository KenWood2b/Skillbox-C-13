using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdditionalMaterialWpfApp
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string PhoneNumber { get; private set; }

        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LastNameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(FirstNameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(MiddleNameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text))
            {
                LastName = LastNameTextBox.Text;
                FirstName = FirstNameTextBox.Text;
                MiddleName = MiddleNameTextBox.Text;
                PhoneNumber = PhoneNumberTextBox.Text;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Заполните все поля.");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Name.Replace("TextBox", ""))
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Name.Replace("TextBox", "");
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }
}

