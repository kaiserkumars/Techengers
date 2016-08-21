


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.Menu;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using System.Linq;
using TechengersBeta.Config;
using TechengersBeta.ViewModels;

namespace TechengersBeta.Sections
{
    public class SolutionsConfig : SectionConfigBase<MenuSchema>
    {
	    public override Func<Task<IEnumerable<MenuSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new LocalStorageDataConfig
                {
                    FilePath = "/Assets/Data/Solutions.json"
                };

                return () => Singleton<LocalStorageDataProvider<MenuSchema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<MenuSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<MenuSchema>
                {
                    Title = "Solutions",

					PageTitle = "Solutions",

                    ListNavigationInfo = NavigationInfo.FromPage("SolutionsListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title;						
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Icon);
                    },
                    DetailNavigation = (item) =>
                    {
                        return item.ToNavigationInfo();
                    }
                };
            }
        }

        public override DetailPageConfig<MenuSchema> DetailPage
        {
            get { return null; }
        }
    }
}
