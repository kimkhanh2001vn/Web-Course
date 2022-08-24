using System;
using System.Linq;

namespace TMDT.Models.DAO
{
    public class UserDAO
    {
        private TapChiDB db = new TapChiDB();

        public bool ChangeStatus(int id)
        {
            UserAdministrator user = db.UserAdministrators.Find(id);
            user.Allowed = !user.Allowed;
            db.SaveChanges();
            return user.Allowed;
        }

        public bool Delete(int id)
        {
            try
            {
                UserAdministrator user = db.UserAdministrators.Find(id);
                db.UserAdministrators.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int CountUserName()
        {
            return db.UserAdministrators.Count();
        }

        public bool CheckUserName(string userName)
        {
            return db.UserAdministrators.Count(x => x.UserName == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.UserAdministrators.Count(x => x.Email == email) > 0;
        }
    }
}