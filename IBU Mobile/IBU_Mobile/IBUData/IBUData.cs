using System;
using Xamarin.Forms;
using IBU_Mobile.Helpers;
using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System.Threading.Tasks;

namespace IBU_Mobile
{
    public static class IBUData
    {
        public static Page CurrentPage;
        public static Overview Overview = new Overview();
        public static void SetUpData()
        {
           Overview = JsonConvert.DeserializeObject<Overview>(Settings.OverviewData);
        }

        public static async void UpdateData()
        {
            UpdateOverview();
        }

        private static async void UpdateOverview()
        {
            try
            {
                var client = new RestClient("https://jsonblob.com/api/jsonBlob/d89eeb62-0e52-11e7-a0ba-2dbdbe732c03");
                client.UserAgent =
                    "Mozilla/5.0 (Linux; Android 7.1.1; Nexus 6P Build/N4F26I) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.91 Mobile Safari/537.36";
                var request = new RestRequest(Method.GET);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                Overview TempOverview = JsonConvert.DeserializeObject<Overview>(data);
                if (Double.Parse(TempOverview.LastModified) > Double.Parse(Overview.LastModified))
                {
                    Settings.OverviewData = data;
                    Overview = TempOverview;
                    if(CurrentPage.GetType() == typeof(OverviewPage))
                    {
                        ((OverviewPage)CurrentPage).SetUpAction.Invoke();
                    }
                }
            }
            catch (Exception e)
            {
                string exception = e.Message;
                //Oh Well... Maybe no internet?
            }
        }
    }

    public class Overview
    {
        public string LastModified { get; set; }
        public string[] Courses { get; set; }
        public string StudentID { get; set; }
        public string Email { get; set; }
        public string Attendance { get; set; }
        public string Nationality { get; set; }
        public string TotalPrice { get; set; }
        public string TotalPaid { get; set; }
        public string TotalToPay { get; set; }

    }
}