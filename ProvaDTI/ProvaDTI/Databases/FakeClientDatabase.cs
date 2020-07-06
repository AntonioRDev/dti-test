using ProvaDTI.Models;
using System.Collections.Generic;

namespace ProvaDTI.Databases
{
    public static class FakeClientDatabase
    {
        private static List<Client> Clients { get; set; } = new List<Client>();

        public static Client GetClientByName(string name)
        {
            return Clients.Find(client => client.Name == name);
        }

        public static Client GetClientByEmail(string email)
        {
            return Clients.Find(client => client.Email == email);
        }

        public static void SaveClient(Client client)
        {
            Clients.Add(client);
        }
    }
}
