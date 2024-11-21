using System;

namespace AdditionalMaterialWpfApp
{
    public class Manager : Worker
    {
        public override void EditClient(Client client, string field, string newValue)
        {
            switch (field)
            {
                case "LastName":
                    client.LastName = newValue;
                    Console.WriteLine($"Фамилия клиента обновлена на {newValue}.");
                    break;

                case "FirstName":
                    client.FirstName = newValue;
                    Console.WriteLine($"Имя клиента обновлено на {newValue}.");
                    break;

                case "MiddleName":
                    client.MiddleName = newValue;
                    Console.WriteLine($"Отчество клиента обновлено на {newValue}.");
                    break;

                case "PhoneNumber":
                    client.PhoneNumber = newValue;
                    Console.WriteLine($"Номер телефона клиента обновлен на {newValue}.");
                    break;

                default:
                    Console.WriteLine($"Поле {field} не существует или не поддерживается для редактирования.");
                    break;
            }
        }
    }
}
