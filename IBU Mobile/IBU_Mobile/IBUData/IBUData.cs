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
        public static OverviewData OverviewData = new OverviewData();
        public static GradesData GradesData = new GradesData();
        public static void SetUpData()
        {
            OverviewData = JsonConvert.DeserializeObject<OverviewData>(Settings.OverviewData);
            GradesData = JsonConvert.DeserializeObject<GradesData>(Settings.GradesData);
        }

        public static void UpdateData()
        {
            UpdateOverview();
            UpdateGrades();
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
                OverviewData tempOverviewData = JsonConvert.DeserializeObject<OverviewData>(data);
                if (tempOverviewData.LastModified == "Invalid Token!")
                {
                    CurrentApp.Current.LogOutAction.Invoke();
                    return;
                }
                if (OverviewData == null || OverviewData.LastModified == null || Double.Parse(tempOverviewData.LastModified) > Double.Parse(OverviewData.LastModified))
                {
                    Settings.OverviewData = data;
                    SetUpData();
                    if (CurrentPage.GetType() == typeof(OverviewPage))
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
        private static async void UpdateGrades()
        {
            try
            {
                var client = new RestClient("http://54.244.213.136/grades.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                if(GradesData.LastModified != null)
                    request.AddParameter("LastModified", GradesData.LastModified, ParameterType.GetOrPost);
                request.AddParameter("Token", Settings.Token, ParameterType.GetOrPost);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                GradesData tempGradesData = JsonConvert.DeserializeObject<GradesData>(data);
                if (tempGradesData.LastModified == "Invalid Token!")
                {
                    CurrentApp.Current.LogOutAction.Invoke();
                    return;
                }
                if (GradesData == null || GradesData.LastModified == null || Double.Parse(tempGradesData.LastModified) > Double.Parse(GradesData.LastModified))
                {
                    Settings.GradesData = data;
                    SetUpData();
                    if (CurrentPage.GetType() == typeof(GradesPage))
                    {
                        ((GradesPage)CurrentPage).SetUpAction.Invoke();
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

    public class OverviewData
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

    public class GradesData
    {
        public string LastModified { get; set; }
        public Grades[] Grades { get; set; }
    }

    public class Grades
    {
        public string StudentID { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Percent { get; set; }
        public string Grade { get; set; }
    }
}