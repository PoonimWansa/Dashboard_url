using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using WebApplication3.Models;
using System.Data;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        static int cnt = 0;
        static int count = 0;
        public async Task<ActionResult> Index()
        {
            lData.Clear();
            cnt = 0;
            Getinfo();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        static List<Data> lData = new List<Data>();
        public static void Getinfo(/*string company, string plant*/)
        {
            //string connn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            InfoMo di = new InfoMo();
            //di.plant = plant;
            //di.compny = company;

            //List<Data> lData = new List<Data>();
            using (OracleConnection conn = DBClass.GetDBConnection())
            //using (OracleConnection conn = new OracleConnection(connn))
            {
                var _with1 = conn;
                if (_with1.State == ConnectionState.Open)
                    _with1.Close();
                _with1.Open();

                OracleCommand cmd = new OracleCommand(DBClass.QPallet(), conn);
                OracleDataReader reader;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Data item = new Data();
                    item.Pallet = reader["PALLET_NO"].ToString();
                    item.Model = reader["MODEL_NAME"].ToString();
                    item.WO = reader["MO_NUMBER"].ToString();
                    item.Qty = reader["ACTUAL_SN_NUM"].ToString();
                    item.QtyStatus_FQC = reader["QA_NO1"].ToString();
                    item.StatusModel = reader["WMS_SEND_FLAG1"].ToString();
                    lData.Add(item);

                }
            }
            
            //return lData;
            //di.listdata = lData;
            //return PartialView("DTInfo", di);
        }

        //static int cnt = 0;
        public async Task<ActionResult> datacoutn()
        {

            InfoMo di = new InfoMo();
            if (lData != null)
            {
                if (cnt == lData.Count)
                {
                    lData.Clear();
                    cnt = 0;
                    Getinfo();
                }
                List<Data> allResponses = new List<Data>();

                for (int i = cnt; i < cnt + 10 && i < lData.Count;  i++)
                {

                        lData[i].Num = (i + 1).ToString();
                        allResponses.Add(lData[i]);
                        count = i + 1;

                }

                cnt = count;
                di.cnt = cnt.ToString();
                di.item = lData.Count.ToString();
                di.listdata = allResponses;
            }

            return PartialView("DTInfo", di);

        }
        //public ActionResult ExportExcel()
        //{
        //    InfoMo di = new InfoMo();
        //    var gv = new GridView();
        //    gv.DataSource = di;
        //    gv.DataBind();
        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
        //    Response.ContentType = "application/ms-excel";
        //    Response.Charset = "";
        //    StringWriter objStringWriter = new StringWriter();
        //    HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        //    gv.RenderControl(objHtmlTextWriter);
        //    Response.Output.Write(objStringWriter.ToString());
        //    Response.Flush();
        //    Response.End();
        //    return View(di);
        //}
    }



    

    //[HttpPost]
    //public JsonResult AjaxPostCall(InfoMo data)
    //{
    //    InfoMo di = new InfoMo
    //    {
    //        No = data.No,
    //        Name = data.Name,
    //    };
    //    return Json(di);
    //}
    //    public void ReadData(string connectionString) 
    //{ 
    //    string queryString = "SELECT EmpNo, EName FROM Emp"; 
    //    using (OracleConnection connection = new OracleConnection(connectionString)) 
    //    { 
    //        OracleCommand command = new OracleCommand(queryString, connection); 
    //        connection.Open(); 
    //        using(OracleDataReader reader = command.ExecuteReader()) 
    //        { 
    //            // Always call Read before accessing data. while (reader.Read())
    //            // { Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetString(1));
    //        } 
    //    } 
    //}

}
