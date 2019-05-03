using Benchmarking.Contracts;
using Benchmarking.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking.BAL.ClientFactory
{
    public class ClientFactory
    {
        public IClient getClientInstance(Clients client)
        {
            IClient _Client;
            switch (client.ToString())
            {
                case "CS":
                    _Client = new CSClient();
                    break;
                case "DB":
                    _Client = new DBClient();
                    break;
                case "EVS":
                    _Client = new EVSClient();
                    break;
                default:
                    _Client = new EVSClient();
                    break;
            }
            return _Client;
        }
    }
}
