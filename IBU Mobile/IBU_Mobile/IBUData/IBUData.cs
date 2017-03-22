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

        public static void UpdateData()
        {
            UpdateOverview();
        }

        private static async void UpdateOverview()
        {
            try
            {
                var client = new RestClient("http://54.244.213.136/overview.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("Token", Settings.Token, ParameterType.GetOrPost);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                Overview tempOverview = JsonConvert.DeserializeObject<Overview>(data);
                if (tempOverview.StudentID == "Invalid Token!")
                {
                    CurrentApp.Current.LogOutAction.Invoke();
                    return;
                }
                if (Overview == null || Double.Parse(tempOverview.LastModified) > Double.Parse(Overview.LastModified))
                {
                    Settings.OverviewData = data;
                    SetUpData();
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