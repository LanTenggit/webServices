using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServicesForOpreateTime
{
    /// <summary>
    /// OperateTimeService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class OperateTimeService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public void GetData(string Schedulestr)
        {
            string[] Sche = Schedulestr.Split(',');
            bool ISTrue = false;
            string msg = "";
            List<ScheduleClass> list = new List<ScheduleClass>();
            try
            {
                ////select* from mesinfo t where(t.Schedule like '%YDSY19B27B2P%' or t.Schedule like '%YDSY19B23BAP%');
                 string  sqlstr = " select * from(select WareHouseingInfo.ID as ID, Schedule, WareHouseingInfo.Operating_Time as Operating_Time from WareHouseingInfo, Mesinfo where" +
                    " Mesinfo.id = WareHouseingInfo.MesinfoID and WareHouseingInfo.ISPrinted = 1 ) as t where( ";
                for (int i = 0; i < Sche.Count() - 1; i++)
                {

                    sqlstr += " t.Schedule like  '%" + Sche[i] + "%' or "; 


                }
                sqlstr += "t.Schedule  like  '%" + Sche[Sche.Count()-1] + "%')";

                DataTable DTSchedule = DBHLpter.GetDataTable(sqlstr);

                foreach (DataRow item in DTSchedule.Rows)
                {
                    ScheduleClass Schedule = new ScheduleClass();
                    Schedule.ID = Convert.ToInt32(item["ID"].ToString());
                    Schedule.Schedule = item["Schedule"].ToString().Split('-')[0];
                    Schedule.OprateTime = item["Operating_Time"].ToString();
                    msg = "数据获取成功";
                    list.Add(Schedule);
                    ISTrue = true;
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                ISTrue = false;
            }
            string json = JsonConvert.SerializeObject(new { ISTrue = ISTrue, Msg = msg, list = list });
            HttpContext.Current.Response.ContentType = "text/html;charset=utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8"); //设置输出流为简体中文
            HttpContext.Current.Response.Write(json);

        }

        /// <summary>
        /// 排程
        /// </summary>
        public class ScheduleClass
        {
            public int ID { get; set; }
            public string  Schedule { get; set; }
            public string OprateTime { get; set; }
        }







    }
}
