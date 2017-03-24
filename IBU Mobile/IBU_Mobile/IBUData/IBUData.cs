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
        public static Page MainPage;
        public static OverviewData OverviewData = new OverviewData();
        public static GradesData GradesData = new GradesData();
        public static UserData UserData = new UserData();
        public static void SetUpData()
        {
            SetUpUser();
            SetUpOverview();
            SetUpGrades();
        }

        public static void UpdateData()
        {
            UpdateUser();
            UpdateOverview();
            UpdateGrades();
        }

        public static async void SetUpUser()
        {
            UserData = JsonConvert.DeserializeObject<UserData>(Settings.UserData);
        }
        public static async void UpdateUser()
        {
            try
            {
                var client = new RestClient("http://54.244.213.136/user.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("Token", Settings.Token, ParameterType.GetOrPost);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                UserData tempUserData = JsonConvert.DeserializeObject<UserData>(data);
                if (tempUserData.StudentID == "Invalid Token!")
                {
                    CurrentApp.Current.LogOutAction.Invoke();
                    return;
                }
                if (UserData?.LastModified == null || Double.Parse(tempUserData.LastModified) > Double.Parse(UserData.LastModified))
                {
                    Settings.UserData = data;
                    SetUpUser();
                    if (MainPage.GetType() == typeof(MainPageMaster))
                    {
                        ((MainPageMaster)MainPage).SetUpAction.Invoke();
                    }
                }
            }
            catch (Exception e)
            {
                string exception = e.Message;
                //Oh Well... Maybe no internet?
            }
        }


        public static async void SetUpOverview()
        {
            OverviewData = JsonConvert.DeserializeObject<OverviewData>(Settings.OverviewData);
        }
        private static async void UpdateOverview()
        {
            try
            {
                var client = new RestClient("http://54.244.213.136/overview.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                if (OverviewData?.LastModified != null)
                    request.AddParameter("LastModified", OverviewData.LastModified, ParameterType.GetOrPost);
                request.AddParameter("Token", Settings.Token, ParameterType.GetOrPost);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                OverviewData tempOverviewData = JsonConvert.DeserializeObject<OverviewData>(data);
                if (tempOverviewData.LastModified == "Invalid Token!")
                {
                    CurrentApp.Current.LogOutAction.Invoke();
                    return;
                }
                if (OverviewData?.LastModified == null || Double.Parse(tempOverviewData.LastModified) > Double.Parse(OverviewData.LastModified))
                {
                    Settings.OverviewData = data;
                    SetUpOverview();
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
        public static async void SetUpGrades()
        {
            GradesData = JsonConvert.DeserializeObject<GradesData>(Settings.GradesData);
        }
        private static async void UpdateGrades()
        {
            try
            {
                var client = new RestClient("http://54.244.213.136/grades.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                if(GradesData?.LastModified != null)
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
                if (GradesData?.LastModified == null || Double.Parse(tempGradesData.LastModified) > Double.Parse(GradesData.LastModified))
                {
                    Settings.GradesData = data;
                    SetUpGrades();
                    if (CurrentPage.GetType() == typeof(GradesPage))
                    {
                        ((GradesPage)CurrentPage).SetUpAction.Invoke();
                    }
                }
            }
            catch (Exception e)
            {
                string exception = e.Message;
            }
        }
    }

    public class UserData
    {
        public string LastModified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public string StudentID { get; set; }
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
        public string Semester { get; set; }
        public string Course { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Percent { get; set; }
        public string Grade { get; set; }
    }
}