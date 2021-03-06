﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IBU_Mobile.Helpers;
using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            StudentID.Completed += SignIn;
            Password.Completed += SignIn;
        }

        private async void SignIn(object sender, EventArgs e)
        {
            SignInButton.IsEnabled = false;
            SignInButton.Text = "SIGNING IN";
            try
            {
                var client = new RestClient("http://mobile.ibu.edu.ba/login.php");
                var request = new RestRequest(Method.POST);
                //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("StudentID", StudentID.Text, ParameterType.GetOrPost);
                request.AddParameter("Password", Password.Text, ParameterType.GetOrPost);
                IRestResponse response = await client.Execute(request);
                string data = response.Content;
                Token token = JsonConvert.DeserializeObject<Token>(data);

                if (token.token == "An error occured." || token.token == "Incorrect Student ID or Password!" || token.token.Length < 64)
                {
                    await DisplayAlert("Warning", token.token, "OK");
                }
                else
                {
                    Settings.Token = token.token;
                    CurrentApp.Current.SetUpAction.Invoke();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "Internet Connection Failed", "OK");
            }

            SignInButton.Text = "SIGN IN";
            SignInButton.IsEnabled = true;
        }
    }

    public class Token
    {
        public string token { get; set; }
    }
}
