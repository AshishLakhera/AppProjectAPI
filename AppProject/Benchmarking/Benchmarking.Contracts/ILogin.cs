using Benchmarking.Model;
using System;

namespace Benchmarking.Contracts
{
    public interface ILogin
    {       
        LoginRS Login(LoginRQ loginRq);
    }
}
