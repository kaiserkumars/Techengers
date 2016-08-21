//---------------------------------------------------------------------------
//
// <copyright file="CodeCalenderListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>4/23/2016 4:05:13 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Rss;
using TechengersBeta.Sections;
using TechengersBeta.ViewModels;
using AppStudio.Uwp;

namespace TechengersBeta.Pages
{
    public sealed partial class CodeCalenderListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public CodeCalenderListPage()
        {
			this.ViewModel = ListViewModel.CreateNew(Singleton<CodeCalenderConfig>.Instance);

            this.InitializeComponent();

        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

    }
}
