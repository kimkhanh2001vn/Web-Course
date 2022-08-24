using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TMDT.Models.DAO;

namespace Common
{
    public class LogActivity
    {
        public static async Task  LogSavedActivity(Object savedObject)
        {
            await new LogUserActivityDAO().LogSavedActivity(JsonConvert.SerializeObject(savedObject));
        }
    }
}