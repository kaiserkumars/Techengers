//---------------------------------------------------------------------------
//
// <copyright file="CodeCalenderDetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>4/23/2016 4:05:13 PM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.Rss;
using TechengersBeta.Sections;
using TechengersBeta.Services;
using TechengersBeta.ViewModels;
using AppStudio.Uwp;

namespace TechengersBeta.Pages
{
    public sealed partial class CodeCalenderDetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public CodeCalenderDetailPage()
        {
            this.ViewModel = DetailViewModel.CreateNew(Singleton<CodeCalenderConfig>.Instance);

            this.InitializeComponent();
        }

        public DetailViewModel ViewModel { get; set; }        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync(e.Parameter as ItemViewModel);

            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }

        private void ChangeFontSize(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AppBarButton button = sender as AppBarButton;
            int newFontSize = Int32.Parse(button.Tag.ToString());
            mainPanel.BodyFontSize = newFontSize;
        }
    }
}
