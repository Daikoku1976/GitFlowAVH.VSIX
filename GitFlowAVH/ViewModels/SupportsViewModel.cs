using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GitFlowAVH;
using System;

namespace GitFlowAVH.ViewModels
{

    public class SupportsViewModel : ViewModelBase
    {
        public SupportsViewModel(IGitFlowSection te)
            : base(te)
        {
            HideProgressBar();
        }

        public List<BranchItem> AllSupports
        {
            get
            {
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                var list = gf.AllSupportBranches.ToList();
                return list;
            }
        }

        public BranchItem SelectedSupport { get; set; }

        public void Update()
        {
            OnPropertyChanged("AllSupports");
            OnPropertyChanged("NoItemsMessageVisibility");
        }

        public Visibility NoItemsMessageVisibility
        {
            get { return AllSupports.Any() ? Visibility.Collapsed : Visibility.Visible; }

        }

        private void ShowErrorMessage(string message)
        {
            Te.ShowErrorNotification(message);
        }

    }
}