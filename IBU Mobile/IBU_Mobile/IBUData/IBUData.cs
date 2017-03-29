﻿using System;
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
        public static UserData UserData = new UserData();
        public static OverviewData OverviewData = new OverviewData();
        public static GradesData GradesData = new GradesData();
        public static MessagesData MessagesData = new MessagesData();
        public static DocumentsData DocumentsData = new DocumentsData();

        public static ToolbarItem OverviewToolbar = new ToolbarItem("Old Data", "failure.png", UpdateOverview);
        public static ToolbarItem GradesToolbar = new ToolbarItem("Old Data", "failure.png", UpdateGrades);
        public static ToolbarItem AttendanceToolbar = new ToolbarItem("Old Data", "failure.png", () => { });
        public static ToolbarItem MessagesToolbar = new ToolbarItem("Old Data", "failure.png", UpdateMessages);
        public static ToolbarItem LMSToolbar = new ToolbarItem("Old Data", "failure.png", () => { });
        public static ToolbarItem DocumentsToolbar = new ToolbarItem("Old Data", "failure.png", UpdateDocuments);

        public static void SetUpData()
        {
            SetUpUser();
            SetUpOverview();
            SetUpGrades();
            SetUpMessages();
            SetUpDocuments();
        }

        public static void UpdateData()
        {
            UpdateUser();
            UpdateOverview();
            UpdateGrades();
            UpdateMessages();
            UpdateDocuments();
        }

        public static void SetUpUser()
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


        public static void SetUpOverview()
        {
            OverviewData = JsonConvert.DeserializeObject<OverviewData>(Settings.OverviewData);
        }
        public static async void UpdateOverview()
        {
            try
            {
                OverviewToolbar.Icon = "loading.png";
                OverviewToolbar.Text = "Loading Data";
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
        public static void SetUpGrades()
        {
            GradesData = JsonConvert.DeserializeObject<GradesData>(Settings.GradesData);
            if (GradesData != null)
            {
                GradesData.CurrentSemester.Courses =
                    GradesData.CurrentSemester.Courses.OrderBy(course => course.CourseCode).ToArray();

                foreach (Semesters gradesDataPreviousSemester in GradesData.PreviousSemesters)
                {
                    gradesDataPreviousSemester.Courses =
                        gradesDataPreviousSemester.Courses.OrderBy(course => course.CourseCode).ToArray();
                }

                GradesData.PreviousSemesters =
                    GradesData.PreviousSemesters.OrderBy(semester => semester.Year)
                        .ThenBy(semester => semester.Semester)
                        .Reverse()
                        .ToArray();
            }
        }
        public static async void UpdateGrades()
        {
            try
            {
                GradesToolbar.Icon = "loading.png";
                GradesToolbar.Text = "Loading Data";
                var client = new RestClient("http://54.244.213.136/grades.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                if (GradesData?.LastModified != null)
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

        public static void SetUpMessages()
        {
            MessagesData = JsonConvert.DeserializeObject<MessagesData>(Settings.MessagesData);
            if (MessagesData != null)
            {
                MessagesData.Messages = MessagesData.Messages.OrderBy(message => message.date).Reverse().ToArray();
            }
            ((MainPageMaster) MainPage)?.SetMessagesAction();
        }

        public static async void UpdateMessages()
        {
            try
            {
                MessagesToolbar.Icon = "loading.png";
                MessagesToolbar.Text = "Loading Data";
                var client = new RestClient("http://54.244.213.136/messages.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                if (MessagesData?.LastModified != null)
                    request.AddParameter("LastModified", MessagesData.LastModified, ParameterType.GetOrPost);
                request.AddParameter("Token", Settings.Token, ParameterType.GetOrPost);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                MessagesData tempMessagesData = JsonConvert.DeserializeObject<MessagesData>(data);
                if (tempMessagesData.LastModified == "Invalid Token!")
                {
                    CurrentApp.Current.LogOutAction.Invoke();
                    return;
                }
                else if (MessagesData?.LastModified == null || Double.Parse(tempMessagesData.LastModified) > Double.Parse(MessagesData.LastModified))
                {
                    Settings.MessagesData = data;
                    SetUpMessages();
                    if (CurrentPage.GetType() == typeof(MessagesPage))
                    {
                        ((MessagesPage)CurrentPage).SetUpAction.Invoke();
                    }
                }
                MessagesToolbar.Icon = "success.png";
                MessagesToolbar.Text = "Successfully Loaded";
            }
            catch (Exception e)
            {
                MessagesToolbar.Icon = "failure.png";
                MessagesToolbar.Text = "Loading Failed";
                string exception = e.Message;
            }
        }

        public static void SetUpDocuments()
        {
            DocumentsData = JsonConvert.DeserializeObject<DocumentsData>(Settings.DocumentsData);

            if (DocumentsData != null)
            {
                DocumentsData.Documents = DocumentsData.Documents.OrderBy(document => document.date).Reverse().ToArray();
            }
        }

        public static async void UpdateDocuments()
        {
            try
            {
                DocumentsToolbar.Icon = "loading.png";
                DocumentsToolbar.Text = "Loading Data";
                var client = new RestClient("http://54.244.213.136/documents.php");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                if (DocumentsData?.LastModified != null)
                    request.AddParameter("LastModified", DocumentsData.LastModified, ParameterType.GetOrPost);
                request.AddParameter("Token", Settings.Token, ParameterType.GetOrPost);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                DocumentsData tempDocumentsData = JsonConvert.DeserializeObject<DocumentsData>(data);
                if (tempDocumentsData.LastModified == "Invalid Token!")
                {
                    CurrentApp.Current.LogOutAction.Invoke();
                    return;
                }
                else if (DocumentsData?.LastModified == null || Double.Parse(tempDocumentsData.LastModified) > Double.Parse(DocumentsData.LastModified))
                {
                    Settings.DocumentsData = data;
                    SetUpDocuments();
                    if (CurrentPage.GetType() == typeof(DocumentRequestPage))
                    {
                        ((DocumentRequestPage)CurrentPage).SetUpAction.Invoke();
                    }
                }
                DocumentsToolbar.Icon = "success.png";
                DocumentsToolbar.Text = "Successfully Loaded";
            }
            catch (Exception e)
            {
                DocumentsToolbar.Icon = "failure.png";
                DocumentsToolbar.Text = "Loading Failed";
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

    public class DocumentsData
    {
        public string LastModified { get; set; }
        public Documents[] Documents { get; set; }
    }

    public class Documents
    {
        public int DocumentType { get; set; }
        public int Turkish { get; set; }
        public int English { get; set; }
        public int Bosnian { get; set; }
        public string Date { get { return date.ToString("dd/MM/yy"); } set { date = DateTime.Parse(value); } }
        public int Status { get; set; }
        public DateTime date;
    }

    public class MessagesData
    {
        public string LastModified { get; set; }
        public Messages[] Messages { get; set; }
    }

    public class Messages
    {
        public int MessageID { get; set; }
        public string Date { get { return date.ToString("dd/MM/yy"); } set { date = DateTime.Parse(value); } }
        public string Title { get; set; }
        public string Contents { get; set; }
        public int Status { get; set; }
        public DateTime date;
    }
}