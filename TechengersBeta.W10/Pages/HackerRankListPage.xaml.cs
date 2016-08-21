//---------------------------------------------------------------------------
//
// <copyright file="HackerRankListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>4/23/2016 4:05:13 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.DynamicStorage;
using TechengersBeta.Sections;
using TechengersBeta.ViewModels;
using AppStudio.Uwp;

namespace TechengersBeta.Pages
{
    public sealed partial class HackerRankListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public HackerRankListPage()
        {
			this.ViewModel = ListViewModel.CreateNew(Singleton<HackerRankConfig>.Instance);

            this.InitializeComponent();

        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

    }
}
