using Benchmarking.Contracts;
using Benchmarking.Model;
using Benchmarking.Model.Model.Dapper;
using CompsBuilderDLL.Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Benchmarking.BAL.ClientFactory
{
    public class EVSClient : IClient
    {
        private readonly IDataRepository _repository;
        public EVSClient()
        {
            _repository = new DataRepository();
        }
        public LoginRS Login(LoginRQ loginRq)
        {
           //make dal call here for login
            return new LoginRS();
        }
    }
}
