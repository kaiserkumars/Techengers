//---------------------------------------------------------------------------
//
// <copyright file="GeeksForGeeksListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class GeeksForGeeksListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public GeeksForGeeksListPage()
        {
			this.ViewModel = ListViewModel.CreateNew(Singleton<GeeksForGeeksConfig>.Instance);

            this.InitializeComponent();

        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

    }
}
