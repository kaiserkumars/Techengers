using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechengersBeta.Sections;
namespace TechengersBeta.ViewModels
{
    public class SearchViewModel : ObservableBase
    {
        public SearchViewModel() : base()
        {
			PageTitle = "Techengers Beta";
            SPOJ = ListViewModel.CreateNew(Singleton<SPOJConfig>.Instance);
            HackerRank = ListViewModel.CreateNew(Singleton<HackerRankConfig>.Instance);
            CodeCalender = ListViewModel.CreateNew(Singleton<CodeCalenderConfig>.Instance);
            GeeksForGeeks = ListViewModel.CreateNew(Singleton<GeeksForGeeksConfig>.Instance);
            Wired = ListViewModel.CreateNew(Singleton<WiredConfig>.Instance);
            TechCrunch = ListViewModel.CreateNew(Singleton<TechCrunchConfig>.Instance);
            Tech2 = ListViewModel.CreateNew(Singleton<Tech2Config>.Instance);
            Digit = ListViewModel.CreateNew(Singleton<DigitConfig>.Instance);
            Gizmodo = ListViewModel.CreateNew(Singleton<GizmodoConfig>.Instance);
            Quora = ListViewModel.CreateNew(Singleton<QuoraConfig>.Instance);
            StackOverflow = ListViewModel.CreateNew(Singleton<StackOverflowConfig>.Instance);
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel SPOJ { get; private set; }
        public ListViewModel HackerRank { get; private set; }
        public ListViewModel CodeCalender { get; private set; }
        public ListViewModel GeeksForGeeks { get; private set; }
        public ListViewModel Wired { get; private set; }
        public ListViewModel TechCrunch { get; private set; }
        public ListViewModel Tech2 { get; private set; }
        public ListViewModel Digit { get; private set; }
        public ListViewModel Gizmodo { get; private set; }
        public ListViewModel Quora { get; private set; }
        public ListViewModel StackOverflow { get; private set; }
        
		public string PageTitle { get; set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return SPOJ;
            yield return HackerRank;
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
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
