using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;

namespace GitFlowAVH.ViewModels
{
    public class HotfixesViewModel : ViewModelBase
    {
        public ICommand PublishHotfixBranchCommand { get; private set; }
        public ICommand TrackHotfixBranchCommand { get; private set; }

        public HotfixesViewModel(IGitFlowSection te)
            : base(te)
        {
            PublishHotfixBranchCommand = new RelayCommand(p => PublishHotfixBranch(), p => CanPublishHotfixBranch);
            TrackHotfixBranchCommand = new RelayCommand(p => TrackHotfixBranch(), p => CanTrackHotfixBranch);

            HideProgressBar();
        }

        public bool CanPublishHotfixBranch
        {
            get
            {
                return SelectedHotfix != null && !SelectedHotfix.IsRemote && !SelectedHotfix.IsTracking;
            }
        }

        public bool CanTrackHotfixBranch
        {
            get
            {
                return SelectedHotfix != null && SelectedHotfix.IsRemote && !SelectedHotfix.IsTracking;
            }
        }

        public bool CanCheckoutHotfixBranch
        {
            get
            {
                return SelectedHotfix != null && !SelectedHotfix.IsCurrentBranch && !SelectedHotfix.IsRemote;
            }
        }

        public void PublishHotfixBranch()
        {
            try
            {
                Logger.Event("PublishHotfixBranch");
                GitFlowPage.ActiveOutputWindow();
                ShowProgressBar();
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                var result = gf.PublishHotfix(SelectedHotfix.Name);
                if (!result.Success)
                {
                    Te.ShowErrorNotification(result.CommandOutput);
                }

                HideProgressBar();
                Update();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }

        }

        public void TrackHotfixBranch()
        {
            try
            {
                Logger.Event("TrackHotfixBranch");
                GitFlowPage.ActiveOutputWindow();
                ShowProgressBar();
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                var result = gf.TrackHotfix(SelectedHotfix.Name);
                if (!result.Success)
                {
                    Te.ShowErrorNotification(result.CommandOutput);
                }

                HideProgressBar();
                Update();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }

        }

        public List<BranchItem> AllHotfixes
        {
            get
            {
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                var list = gf.AllHotfixBranches.ToList();
                return list;
            }
        }

        public BranchItem SelectedHotfix { get; set; }

        public void Update()
        {
            OnPropertyChanged("AllHotfixes");
            OnPropertyChanged("NoItemsMessageVisibility");
        }

        public Visibility NoItemsMessageVisibility
        {
            get { return AllHotfixes.Any() ? Visibility.Collapsed : Visibility.Visible; }

        }

        private void ShowErrorMessage(string message)
        {
            Te.ShowErrorNotification(message);
        }
    }
}