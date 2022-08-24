using Common;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TMDT.Models.DAO;

namespace TMDT.Areas.Admin.Controllers
{
    [AuthorizeBusiness]
    public class SubscribedEmailsController : Controller
    {
        private TapChiDB db = new TapChiDB();

        // GET: Admin/Feedbacks
        public async Task<ActionResult> Index()
        {
            return View(await db.SubscribedEmails.ToListAsync());
        }

        // GET: Admin/Feedbacks/Compose
        public async Task<ActionResult> Compose()
        {
            return View(await db.SubscribedEmails.ToListAsync());
        }
        //POST : send email after composing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Send(string receiveRadio, string[] recipients, string subject, bool? isDecode, string content)
        {
            switch (receiveRadio)
            {
                case "All":
                    recipients = await db.SubscribedEmails.Select(x => x.Email).ToArrayAsync();
                    break;
                case "Status":
                    recipients = await db.SubscribedEmails.Where(x => x.Status == true).Select(x => x.Email).ToArrayAsync();
                    break;
                default:
                    break;
            }

            if (isDecode.Value)
            {
                content = HttpUtility.HtmlDecode(content);
            }

            EmailSetting setting = await db.EmailSettings.FirstOrDefaultAsync();
            SmtpClient client = new SmtpClient(setting.SmtpClient)
            {
                Port = setting.Port.Value,
                EnableSsl = setting.EnableSsl,
                Credentials = new NetworkCredential(setting.UserName, Encryptor.SimpleDecrypt(setting.Password))
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(setting.UserName)
            };

            foreach (string email in recipients)
            {
                mailMessage.To.Add(new MailAddress(email));
            }
            mailMessage.Subject = subject;
            mailMessage.Body = content;
            mailMessage.IsBodyHtml = isDecode.Value;
            client.Send(mailMessage);
            TempData["SendSucceeded"] = true;
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> EmailSetting()
        {
            return View(await db.EmailSettings.FirstOrDefaultAsync());
        }

        [HttpPost]
        public async Task<ActionResult> EmailSetting([Bind(Include = "ID,SmtpClient,Port,EnableSsl,UserName,Password")]EmailSetting emailSetting)
        {
            if (!ModelState.IsValid)
            {
                return View(emailSetting);
            }

            if (db.EmailSettings.Any())
            {
                db.Entry(emailSetting).State = EntityState.Modified;
                if (string.IsNullOrEmpty(emailSetting.Password))
                {
                    db.Entry(emailSetting).Property("Password").IsModified = false;
                }
                else
                {
                    emailSetting.Password = Encryptor.SimpleEncrypt(emailSetting.Password);
                }

                await db.SaveChangesAsync();
            }
            else
            {
                emailSetting.Password = Encryptor.SimpleEncrypt(emailSetting.Password);
                db.EmailSettings.Add(emailSetting);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            return Json(new
            {
                status = new ChangesDAO().SubscribedEmailStatus(id)
            });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int? id, string category)
        {
            if (id.HasValue)
            {
                SubscribedEmail email = await db.SubscribedEmails.FindAsync(id.Value);
                db.SubscribedEmails.Remove(email);
                await db.SaveChangesAsync();
            }
            return Json(new { redirectUrl = Url.Action("Index", category), isRedirect = true }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
