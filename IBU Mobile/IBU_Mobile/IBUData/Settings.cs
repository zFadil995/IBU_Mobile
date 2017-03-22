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

        private const string _overviewData = "OverviewData";
        private static readonly string _overviewDataDefault = String.Empty;

        public static string OverviewData
        {
            get { return AppSettings.GetValueOrDefault<string>(_overviewData, _overviewDataDefault); }
            set { AppSettings.AddOrUpdateValue<string>(_overviewData, value); }
        }

    }
}