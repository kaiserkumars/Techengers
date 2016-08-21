using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.Rss;
using AppStudio.DataProviders.LocalStorage;
using TechengersBeta.Sections;


namespace TechengersBeta.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        public MainViewModel(int visibleItems) : base()
        {
            PageTitle = "Techengers Beta";
            Solutions = ListViewModel.CreateNew(Singleton<SolutionsConfig>.Instance);
            CodeCalender = ListViewModel.CreateNew(Singleton<CodeCalenderConfig>.Instance, visibleItems);
            GeeksForGeeks = ListViewModel.CreateNew(Singleton<GeeksForGeeksConfig>.Instance, visibleItems);
            Wired = ListViewModel.CreateNew(Singleton<WiredConfig>.Instance, visibleItems);
            TechCrunch = ListViewModel.CreateNew(Singleton<TechCrunchConfig>.Instance, visibleItems);
            Tech2 = ListViewModel.CreateNew(Singleton<Tech2Config>.Instance, visibleItems);
            Digit = ListViewModel.CreateNew(Singleton<DigitConfig>.Instance, visibleItems);
            Gizmodo = ListViewModel.CreateNew(Singleton<GizmodoConfig>.Instance, visibleItems);
            Quora = ListViewModel.CreateNew(Singleton<QuoraConfig>.Instance, visibleItems);
            StackOverflow = ListViewModel.CreateNew(Singleton<StackOverflowConfig>.Instance, visibleItems);

            Actions = new List<ActionInfo>();

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = new RelayCommand(Refresh),
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

        public string PageTitle { get; set; }
        public ListViewModel Solutions { get; private set; }
        public ListViewModel CodeCalender { get; private set; }
        public ListViewModel GeeksForGeeks { get; private set; }
        public ListViewModel Wired { get; private set; }
        public ListViewModel TechCrunch { get; private set; }
        public ListViewModel Tech2 { get; private set; }
        public ListViewModel Digit { get; private set; }
        public ListViewModel Gizmodo { get; private set; }
        public ListViewModel Quora { get; private set; }
        public ListViewModel StackOverflow { get; private set; }

        public RelayCommand<INavigable> SectionHeaderClickCommand
        {
            get
            {
                return new RelayCommand<INavigable>(item =>
                    {
                        NavigationService.NavigateTo(item);
                    });
            }
        }

        public DateTime? LastUpdated
        {
            get
            {
                return GetViewModels().Select(vm => vm.LastUpdated)
                            .OrderByDescending(d => d).FirstOrDefault();
            }
        }

        public List<ActionInfo> Actions { get; private set; }

        public bool HasActions
        {
            get
            {
                return Actions != null && Actions.Count > 0;
            }
        }

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private async void Refresh()
        {
            var refreshDataTasks = GetViewModels()
                                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

            await Task.WhenAll(refreshDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Solutions;
            yield return CodeCalender;
            yield return GeeksForGeeks;
            yield return Wired;
            yield return TechCrunch;
            yield return Tech2;
            yield return Digit;
            yield return Gizmodo;
            yield return Quora;
            yield return StackOverflow;
        }
    }
}
