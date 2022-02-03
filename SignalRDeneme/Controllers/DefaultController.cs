using Newtonsoft.Json;
using SignalRDeneme.Models;
using SignalRDeneme.SignalHub;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRDeneme.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult VeriGetir()
        {
            using (SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglantim"].ConnectionString))
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                using (SqlCommand cmd = new SqlCommand(@"[sp_crw_test_test_signalr_veri_getir]", baglanti))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDependency dependency = new SqlDependency(cmd);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }

        public JsonResult VeriEkle()
        {
            using (SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglantim"].ConnectionString))
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                //using (SqlCommand cmd = new SqlCommand(@"insert into [dbo].[aaaaaaaaa_stok] ([kod] ,[miktar]) values ('asdsa',123)", baglanti))
                using (SqlCommand cmd = new SqlCommand(@"[sp_crw_test_test_signalr]", baglanti))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDependency dependency = new SqlDependency(cmd);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    cmd.ExecuteNonQuery();
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult VeriSil()
        {
            using (SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglantim"].ConnectionString))
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                using (SqlCommand cmd = new SqlCommand(@"delete aaaaaaaaa_stok", baglanti))
                {
                    SqlDependency dependency = new SqlDependency(cmd);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    cmd.ExecuteNonQuery();
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MyHub.Show();
            }
        }


    }
}