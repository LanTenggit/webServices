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
            List<ScheduleClass> OprateTimestrlist = new List<ScheduleClass>();
          

            for (int i = 0; i < Sche.Count(); i++)
            {
                ScheduleClass scheduleLength = new ScheduleClass();
                scheduleLength.Schedule = Sche[i];
                OprateTimestrlist.Add(scheduleLength);
            }


          
            string OprateTimestr = "";
            try
            {
                ////select* from mesinfo t where(t.Schedule like '%YDSY19B27B2P%' or t.Schedule like '%YDSY19B23BAP%');
                 string  sqlstr = " select * from(select WareHouseingInfo.ID as ID, Schedule, WareHouseingInfo.Operating_Time as Operating_Time from WareHouseingInfo, Mesinfo where" +
                    " Mesinfo.id = WareHouseingInfo.MesinfoID and WareHouseingInfo.ISPrinted = 1 ) as t where( ";
                for (int i = 0; i < Sche.Count() - 1; i++)
                {

                    sqlstr += " t.Schedule like  '%" + Sche[i] + "%' or "; 


                }
                sqlstr += "t.Schedule  like  '%" + Sche[Sche.Count()-1] + "%')   ORDER BY Operating_Time";

                DataTable DTSchedule = DBHLpter.GetDataTable(sqlstr);

                if (DTSchedule.Rows.Count>0)
                {
                  foreach (DataRow item in DTSchedule.Rows)
                  {
                    ScheduleClass Schedule = new ScheduleClass();
                    Schedule.ID = Convert.ToInt32(item["ID"].ToString());
                    Schedule.Schedule = item["Schedule"].ToString().Split('-')[0];
                    Schedule.OprateTime = item["Operating_Time"].ToString();
                    list.Add(Schedule);
                    ISTrue = true;
                   
                  }

                   ///根据值的顺序排序
                   for (int i = 0; i < Sche.Length ; i++)
                   {
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (Sche[i] == list[j].Schedule)
                            {
                                msg = "数据获取成功";
                                OprateTimestrlist[i].OprateTime =Convert.ToDateTime( list[j].OprateTime).ToString("yyyy-MM-dd HH:mm:ss") ;
                                j = list.Count();
                               
                                //continue;
                            }
                            else
                            {
                                OprateTimestrlist[i].OprateTime= " null";
                            }
                        }
                   }

                  for (int i = 0; i < OprateTimestrlist.Count; i++)
                  {
                    if (i< OprateTimestrlist.Count-1)
                    {
                        OprateTimestr += OprateTimestrlist[i].OprateTime + ",";
                    }
                    else
                    {
                        OprateTimestr +=  OprateTimestrlist[i].OprateTime;
                    }

                
                  }

                }
                else
                {

                    ISTrue = false;
                    msg = "未查询到数据！";
                    OprateTimestr = null;
                }


            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                ISTrue = false;
            }
            string json = JsonConvert.SerializeObject(new { ISTrue = ISTrue, Msg = msg, OprateTime = OprateTimestr});
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
