using System;
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


        private const string _token = "Token";
        private static readonly string _tokenDefault = String.Empty;

        public static string Token
        {
            get { return AppSettings.GetValueOrDefault<string>(_token, _tokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_token, value); }
        }

        private const string _userData = "UserData";
        private static readonly string _userDataDefault = String.Empty;

        public static string UserData
        {
            get { return AppSettings.GetValueOrDefault<string>(_userData, _userDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_userData, value); }
        }

        private const string _overviewData = "OverviewData";
        private static readonly string _overviewDataDefault = String.Empty;

        public static string OverviewData
        {
            get { return AppSettings.GetValueOrDefault<string>(_overviewData, _overviewDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_overviewData, value); }
        }

        private const string _gradesData = "GradesData";
        private static readonly string _gradesDataDefault = String.Empty;

        public static string GradesData
        {
            get { return AppSettings.GetValueOrDefault<string>(_gradesData, _gradesDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_gradesData, value); }
        }

        private const string _documentsData = "DocumentsData";
        private static readonly string _documentsDataDefault = String.Empty;

        public static string DocumentsData
        {
            get { return AppSettings.GetValueOrDefault<string>(_documentsData, _documentsDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_documentsData, value); }
        }

        private const string _attendanceData = "AttendanceData";
        private static readonly string _attendanceDataDefault = String.Empty;

        public static string AttendanceData
        {
            get { return AppSettings.GetValueOrDefault<string>(_attendanceData, _attendanceDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_attendanceData, value); }
        }

        private const string _messagesData = "MessagesData";
        private static readonly string _messagesDataDefault = String.Empty;

        public static string MessagesData
        {
            get { return AppSettings.GetValueOrDefault<string>(_messagesData, _messagesDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_messagesData, value); }
        }

        private const string _lmsData = "LMSData";
        private static readonly string _lmsDataDefault = String.Empty;

        public static string LMSData
        {
            get { return AppSettings.GetValueOrDefault<string>(_lmsData, _lmsDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_lmsData, value); }
        }

        private const string _examsData = "examsData";
        private static readonly string _examsDataDefault = String.Empty;

        public static string ExamsData
        {
            get { return AppSettings.GetValueOrDefault<string>(_examsData, _examsDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_examsData, value); }
        }

        public static void ClearSettings()
        {
            Token = string.Empty;
            UserData = string.Empty;
            OverviewData = string.Empty;
            GradesData = string.Empty;
            AttendanceData = string.Empty;
            MessagesData = string.Empty;
            LMSData = string.Empty;
            ExamsData = string.Empty;
            DocumentsData = string.Empty;
        }

    }
}