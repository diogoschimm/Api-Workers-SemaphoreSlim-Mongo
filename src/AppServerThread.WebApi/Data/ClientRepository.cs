using AppServerThread.Core;
using Bogus;
using Bogus.Extensions.Brazil;
using System.Collections.Generic;

namespace AppServerThread.WebApi.Data
{
    public class ClientRepository
    {
        private readonly Faker faker = new("pt_BR");
        private const int TOTAL_CLIENTS = 777;
        private static List<Client> Db = new();

        public IList<Client> GetResults()
        {
            if (Db.Count == 0)
            {
                Db = new List<Client>();
                for (int i = 1; i <= TOTAL_CLIENTS; i++)
                {
                    Db.Add(new Client
                    {
                        ClientId = i,
                        Name = faker.Name.FullName(),
                        DocumentNumber = new Faker("pt_BR").Person.Cpf(false)
                    });
                }
            }
            return Db;
        }
    }
}
