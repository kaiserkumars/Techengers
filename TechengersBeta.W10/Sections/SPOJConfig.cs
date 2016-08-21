


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
    public class SPOJConfig : SectionConfigBase<SPOJ1Schema>
    {
	    public override Func<Task<IEnumerable<SPOJ1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new DynamicStorageDataConfig
                {
                    Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=7b5998c0-2844-4da6-8ddd-38f6ef7392b8&appId=d2b59d42-84cc-40b4-8107-682771b95228"),
                    AppId = "d2b59d42-84cc-40b4-8107-682771b95228",
                    StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                    DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string
                };

                return () => Singleton<DynamicStorageDataProvider<SPOJ1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<SPOJ1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<SPOJ1Schema>
                {
                    Title = "SPOJ",

					PageTitle = "SPOJ",

                    ListNavigationInfo = NavigationInfo.FromPage("SPOJListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.Description = item.Description.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("SPOJDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<SPOJ1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, SPOJ1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl("");
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<SPOJ1Schema>>
                {
                };

                return new DetailPageConfig<SPOJ1Schema>
                {
                    Title = "SPOJ",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
