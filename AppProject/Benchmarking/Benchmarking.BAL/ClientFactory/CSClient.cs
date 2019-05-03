using Benchmarking.Contracts;
using Benchmarking.Model;
using CompsBuilderDLL.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking.BAL.ClientFactory
{
    public class CSClient : IClient
    {
        private readonly IDataRepository _repository;
        public CSClient()
        {
            _repository = new DataRepository();
        }
        public LoginRS Login(LoginRQ loginRq)
        {
            throw new NotImplementedException();
        }
    }
}
