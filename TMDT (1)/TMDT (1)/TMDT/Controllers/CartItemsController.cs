
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TMDT;

namespace Fix24h_AdobeBricks_V1.Controllers
{
    public class CartItemsController : Controller
    {
        // GET: CartItems
        public ActionResult Index()
        {
            return View();
        }
        private TapChiDB db = new TapChiDB();
        static string getProduct = "";
        public ActionResult getData(string product)
        {
            {
                getProduct = product;
                //var json = new JavaScriptSerializer();
                //var data = json.Deserialize<dynamic>(product);

                //foreach (var obj in data)
                //{
                //    var pList = new OrderDetail();

                //    pList.OrderID = 1;
                //    pList.ProductID = (int)obj["id"];
                //    pList.Price = double.Parse(obj["price"] + "");
                //    pList.Quantity = int.Parse(obj["quantity"] + "");

                //    db.OrderDetails.Add(pList);
                //    db.SaveChanges();





                //}


            }

            return Json(new { t = 1 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult saveOrders(string customers)
        {
            {
               
                    var jsonCustomer = new JavaScriptSerializer();
                    var CustomerData = jsonCustomer.Deserialize<dynamic>(customers);
                    int orderID = 0; try
                {
                    foreach (var obj in CustomerData)
                    {
                        var pList = new Order();
                        pList.CreatedDate = DateTime.Now;
                        pList.CustomerName = obj["CustomerName"] + "";
                        pList.CustomerAddress = obj["CustomerAddress"] + "";
                        pList.CustomerEmail = obj["CustomerEmail"] + "";
                        pList.CustomerMobile = obj["CustomerMobile"] + "";
                        pList.Status = false;
                        pList.CustomerId = int.Parse(HttpContext.Application["ID"].ToString());

                         //  var value = db.Orders.Where(x=>x.CustomerMobile==pList.CustomerMobile).ToList();
                        //var value1 = db.Orders.Max(x => x.ID);

                        db.Orders.Add(pList);
                        db.SaveChanges();
                        orderID = pList.ID;




                    }
                }catch(DbEntityValidationException e)
                {
                  
                    throw e;
                }

                var json = new JavaScriptSerializer();
                var data = json.Deserialize<dynamic>(getProduct);

                foreach (var obj in data)
                {
                    var pList = new OrderDetail();

                    pList.OrderID = orderID;
                    pList.ProductID = (int)obj["id"];
                    pList.Price = decimal.Parse(obj["price"] + "");
                    pList.Quantity = int.Parse(obj["quantity"] + "");
                    pList.ColorId = 5;
                    pList.SizeId = 2;
                    db.OrderDetails.Add(pList);
                    db.SaveChanges();





                }


            }

            return Json(new { t = 1 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login(string customers)
        {
            {

                var jsonCustomer = new JavaScriptSerializer();
                var CustomerData = jsonCustomer.Deserialize<dynamic>(customers);
         
                    ; try
                {
                    foreach (var obj in CustomerData)
                    {
                      
                       
                        string CustomerName = obj["CustomerName"] + "";
                        string Password = obj["CustomerPassword"] + "";
                    
                    }
                }
                catch (DbEntityValidationException e)
                {

                    throw e;
                }

     

            }

            return Json(new { t = 1 }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult getShip(string shiprate)
        //{
        //    {
        //        try
        //        {
        //            var jsonRates = new JavaScriptSerializer();
        //            var ShipRate = jsonRates.Deserialize<dynamic>(shiprate);

        //            foreach (var obj in ShipRate)
        //            {

        //                int distance = (int)obj["ship"];
        //                var money = new ShipRateDao().getMoney(distance);

        //            }
        //        }
        //        catch (Exception ex)
        //        {


        //        }
        //    }

        //    return Json(new { t = 1 }, JsonRequestBehavior.AllowGet);
        //}
    }
}