using AppProject.Model;
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Repositories
{
    public class LoginRepositories
    {
        private readonly Entities _db;
        public LoginRepositories(Entities Db)
        {
            _db = Db;
        }
        public LoginRepositories()
        {
            
        }
        public bool ValidateUser(LoginModel model)
        {
            var user = _db.Logins.Where(x => x.UserEmail == model.UserEmail && x.Password == model.Password).FirstOrDefault();
            if (user != null)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
    }
}
