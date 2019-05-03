using Benchmarking.Model.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking.Model
{
    public class LoginRS:BaseResponse
    {
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
