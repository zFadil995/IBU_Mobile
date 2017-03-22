﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IBU_Mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IBU_Mobile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesPage : ContentPage
    {
        public GradesPage()
        {
            InitializeComponent();
            IBUData.CurrentPage = this;
            SetUp();
        }

        private void SetUp()
        {
        }

        public Action SetUpAction
        {
            get
            {
                return new Action(SetUp);
            }
        }
    }
}
