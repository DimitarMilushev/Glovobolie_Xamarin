﻿using GlovobolieApp.Models;
using GlovobolieApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GlovobolieApp.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Product Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}