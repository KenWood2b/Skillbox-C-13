using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalMaterialWpfApp
{
    public class Client : INotifyPropertyChanged
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _phoneNumber;
        private List<IAccount<string>> _accounts;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public List<IAccount<string>> Accounts
        {
            get => _accounts;
            private set
            {
                _accounts = value;
                OnPropertyChanged();
            }
        }

        public Client(int id, string firstName, string lastName, string middleName, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            Accounts = new List<IAccount<string>>();
        }

        public void AddAccount(IAccount<string> account)
        {
            if (Accounts.Exists(a => a.Type == account.Type))
                throw new InvalidOperationException($"У клиента уже есть счёт типа {account.Type}.");

            Accounts.Add(account);

            OnPropertyChanged(nameof(Accounts));
        }
        public string FullName => $"{LastName} {FirstName} {MiddleName}";
        public override string ToString() =>
            $"{LastName} {FirstName} {MiddleName} (ID: {Id}) — Телефон: {PhoneNumber}, Счета: {Accounts.Count}";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
