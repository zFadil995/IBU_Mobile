using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace IBU_Mobile.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }


        private const string _overviewData = "OverviewData";

        private static readonly string _overviewDataDefault =
            "{\n  \"LastModified\": \"1490112702\",\n  \"Courses\": [\n    \"Special Topics in Database Systems /A\",\n    \"Introduction to Machine Learning /A\",\n    \"Introduction to Network Security /A\",\n    \"Leadership and Corporate Responsibility /D\", \n    \"Senior Design Project /A\"\n  ],\n  \n  \"StudentID\": \"301014015\",\n  \"Email\": \"fadil.zilic@stu.ibu.edu.ba\",\n  \"Attendance\": \"Continuing\",\n  \"Nationality\": \"Bosnian\",\n  \"TotalPrice\": \"0 BAM\",\n  \"TotalPaid\": \"0 BAM\",\n  \"TotalToPay\": \"0 BAM\"\n  \n}";


        public static string OverviewData
        {
            get { return AppSettings.GetValueOrDefault<string>(_overviewData, _overviewDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_overviewData, value); }
        }

    }
}