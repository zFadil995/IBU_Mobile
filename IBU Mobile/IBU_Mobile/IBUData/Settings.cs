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

    }
}