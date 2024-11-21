
using System;
using System.Collections.Generic;
using System.IO;
using AdditionalMateriaClassLibrary;
using AdditionalMaterialWpfApp;
using Newtonsoft.Json;

namespace AdditionalMaterialWpfApp
{
    public class Bank : ITransaction<Client>
    {
        public List<Client> Clients { get; private set; }

        public event Action<LogEntry> OnOperationPerformed;

        public Bank()
        {
            Clients = new List<Client>();
        }

        public void AddClient(Client client)
        {
            if (Clients.Exists(c => c.Id == client.Id))
                throw new InvalidOperationException("Клиент с таким ID уже существует.");
            Clients.Add(client);

            OnOperationPerformed?.Invoke(new LogEntry
            {
                Timestamp = DateTime.Now,
                Action = "Добавление клиента",
                PerformedBy = "Система",
                Details = $"Клиент {client.FirstName} {client.LastName} {client.MiddleName} добавлен"
            });
        }

        public void SaveClientsToFile(string filePath)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new AccountConverter() }
            };

            var json = JsonConvert.SerializeObject(Clients, settings);
            File.WriteAllText(filePath, json);
        }

        public void LoadClientsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var settings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new AccountConverter() }
                };

                var json = File.ReadAllText(filePath);
                Clients = JsonConvert.DeserializeObject<List<Client>>(json, settings) ?? new List<Client>();
            }
        }

        public void TransferFunds(Client sender, Account<string> senderAccount, Client receiver, Account<string> receiverAccount, decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Сумма перевода должна быть положительной.");
            senderAccount.Withdraw(amount);
            receiverAccount.Deposit(amount);

            OnOperationPerformed?.Invoke(new LogEntry
            {
                Timestamp = DateTime.Now,
                Action = "Перевод средств",
                PerformedBy = "Система",
                Details = $"Перевод от {sender.FirstName} {sender.LastName} {sender.MiddleName} к {receiver.LastName} {receiver.FirstName} {receiver.MiddleName}, сумма: {amount:C}"
            });
        }
    }
}


