using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace WebApplication3
{
    public class DBClass
    {
        public static OracleConnection GetDBConnection()
        {

            //Console.WriteLine("Getting Connection ...");

            string host = "THPUBMES-SCAN";
            int port = 1521;
            string sid = "THPUBMES";
            string user = "MESAP03";
            string password = "Delta12345";

            // 'Connection string' to connect directly to Oracle.
            string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
                 + host + ")(PORT = " + port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
                 + sid + ")));Password=" + password + ";User ID=" + user;

            //OracleConnection conn = new OracleConnection();

            //conn.ConnectionString = connString;

            //return conn;
            return new OracleConnection(connString);
        }
        public static OracleConnection GetDBConnection6()
        {

            // Console.WriteLine("Getting Connection6 ...");

            string host = "172.19.249.3";
            int port = 1521;
            string sid = "DETBCWG";
            string user = "MESAP03";
            string password = "Delta12345";

            // 'Connection string' to connect directly to Oracle.
            string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
                 + host + ")(PORT = " + port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
                 + sid + ")));Password=" + password + ";User ID=" + user;

            return new OracleConnection(connString);
        }

        public static string QPallet()
        {
            string sql = @"select a.pallet_no,a.model_name,
                        a.mo_number,a.actual_SN_NUM,
                        a.qa_no,b.wms_send_flag, 
                        CASE to_number(b.wms_send_flag) 
                        WHEN 1 THEN 'EWM' end as wms_send_flag1,
                        CASE When a.qa_no is null THEN 'Do not FQC' else 'Already pass FQC' end as qa_no1 
                        from sfism4.r_pallet_t a 
                        inner join sfism4.C_MODEL_DESC_T b 
                        on a.model_name = b.model_name 
                        where a.packing_time >= sysdate -5 and a.actual_SN_NUM > 0 and b.wms_send_flag != 0 
                        order BY a.packing_time desc";
            return sql;
        }
        //                    select a.pallet_no, a.model_name,a.mo_number, a.actual_SN_NUM,a.qa_no, b.wms_send_flag,
        //                    from det_ps.r_pallet_t a
        //                    inner join det_ps.C_MODEL_DESC_T b on a.model_name = b.model_name
        //                    where a.packing_time >= sysdate -5 and a.actual_SN_NUM > 0 and b.wms_send_flag != 0 
        //                    order BY a.packing_time desc


        //                    CASE to_number(b.wms_send_flag)
        //                    WHEN 1 THEN 'EWM' end as wms_send_flag1,
        //                    CASE When a.qa_no is null THEN 'Do not FQC' else 'Already pass FQC' end as qa_no1

        }
    }