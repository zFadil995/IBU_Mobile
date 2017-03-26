using System;
using System.Linq;
using Xamarin.Forms;
using IBU_Mobile.Helpers;
using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System.Threading.Tasks;
using IBU_Mobile.Pages;

namespace IBU_Mobile
{
    public static class IBUData
    {
        public static Page CurrentPage;
        public static Page MainPage;
        public static OverviewData OverviewData = new OverviewData();
        public static ToolbarItem OverviewToolbar = new ToolbarItem("Loading Data", "loading.png", UpdateOverview);
        public static ToolbarItem GradesToolbar = new ToolbarItem("Loading Data", "loading.png", UpdateGrades);
        public static ToolbarItem AttendanceToolbar = new ToolbarItem("Loading Data", "loading.png", () => { });
        public static ToolbarItem MessagesToolbar = new ToolbarItem("Loading Data", "loading.png", () => { });
        public static ToolbarItem LMSToolbar = new ToolbarItem("Loading Data", "loading.png", () => { });
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
                else if (UserData?.LastModified == null || Double.Parse(tempUserData.LastModified) > Double.Parse(UserData.LastModified))
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
                else if (OverviewData?.LastModified == null || Double.Parse(tempOverviewData.LastModified) > Double.Parse(OverviewData.LastModified))
                {
                    Settings.OverviewData = data;
                    SetUpOverview();
                    if (CurrentPage.GetType() == typeof(OverviewPage))
                    {
                        ((OverviewPage)CurrentPage).SetUpAction.Invoke();
                    }
                }

                OverviewToolbar.Icon = "success.png";
                OverviewToolbar.Text = "Successfully Loaded";
            }
            catch (Exception e)
            {
                OverviewToolbar.Icon = "failure.png";
                OverviewToolbar.Text = "Loading Failed";
                string exception = e.Message;
                //Oh Well... Maybe no internet?
            }
        }
        public static async void SetUpGrades()
        {
            GradesData = JsonConvert.DeserializeObject<GradesData>(Settings.GradesData);

            GradesData.CurrentSemester.Courses =
                GradesData.CurrentSemester.Courses.OrderBy(course => course.CourseCode).ToArray();

            foreach (Semesters gradesDataPreviousSemester in GradesData.PreviousSemesters)
            {
                gradesDataPreviousSemester.Courses = gradesDataPreviousSemester.Courses.OrderBy(course => course.CourseCode).ToArray();
            }

            GradesData.PreviousSemesters =
                GradesData.PreviousSemesters.OrderBy(semester => semester.Year).ThenBy(semester => semester.Semester).Reverse().ToArray();

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
                else if (GradesData?.LastModified == null || Double.Parse(tempGradesData.LastModified) > Double.Parse(GradesData.LastModified))
                {
                    Settings.GradesData = data;
                    SetUpGrades();
                    if (CurrentPage.GetType() == typeof(GradesPage))
                    {
                        ((GradesPage)CurrentPage).SetUpAction.Invoke();
                    }
                }
                GradesToolbar.Icon = "success.png";
                GradesToolbar.Text = "Successfully Loaded";
            }
            catch (Exception e)
            {
                GradesToolbar.Icon = "failure.png";
                GradesToolbar.Text = "Loading Failed";
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
        public Semesters CurrentSemester { get; set; }
        public Semesters[] PreviousSemesters { get; set; }
    }

    public class Semesters
    {
        public int Year { get; set; }
        public int Semester { get; set; }
        public Courses[] Courses { get; set; }

    }

    public class Courses
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int FinalGrade { get; set; }
        public Grades[] Grades { get; set; }
    }

    public class Grades
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public int Percent { get; set; }
        public int Grade { get; set; }
    }
}