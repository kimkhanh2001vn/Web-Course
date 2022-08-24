using System.Linq;
using System.Text.RegularExpressions;

namespace TMDT.Models.DAO
{
    public class SubscribedEmailDAO
    {
        private TapChiDB db = new TapChiDB();
        public bool EmailExists(string email)
        {
            return db.SubscribedEmails.Any(x => x.Email == email);
        }

        public static bool IsEmail(string email)
        {
            const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
            if (email != null)
            {
                return Regex.IsMatch(email, MatchEmailPattern);
            }
            else
            {
                return false;
            }
        }
    }
}