using System;

namespace AppServerThread.Core
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string DocumentNumber { get; set; }

        public override string ToString()
        {
            return $"{DateTime.Now} - ID: {ClientId}, Name: {Name}, Document: {DocumentNumber}";
        }
    }
}
