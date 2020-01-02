using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebServicesForOpreateTime
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string url = "http://10.114.130.182:8082/MTSC/ExternalApplyController";


  //          {
  //              "dName": "Automation",
  //"oName": "接口测试",
  //"mtID": "3",
  //"mtName": "生产物料",
  //"oMbelongDepartmentV": "1",
  //"oMbelongDepartmentN": "Automation",
  //"oIsReturn": "0",
  //"oIsleave": "0",
  //"oPredictReturnTime": "2020-01-10",
  //"oCarryUserName": "喻振涛/694217",
  //"gdID": "5",
  //"oPredictGdName": "四楼安检岗",
  //"oDestination": "二楼",
  //"oRemarks": "API 测试",
  //"oCause": "API 测试",
  //"oNum": "100",
  //"TBNum": "3",
  //"NPS": "0",
  //"oPreOutputTime": "2019-12-30",
  //"Ex1": [
  //  "90019944","90075733"
  //],
  //"Ex2": [
  //  "90019944"
  //],
  //"Ex3": [
  //  "90019944"
  //],
  //"Ex4": [
  //  "90019944"
  //],
  //"uAccount": "90019944",
  //"uName": "张三",
  //"rowList": [
  //  ["1","2","3"],
  //  ["4","5","6"]
  //]
//}


    string postDataStr = "";
            HttpPost(url, postDataStr);

            postDataStr= RestClient.Post("",url);


        }



      
        /// <summary>
        /// 发送请求的方法
        /// </summary>
        /// <param name="Url">地址</param>
        /// <param name="postDataStr">数据</param>
        /// <returns></returns>
        private string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;


        }

        public string HttpGetRecerevice(string Url, string postDataStr) {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            return Encoding.UTF8.GetString(b);



        }
       
    }


}
