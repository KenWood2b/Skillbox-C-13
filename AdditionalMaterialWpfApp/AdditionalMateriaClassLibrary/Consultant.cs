using System;

namespace AdditionalMaterialWpfApp
{
    public class Consultant : Worker
    {
        public override void EditClient(Client client, string field, string newValue)
        {
            if (field == "PhoneNumber")
            {
                client.PhoneNumber = newValue;
                Console.WriteLine($"Номер телефона клиента обновлен на {newValue}.");
            }
            else
            {
                Console.WriteLine("Консультант может изменять только номер телефона.");
            }
        }
    }
}
