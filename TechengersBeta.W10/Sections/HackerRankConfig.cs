


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using Windows.Storage;
using AppStudio.DataProviders.DynamicStorage;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using Windows.ApplicationModel.Appointments;
using System.Linq;
using TechengersBeta.Config;
using TechengersBeta.ViewModels;

namespace TechengersBeta.Sections
{
    public class HackerRankConfig : SectionConfigBase<HackerRank1Schema>
    {
	    public override Func<Task<IEnumerable<HackerRank1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new DynamicStorageDataConfig
                {
                    Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=94d4dfc9-a68c-493e-87f5-cb9c7bafd246&appId=d2b59d42-84cc-40b4-8107-682771b95228"),
                    AppId = "d2b59d42-84cc-40b4-8107-682771b95228",
                    StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                    DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string
                };

                return () => Singleton<DynamicStorageDataProvider<HackerRank1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<HackerRank1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<HackerRank1Schema>
                {
                    Title = "HackerRank",

					PageTitle = "HackerRank",

                    ListNavigationInfo = NavigationInfo.FromPage("HackerRankListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.Description = item.Description.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("HackerRankDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<HackerRank1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, HackerRank1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Hackerrank";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl("");
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<HackerRank1Schema>>
                {
                };

                return new DetailPageConfig<HackerRank1Schema>
                {
                    Title = "HackerRank",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
