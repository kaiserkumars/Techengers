using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppStudio.Uwp.Navigation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;

namespace TechengersBeta.Navigation
{
    public class AppNavigation
    {
        private NavigationNode _active;

        static AppNavigation()
        {

        }

        public NavigationNode Active
        {
            get
            {
                return _active;
            }
            set
            {
                if (_active != null)
                {
                    _active.IsSelected = false;
                }
                _active = value;
                if (_active != null)
                {
                    _active.IsSelected = true;
                }
            }
        }


        public ObservableCollection<NavigationNode> Nodes { get; private set; }

        public void LoadNavigation()
        {
            Nodes = new ObservableCollection<NavigationNode>();
		    var resourceLoader = new ResourceLoader();
			AddNode(Nodes, "Home", "\ue10f", string.Empty, "HomePage", true, @"Techengers Beta");
			var menuNodeSolutions = new GroupNavigationNode("Solutions", "\ue10c", string.Empty);
			AddNode(menuNodeSolutions.Nodes, "SPOJ", string.Empty, "/Assets/DataImages/spoj-3.png", "SPOJListPage");
			AddNode(menuNodeSolutions.Nodes, "HackerRank", string.Empty, "/Assets/DataImages/hackerank-7.png", "HackerRankListPage");
			Nodes.Add(menuNodeSolutions);
			AddNode(Nodes, "Code Calender", string.Empty, "/Assets/DataImages/cc.png", "CodeCalenderListPage", true);			
			AddNode(Nodes, "GeeksForGeeks", string.Empty, "/Assets/DataImages/gfg.png", "GeeksForGeeksListPage", true);			
			AddNode(Nodes, "Wired", string.Empty, "/Assets/DataImages/wired-2.png", "WiredListPage", true);			
			AddNode(Nodes, "TechCrunch", string.Empty, "/Assets/DataImages/tc.png", "TechCrunchListPage", true);			
			AddNode(Nodes, "Tech2", string.Empty, "/Assets/DataImages/tech2.png", "Tech2ListPage", true);			
			AddNode(Nodes, "Digit", string.Empty, "/Assets/DataImages/digit-1.png", "DigitListPage", true);			
			AddNode(Nodes, "Gizmodo", string.Empty, "/Assets/DataImages/gizmodo.png", "GizmodoListPage", true);			
			AddNode(Nodes, "Quora", string.Empty, "/Assets/DataImages/quora-1.png", "QuoraListPage", true);			
			AddNode(Nodes, "Stack Overflow", string.Empty, "/Assets/DataImages/so-icon.png", "StackOverflowListPage", true);			
			AddNode(Nodes, resourceLoader.GetString("NavigationPanePrivacy"), "\ue1f7", string.Empty, string.Empty, true, string.Empty, "http://appstudio.windows.com/home/appprivacyterms");            
        }

		private void AddNode(ObservableCollection<NavigationNode> nodes, string label, string fontIcon, string image, string pageName, bool isVisible = true, string title = null, string deepLinkUrl = null, bool isSelected = false)
        {
            if (nodes != null && isVisible)
            {
                var node = new ItemNavigationNode
                {
                    Title = title,
                    Label = label,
                    FontIcon = fontIcon,
                    Image = image,
                    IsSelected = isSelected,
                    IsVisible = isVisible,
                    NavigationInfo = NavigationInfo.FromPage(pageName)
                };
                if (!string.IsNullOrEmpty(deepLinkUrl))
                {
                    node.NavigationInfo = new NavigationInfo()
                    {
                        NavigationType = NavigationType.DeepLink,
                        TargetUri = new Uri(deepLinkUrl, UriKind.Absolute)
                    };
                }
                nodes.Add(node);
            }            
        }

        public NavigationNode FindPage(Type pageType)
        {
            return GetAllItemNodes(Nodes).FirstOrDefault(n => n.NavigationInfo.NavigationType == NavigationType.Page && n.NavigationInfo.TargetPage == pageType.Name);
        }

        private IEnumerable<ItemNavigationNode> GetAllItemNodes(IEnumerable<NavigationNode> nodes)
        {
            foreach (var node in nodes)
            {
                if (node is ItemNavigationNode)
                {
                    yield return node as ItemNavigationNode;
                }
                else if(node is GroupNavigationNode)
                {
                    var gNode = node as GroupNavigationNode;

                    foreach (var innerNode in GetAllItemNodes(gNode.Nodes))
                    {
                        yield return innerNode;
                    }
                }
            }
        }
    }
}
