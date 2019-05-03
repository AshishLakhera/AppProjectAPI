using AppProject.Model;
using ClassLibrary1;
using CompsContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppProject.Repositories
{
    public class LoginRepositories
    {
        private readonly Entities _db;
        private readonly CompsEntities _compsDb;
        public LoginRepositories(Entities Db, CompsEntities compsDb)
        {
            _db = Db;
            _compsDb = compsDb;
        }
        public bool ValidateCompsUser(LoginModel model)
        {
            var user = _compsDb.UserProfile.Where(x => x.UserName== model.UserName && x.UserName== model.Password).FirstOrDefault();
            if (user != null)
            {
                return true;

            }
            else
            {
                return false;
            }

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
