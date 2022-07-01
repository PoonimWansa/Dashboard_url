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

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {


        public async Task<ActionResult> Index()
        {
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
            string connn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            InfoMo di = new InfoMo();
            //di.plant = plant;
            //di.compny = company;

            //List<Data> lData = new List<Data>();

            using (OracleConnection conn = new OracleConnection(connn))
            {
                var _with1 = conn;
                if (_with1.State == ConnectionState.Open)
                    _with1.Close();
                _with1.Open();

                // OracleCommand cmd = new OracleCommand("SELECT customer, passwd, cust_code FROM DET_DA.c_customer_t  order by customer DESC FETCH NEXT 11 ROWS ONLY", conn);
                //OracleCommand cmd = new OracleCommand("SELECT customer, passwd, cust_code FROM DET_DA.c_customer_t", conn);
                //want show data
                OracleCommand cmd = new OracleCommand("select a.pallet_no,a.model_name,a.mo_number,a.actual_SN_NUM,a.qa_no,b.wms_send_flag, CASE to_number(b.wms_send_flag) WHEN 0 THEN 'Non EWM' WHEN 1 THEN 'EWM' end as wms_send_flag1,CASE When a.qa_no is null THEN 'Do not FQC' else 'Already pass FQC' end as qa_no1 from sfism4.r_pallet_t a inner join sfism4.C_MODEL_DESC_T b on a.model_name = b.model_name where a.packing_time >= sysdate -3 order by a.packing_time desc", conn);
                //OracleCommand cmd = new OracleCommand("select a.pallet_no,a.model_name,a.mo_number,a.actual_SN_NUM,a.qa_no,b.wms_send_flag,CASE to_number(b.wms_send_flag) WHEN 0 THEN 'Normal' WHEN 1 THEN 'EWM' end as wms_send_flag1,CASE a.qa_no WHEN null THEN 'Already pass FQC' else 'Do not FQC' end as qa_no1 from sfism4.r_pallet_t a inner join sfism4.C_MODEL_DESC_T b on a.model_name = b.model_name where a.packing_time > sysdate -1/168 order by a.packing_time desc", conn);
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

        static int cnt = 0;
        public async Task<ActionResult> datacoutn()
        {
            //List<Data> listdt = Getinfo();

            InfoMo di = new InfoMo();
            if (lData != null)
            {
                if (cnt == lData.Count)
                {
                    lData.Clear();
                    Getinfo();
                }
                List<Data> allResponses = new List<Data>();
                int itemp = 0;
                for (int i = cnt; i < cnt + 10 && i < lData.Count ; i++)
                {
                    lData[i].Num = (i + 1).ToString();
                    allResponses.Add(lData[i]);
                    itemp = i + 1;
                }

                cnt = itemp;
                di.listdata = allResponses;
            }

            return PartialView("DTInfo", di);
        }

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
