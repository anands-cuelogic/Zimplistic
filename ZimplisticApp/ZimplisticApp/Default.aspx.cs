using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ZimplisticApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void XMLFileUpload_Click(object sender, EventArgs e)
        {
            FileUploadToServer.Enabled = true;
            GetResponse.Enabled = true;
            requestText.ReadOnly = true;
        }

        protected void XML_Text_Click(object sender, EventArgs e)
        {
            FileUploadToServer.Enabled = false;
            GetResponse.Enabled = true;
            requestText.ReadOnly = false;
        }
        protected void apiRequestMethod_Click(object sender, EventArgs e)
        {
            string methodType = null;
            if (((RadioButton)sender).Checked)
                methodType = ((RadioButton)sender).Text;
            if(methodType == "Xml Text")
            {
                FileUploadToServer.Enabled = false;
                FileUploadToServer.Visible = false;
            }
        }
        public string CallWebService(string apiName)
        {
            var _url = "https://upsprodwebservices.upsefulfillment.com/ws.asmx";
            var _action = "http://webservices.liquidatedirect.com/" + apiName;

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult = null;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                Console.Write(soapResult);
            }
            return soapResult;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
           
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                webRequest.Headers.Add("SOAPAction", action);
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";
            }catch(Exception ex)
            {
                throw ex;
            }
            return webRequest;
        }

        private XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            if (FileUploadToServer.Enabled)
            {
                try
                {
                    bool correctExtension = false;
                    if (FileUploadToServer.HasFile)
                    {
                        string fileName = FileUploadToServer.PostedFile.FileName;
                        string fileExtension = Path.GetExtension(fileName).ToLower();
                        string[] extensionsAllowed = { ".xml" };

                        for (int i = 0; i < extensionsAllowed.Length; i++)
                        {
                            if (fileExtension == extensionsAllowed[i])
                            {
                                correctExtension = true;
                            }
                        }
                        if (correctExtension)
                        {
                            try
                            {
                                string fileSavePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");
                                FileUploadToServer.PostedFile.SaveAs(fileSavePath + fileName);
                                FileStream fs = new FileStream(fileSavePath + fileName, FileMode.Open, FileAccess.ReadWrite);
                                soapEnvelopeDocument.Load(fs);
                                fs.Seek(0, SeekOrigin.Begin);
                                using (StreamReader reader = new StreamReader(fs))
                                {
                                    requestText.Text = reader.ReadToEnd();
                                }
                                soapEnvelopeDocument.LoadXml(requestText.Text);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
                catch (WebException ex)
                {
                    string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    throw ex;
                }
            }
            else
            {
                soapEnvelopeDocument.LoadXml(requestText.Text);
            }
            return soapEnvelopeDocument;
        }
        
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
        protected void apiList_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected string GetAPIName()
        {
            string selectedApiName = null;
            if (GetListItemsByListNameV4.Checked)
            {
                selectedApiName = "GetListItemsByListNameV4";
            }
            else if (InsertOrderV1.Checked)
            {
                selectedApiName = "InsertOrderV1";
            }
            else if (SearchOrdersV6.Checked)
            {
                selectedApiName = "SearchOrdersV6";
            }
            else if (GetOrderShippingRecords.Checked)
            {
                selectedApiName = "GetOrderShippingRecords";
            }
            else if (GetOrderV2.Checked)
            {
                selectedApiName = "GetOrderV2";
            }
            return selectedApiName;
        }
        protected void GetResponse_Click(object sender, EventArgs e)
        {
            try
            {
                responseText.Text = "";
                Zimplistic.wsSoapClient client = new Zimplistic.wsSoapClient("wsSoap");
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string result = CallWebService(GetAPIName());
                responseText.Text = result;
                result = null;
                client.Close();
            }
            catch (Exception ex)
            {
                responseText.Text = "Please upload the right content related with selected API";
                //throw ex;
            }
        }
    }
}