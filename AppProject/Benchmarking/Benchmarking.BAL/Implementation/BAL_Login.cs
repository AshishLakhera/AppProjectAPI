using Benchmarking.Contracts;
using Benchmarking.DAL;
using Benchmarking.Logger;
using Benchmarking.Model;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;

namespace Benchmarking.BAL.Implementation
{
    public class BAL_Login:ILogin
    {
        private IClient _Client;
        
        public BAL_Login()
        {
            ClientFactory.ClientFactory factory = new ClientFactory.ClientFactory();
            _Client = factory.getClientInstance(ConfigurationSettings.Client);
        }
        public LoginRS Login(LoginRQ loginRq)
        {
            LoginRS response=_Client.Login(loginRq);
            BMLogger.Info("Login BAL");
            return response;
        }
    }
}
