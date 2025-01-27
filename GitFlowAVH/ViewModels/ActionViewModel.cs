﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GitFlowAVH;
using Microsoft.TeamFoundation.Controls.WPF.TeamExplorer;

namespace GitFlowAVH.ViewModels
{
    public class ActionViewModel : ViewModelBase
    {
        private Visibility showStartFeature;
        private Visibility showStartBugfix;
        private Visibility showStartRelease;
        private Visibility showStartSupport;
        private Visibility showStartHotfix;

        private Visibility showFinishFeature;
        private Visibility showFinishBugfix;
        private Visibility showFinishRelease;
        private Visibility showFinishHotfix;

        private Visibility showPullRequestFeature;
        private Visibility showPullRequestBugfix;
        private Visibility showPullRequestHotfix;

        private string featureName;
        private string bugfixName;
        private string releaseName;
        private string supportName;
        private string supportBase;
        private string hotfixName;

        private bool featureRebaseOnDevelopmentBranch;
        private bool featureDeleteLocalBranch;
        private bool featureDeleteRemoteBranch;
        private bool featureSquash;
        private bool featureNoFastForward;
        private string featurePullRequestWorkItems;
        private bool bugfixRebaseOnDevelopmentBranch;
        private bool bugfixDeleteLocalBranch;
        private bool bugfixDeleteRemoteBranch;
        private bool bugfixSquash;
        private bool bugfixNoFastForward;
        private string bugfixPullRequestWorkItems;
        private bool releaseDeleteBranch;
        private string releaseTagMessage;
        private bool releaseForceDeletion;
        private bool releasePushChanges;
		private bool releaseNoBackMerge;
        private bool hotfixDeleteBranch;
        private bool hotfixPushChanges;
        private bool hotfixForceDeletion;
        private string hotfixTagMessage;
        private string hotfixPullRequestWorkItems;
        private ListItem selectedFeature;
        private ListItem selectedBugfix;
        private bool hotfixTagMessageSelected;
        private bool releaseTagMessageSelected;
        private ListItem selectedHotfix;
        private ListItem selectedRelease;
        private ListItem selectedSupport;

        public ICommand StartFeatureDropDownCommand { get; private set; }
        public ICommand StartFeatureCommand { get; private set; }
        public ICommand CancelStartFeatureCommand { get; private set; }

        public ICommand StartBugfixDropDownCommand { get; private set; }
        public ICommand StartBugfixCommand { get; private set; }
        public ICommand CancelStartBugfixCommand { get; private set; }

        public ICommand StartReleaseDropDownCommand { get; private set; }
        public ICommand StartReleaseCommand { get; private set; }
        public ICommand CancelStartReleaseCommand { get; private set; }

        public ICommand StartSupportDropDownCommand { get; private set; }
        public ICommand StartSupportCommand { get; private set; }
        public ICommand CancelStartSupportCommand { get; private set; }

        public ICommand StartHotfixDropDownCommand { get; private set; }
        public ICommand StartHotfixCommand { get; private set; }
        public ICommand CancelStartHotfixCommand { get; private set; }

        public ICommand FinishFeatureDropDownCommand { get; private set; }
        public ICommand FinishFeatureCommand { get; private set; }
        public ICommand CancelFinishFeatureCommand { get; private set; }

        public ICommand PullRequestFeatureDropDownCommand { get; private set; }
        public ICommand PullRequestFeatureCommand { get; private set; }
        public ICommand CancelPullRequestFeatureCommand { get; private set; }

        public ICommand FinishBugfixDropDownCommand { get; private set; }
        public ICommand FinishBugfixCommand { get; private set; }
        public ICommand CancelFinishBugfixCommand { get; private set; }

        public ICommand PullRequestBugfixDropDownCommand { get; private set; }
        public ICommand PullRequestBugfixCommand { get; private set; }
        public ICommand CancelPullRequestBugfixCommand { get; private set; }

        public ICommand FinishReleaseDropDownCommand { get; private set; }
        public ICommand FinishReleaseCommand { get; private set; }
        public ICommand CancelFinishReleaseCommand { get; private set; }

        public ICommand FinishHotfixDropDownCommand { get; private set; }
        public ICommand FinishHotfixCommand { get; private set; }
        public ICommand CancelFinishHotfixCommand { get; private set; }

        public ICommand PullRequestHotfixDropDownCommand { get; private set; }
        public ICommand PullRequestHotfixCommand { get; private set; }
        public ICommand CancelPullRequestHotfixCommand { get; private set; }


        public ActionViewModel(GitFlowActionSection te)
            : base(te)
        {
            FeatureDeleteLocalBranch = true;
            FeatureDeleteRemoteBranch = true;
            FeatureSquash = false;
            FeatureNoFastForward = false;
            BugfixDeleteLocalBranch = true;
            BugfixDeleteRemoteBranch = true;
            BugfixSquash = false;
            BugfixNoFastForward = false;
            ReleaseDeleteBranch = true;
            ReleaseTagMessageSelected = true;
            ReleaseNoBackMerge = false;
            HotfixDeleteBranch = true;
            HotfixTagMessageSelected = true;

            ShowStartFeature = Visibility.Collapsed;
            ShowStartBugfix = Visibility.Collapsed;
            ShowStartRelease = Visibility.Collapsed;
            ShowStartSupport = Visibility.Collapsed;
            ShowStartHotfix = Visibility.Collapsed;
            
            ShowFinishFeature = Visibility.Collapsed;
            ShowFinishBugfix = Visibility.Collapsed;
            ShowFinishRelease = Visibility.Collapsed;
            ShowFinishHotfix = Visibility.Collapsed;

            ShowPullRequestFeature = Visibility.Collapsed;
            ShowPullRequestBugfix = Visibility.Collapsed;
            ShowPullRequestHotfix = Visibility.Collapsed;

            HideProgressBar();

            StartFeatureDropDownCommand = new DropDownLinkCommand(p => StartFeatureDropDown(), p => CanShowStartFeatureDropDown());
            StartFeatureCommand = new RelayCommand(p => StartFeature(), p => CanCreateFeature);
            CancelStartFeatureCommand = new RelayCommand(p => CancelStartFeature(), p => CanCancelFinishCommand());

            StartBugfixDropDownCommand = new DropDownLinkCommand(p => StartBugfixDropDown(), p => CanShowStartBugfixDropDown());
            StartBugfixCommand = new RelayCommand(p => StartBugfix(), p => CanCreateBugfix);
            CancelStartBugfixCommand = new RelayCommand(p => CancelStartBugfix(), p => CanCancelFinishCommand());

            StartReleaseDropDownCommand = new DropDownLinkCommand(p => StartReleaseDropDown(), p => CanShowStartReleaseDropDown());
            StartReleaseCommand = new RelayCommand(p => StartRelease(), p => CanCreateRelease);
            CancelStartReleaseCommand = new RelayCommand(p => CancelStartRelease(), p => CanCancelFinishCommand());

            StartSupportDropDownCommand = new DropDownLinkCommand(p => StartSupportDropDown(), p => CanShowStartSupportDropDown());
            StartSupportCommand = new RelayCommand(p => StartSupport(), p => CanCreateSupport);
            CancelStartSupportCommand = new RelayCommand(p => CancelStartSupport(), p => CanCancelFinishCommand());

            StartHotfixDropDownCommand = new DropDownLinkCommand(p => StartHotfixDropDown(), p => CanShowStartHotfixDropDown());
            StartHotfixCommand = new RelayCommand(p => StartHotfix(), p => CanCreateHotfix);
            CancelStartHotfixCommand = new RelayCommand(p => CancelStartHotfix(), p => CanCancelFinishCommand());

            FinishFeatureDropDownCommand = new DropDownLinkCommand(p => FinishFeatureDropDown(), p => CanShowFinishFeatureDropDown());
            FinishFeatureCommand = new RelayCommand(p => FinishFeature(), p => CanFinishFeature);
            CancelFinishFeatureCommand = new RelayCommand(p => CancelFinishFeature(), p => CanCancelFinishCommand());

            PullRequestFeatureDropDownCommand = new DropDownLinkCommand(p => PullRequestFeatureDropDown(), p => CanShowPullRequestFeatureDropDown());
            PullRequestFeatureCommand = new RelayCommand(p => PullRequestFeature(), p => CanFinishFeature);
            CancelPullRequestFeatureCommand = new RelayCommand(p => CancelPullRequestFeature(), p => CanCancelFinishCommand());

            FinishBugfixDropDownCommand = new DropDownLinkCommand(p => FinishBugfixDropDown(), p => CanShowFinishBugfixDropDown());
            FinishBugfixCommand = new RelayCommand(p => FinishBugfix(), p => CanFinishBugfix);
            CancelFinishBugfixCommand = new RelayCommand(p => CancelFinishBugfix(), p => CanCancelFinishCommand());

            PullRequestBugfixDropDownCommand = new DropDownLinkCommand(p => PullRequestBugfixDropDown(), p => CanShowPullRequestBugfixDropDown());
            PullRequestBugfixCommand = new RelayCommand(p => PullRequestBugfix(), p => CanFinishBugfix);
            CancelPullRequestBugfixCommand = new RelayCommand(p => CancelPullRequestBugfix(), p => CanCancelFinishCommand());

            FinishReleaseDropDownCommand = new DropDownLinkCommand(p => FinishReleaseDropDown(), p => CanShowFinishReleaseDropDown());
            FinishReleaseCommand = new RelayCommand(p => FinishRelease(), p => CanFinishRelease);
            CancelFinishReleaseCommand = new RelayCommand(p => CancelFinishRelease(), p => CanCancelFinishCommand());

            FinishHotfixDropDownCommand = new DropDownLinkCommand(p => FinishHotfixDropDown(), p => CanShowFinishHotfixDropDown());
            FinishHotfixCommand = new RelayCommand(p => FinishHotfix(), p => CanFinishHotfix);
            CancelFinishHotfixCommand = new RelayCommand(p => CancelFinishHotfix(), p => CanCancelFinishCommand());

            PullRequestHotfixDropDownCommand = new DropDownLinkCommand(p => PullRequestHotfixDropDown(), p => CanShowPullRequestHotfixDropDown());
            PullRequestHotfixCommand = new RelayCommand(p => PullRequestHotfix(), p => CanFinishHotfix);
            CancelPullRequestHotfixCommand = new RelayCommand(p => CancelPullRequestHotfix(), p => CanCancelFinishCommand());
        }

        public bool CanFinishRelease
        {
            get { return SelectedRelease != null && CanCancelFinishCommand(); }
        }

        public bool CanFinishHotfix
        {
            get { return SelectedHotfix != null && CanCancelFinishCommand(); }
        }

        public bool CanFinishFeature
        {
            get { return SelectedFeature != null && CanCancelFinishCommand(); }
        }

        public bool CanFinishBugfix
        {
            get { return SelectedBugfix != null && CanCancelFinishCommand(); }
        }

        private void CancelStartFeature()
        {
            ShowStartFeature = Visibility.Collapsed;
        }

        private void CancelStartBugfix()
        {
            ShowStartBugfix = Visibility.Collapsed;
        }

        private void CancelStartHotfix()
        {
            ShowStartHotfix = Visibility.Collapsed;
        }

        private void CancelStartRelease()
        {
            ShowStartRelease = Visibility.Collapsed;
        }

        private void CancelStartSupport()
        {
            ShowStartSupport = Visibility.Collapsed;
        }

        private void CancelFinishFeature()
        {
            ShowFinishFeature = Visibility.Collapsed;
        }

        private void CancelPullRequestFeature()
        {
            ShowPullRequestFeature = Visibility.Collapsed;
        }

        private void CancelFinishBugfix()
        {
            ShowFinishBugfix = Visibility.Collapsed;
        }

        private void CancelPullRequestBugfix()
        {
            ShowPullRequestBugfix = Visibility.Collapsed;
        }

        private void CancelFinishHotfix()
        {
            ShowFinishHotfix = Visibility.Collapsed;
        }

        private void CancelPullRequestHotfix()
        {
            ShowPullRequestHotfix = Visibility.Collapsed;
        }

        private void CancelFinishRelease()
        {
            ShowFinishRelease = Visibility.Collapsed;
        }

        private bool CanShowStartFeatureDropDown()
        {
            return true;
        }

        private bool CanShowStartBugfixDropDown()
        {
            return true;
        }

        private bool CanCancelFinishCommand()
        {
            return ProgressVisibility != Visibility.Visible;
        }

        private bool CanShowStartHotfixDropDown()
        {
            return true;
        }
        private bool CanShowStartReleaseDropDown()
        {
            return true;
        }

        private bool CanShowStartSupportDropDown()
        {
            return true;
        }

        private bool CanShowFinishFeatureDropDown()
        {
            return true;
        }

        private bool CanShowPullRequestFeatureDropDown()
        {
            return true;
        }

        private bool CanShowFinishBugfixDropDown()
        {
            return true;
        }

        private bool CanShowPullRequestBugfixDropDown()
        {
            return true;
        }

        private bool CanShowFinishHotfixDropDown()
        {
            return true;
        }

        private bool CanShowPullRequestHotfixDropDown()
        {
            return true;
        }

        private bool CanShowFinishReleaseDropDown()
        {
            return true;
        }

        private void StartFeatureDropDown()
        {
            HideAll();
            ShowStartFeature = Visibility.Visible;
        }

        private void StartBugfixDropDown()
        {
            HideAll();
            ShowStartBugfix = Visibility.Visible;
        }

        private void HideAll()
        {
            ShowStartFeature = Visibility.Collapsed;
            ShowStartBugfix = Visibility.Collapsed;
            ShowStartRelease = Visibility.Collapsed;
            ShowStartSupport = Visibility.Collapsed;
            ShowStartHotfix = Visibility.Collapsed;
            ShowFinishFeature = Visibility.Collapsed;
            ShowPullRequestFeature = Visibility.Collapsed;
            ShowFinishBugfix = Visibility.Collapsed;
            ShowPullRequestBugfix = Visibility.Collapsed;
            ShowFinishRelease = Visibility.Collapsed;
            ShowFinishHotfix = Visibility.Collapsed;
            ShowPullRequestHotfix = Visibility.Collapsed;
        }

        private void UpdateMenus()
        {
            OnPropertyChanged("StartFeatureVisible");
            OnPropertyChanged("StartBugfixVisible");
            OnPropertyChanged("StartReleaseVisible");
            OnPropertyChanged("StartSupportVisible");
            OnPropertyChanged("StartHotfixVisible");
            OnPropertyChanged("FinishFeatureVisible");
            OnPropertyChanged("PullRequestFeatureVisible");
            OnPropertyChanged("FinishBugfixVisible");
            OnPropertyChanged("PullRequestBugfixVisible");
            OnPropertyChanged("FinishReleaseVisible");
            OnPropertyChanged("FinishHotfixVisible");
            OnPropertyChanged("PullRequestHotfixVisible");

            OnPropertyChanged("OtherStartFeatureVisible");
            OnPropertyChanged("OtherStartBugfixVisible");
            OnPropertyChanged("OtherStartReleaseVisible");
            OnPropertyChanged("OtherStartSupportVisible");
            OnPropertyChanged("OtherStartHotfixVisible");
            OnPropertyChanged("OtherFinishFeatureVisible");
            OnPropertyChanged("OtherPullRequestFeatureVisible");
            OnPropertyChanged("OtherFinishBugfixVisible");
            OnPropertyChanged("OtherPullRequestBugfixVisible");
            OnPropertyChanged("OtherFinishReleaseVisible");
            OnPropertyChanged("OtherFinishHotfixVisible");
            OnPropertyChanged("OtherPullRequestHotfixVisible");
        }

        private void StartHotfixDropDown()
        {
            HideAll();
            ShowStartHotfix = Visibility.Visible;

        }
        private void StartReleaseDropDown()
        {
            HideAll();
            ShowStartRelease = Visibility.Visible;
        }

        private void StartSupportDropDown()
        {
            HideAll();
            ShowStartSupport = Visibility.Visible;
        }

        private void FinishFeatureDropDown()
        {
            HideAll();
            ShowFinishFeature = Visibility.Visible;
        }

        private void PullRequestFeatureDropDown()
        {
            HideAll();
            ShowPullRequestFeature = Visibility.Visible;
        }

        private void FinishBugfixDropDown()
        {
            HideAll();
            ShowFinishBugfix = Visibility.Visible;
        }

        private void PullRequestBugfixDropDown()
        {
            HideAll();
            ShowPullRequestBugfix = Visibility.Visible;
        }

        private void FinishHotfixDropDown()
        {
            HideAll();
            ShowFinishHotfix = Visibility.Visible;
        }

        private void PullRequestHotfixDropDown()
        {
            HideAll();
            ShowPullRequestHotfix = Visibility.Visible;
        }

        private void FinishReleaseDropDown()
        {
            HideAll();
            ShowFinishRelease = Visibility.Visible;
        }

        public bool CanCreateFeature
        {
            get { return !String.IsNullOrEmpty(FeatureName) && CanCancelFinishCommand(); }
        }

        public bool CanCreateBugfix
        {
            get { return !String.IsNullOrEmpty(BugfixName) && CanCancelFinishCommand(); }
        }

        public bool CanCreateRelease
        {
            get
            {
                return !String.IsNullOrEmpty(ReleaseName) && CanCancelFinishCommand();
            }
        }

        public bool CanCreateSupport
        {
            get
            {
                return !String.IsNullOrEmpty(SupportName) && !String.IsNullOrEmpty(supportBase) && CanCancelFinishCommand();
            }
        }

        public bool CanCreateHotfix
        {
            get { return !String.IsNullOrEmpty(HotfixName) && CanCancelFinishCommand(); }
        }

        public List<ListItem> AllFeatures
        {
            get
            {
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                return gf.AllFeatures.Select(x => new ListItem {Name = x}).ToList();
            }
        }

        public ListItem SelectedFeature
        {
            get { return selectedFeature; }
            set
            {
                if (Equals(value, selectedFeature)) return;
                selectedFeature = value;

                if (!string.IsNullOrWhiteSpace(selectedFeature?.Name))
                    FeaturePullRequestWorkItems = ParseWorkItem(selectedFeature.Name);

                OnPropertyChanged();
            }
        }

        public List<ListItem> AllBugfixes
        {
            get
            {
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                return gf.AllBugfixes.Select(x => new ListItem { Name = x }).ToList();
            }
        }

        public ListItem SelectedBugfix
        {
            get { return selectedBugfix; }
            set
            {
                if (Equals(value, selectedBugfix)) return;
                selectedBugfix = value;

                if (!string.IsNullOrWhiteSpace(selectedBugfix?.Name))
                    BugfixPullRequestWorkItems = ParseWorkItem(selectedBugfix.Name);

                OnPropertyChanged();
            }
        }

        private string ParseWorkItem(string branchName)
        {
            try
            {
                var branchNameSplit = branchName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (branchNameSplit.Length > 1)
                    branchName = branchNameSplit[1];

                var workItemId = branchName.Replace("#", string.Empty);
                var indexSep = workItemId.IndexOfAny(new char[] { '-', '_', '.' });

                if (indexSep > 0)
                    workItemId = workItemId.Substring(0, indexSep);

                return int.Parse(workItemId).ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception on parse WorkItem on branch {branchName}: {ex}");
                return string.Empty;
            }
        }

        public List<ListItem> AllReleases
        {
            get
            {
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                return gf.AllReleases.Select(x => new ListItem { Name = x }).ToList();
            }
        }

        public ListItem SelectedRelease
        {
            get { return selectedRelease; }
            set
            {
                if (Equals(value, selectedRelease)) return;
                selectedRelease = value;
                OnPropertyChanged();
            }
        }

        public List<ListItem> AllSupports
        {
            get
            {
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                return gf.AllSupports.Select(x => new ListItem { Name = x }).ToList();
            }
        }

        public ListItem SelectedSupport
        {
            get { return selectedSupport; }
            set
            {
                if (Equals(value, selectedSupport)) return;
                selectedSupport = value;
                OnPropertyChanged();
            }
        }

        public List<ListItem> AllHotfixes
        {
            get
            {
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                return gf.AllHotfixes.Select(x => new ListItem { Name = x }).ToList();
            }
        }


        public ListItem SelectedHotfix
        {
            get { return selectedHotfix; }
            set
            {
                if (Equals(value, selectedHotfix)) return;
                selectedHotfix = value;

                if (!string.IsNullOrWhiteSpace(selectedHotfix?.Name))
                    HotfixPullRequestWorkItems = ParseWorkItem(selectedHotfix.Name);

                OnPropertyChanged();
            }
        }

        private void StartFeature()
        {
            try
            { 
                if (String.IsNullOrEmpty(FeatureName))
                    return;

			    Logger.Event("StartFeature");
	            DateTime start = DateTime.Now;

                if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();
                    ShowProgressBar();
                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.StartFeature(FeatureName);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideProgressBar();
                    FeatureName = String.Empty;
                    UpdateMenus();
                    HideAll();
                    OnPropertyChanged("AllFeatures");
                    Te.Refresh();
                }
			    Logger.Metric("Duration-StartFeature", (DateTime.Now-start).Milliseconds);
            }
            catch (ArgumentException ex)
            {
                ShowErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }
        }

        private void StartBugfix()
        {
            try
            {
                if (String.IsNullOrEmpty(BugfixName))
                    return;

                Logger.Event("StartBugfix");
                DateTime start = DateTime.Now;

                if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();
                    ShowProgressBar();
                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.StartBugfix(BugfixName);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideProgressBar();
                    BugfixName = String.Empty;
                    UpdateMenus();
                    HideAll();
                    OnPropertyChanged("AllBugfixes");
                }
                Logger.Metric("Duration-StartBugfix", (DateTime.Now - start).Milliseconds);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }

        }

        private void StartRelease()
        {
            try
            { 
                if (String.IsNullOrEmpty(ReleaseName))
                    return;

			    Logger.Event("StartRelease");
	            DateTime start = DateTime.Now;

			    if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();
                    ShowProgressBar();
                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.StartRelease(ReleaseName);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }
                    HideProgressBar();
                    ShowStartRelease = Visibility.Collapsed;
                    ReleaseName = String.Empty;
                    UpdateMenus();
                    HideAll();
                    OnPropertyChanged("AllReleases");
                    Te.Refresh();
                }
			    Logger.Metric("Duration-StartRelease", (DateTime.Now - start).Milliseconds);
            }
            catch (ArgumentException ex)
            {
                ShowErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }

        }

        private void StartSupport()
        {
            try
            {
                if (String.IsNullOrEmpty(SupportName))
                    return;

                if (String.IsNullOrEmpty(SupportBase))
                    return;

                Logger.Event("StartSupport");
                DateTime start = DateTime.Now;

                if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();
                    ShowProgressBar();
                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.StartSupport(SupportName, SupportBase);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }
                    HideProgressBar();
                    ShowStartSupport = Visibility.Collapsed;
                    SupportName = String.Empty;
                    SupportBase = String.Empty;
                    UpdateMenus();
                    HideAll();
                    OnPropertyChanged("AllSupports");
                    Te.Refresh();
                }
                Logger.Metric("Duration-StartSupport", (DateTime.Now - start).Milliseconds);
            }
            catch (ArgumentException ex)
            {
                ShowErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }

        }

        private void ShowErrorMessage(GitFlowCommandResult result)
        {
            Te.ShowErrorNotification(result.CommandOutput);
        }
        private void ShowErrorMessage(string message)
        {
            Te.ShowErrorNotification(message);
        }
        private void ShowInfoMessage(string message)
        {
            Te.ShowInfoNotification(message);
        }
        private void StartHotfix()
        {
            try
            { 
                if (String.IsNullOrEmpty(HotfixName))
                    return;

			    Logger.Event("StartHotfix");
	            DateTime start = DateTime.Now;

			    if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();
                    ShowProgressBar();

                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.StartHotfix(HotfixName);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideProgressBar();
                    ShowStartHotfix = Visibility.Collapsed;
                    HotfixName = String.Empty;
                    UpdateMenus();
                    HideAll();
                    OnPropertyChanged("AllHotfixes");
                    Te.Refresh();
                }
			    Logger.Metric("Duration-StartHotfix", (DateTime.Now - start).Milliseconds);
            }
            catch(ArgumentException ex)
            {
                ShowErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }

        }

        private void FinishFeature()
        {
            try
            {
                DateTime start = DateTime.Now;

                var properties = new Dictionary<string, string>
                {
                    {"RebaseOnDevelopmentBranch", FeatureRebaseOnDevelopmentBranch.ToString()},
                    {"DeleteLocalBranch", FeatureDeleteLocalBranch.ToString()},
                    {"DeleteRemoteBranch", FeatureDeleteRemoteBranch.ToString()},
                    {"Squash", FeatureSquash.ToString()},
                    {"NoFastForward", FeatureNoFastForward.ToString()}
                };
                Logger.Event("FinishFeature", properties);

                if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();

                    ShowProgressBar();

                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    if( FeatureSquash)
                    {
                        ShowInfoMessage("Waiting for your editor to close the file...");
                    }
                    var result = gf.FinishFeature(SelectedFeature.Name, FeatureRebaseOnDevelopmentBranch, FeatureDeleteLocalBranch, FeatureDeleteRemoteBranch, FeatureSquash, FeatureNoFastForward);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideProgressBar();
                    ShowFinishFeature = Visibility.Collapsed;
                    UpdateMenus();
                    HideAll();
                    OnPropertyChanged("AllFeatures");
                    Te.Refresh();
                }

                Logger.Metric("Duration-FinishFeature", (DateTime.Now - start).Milliseconds);
            }
            catch( Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }
		}

        private void PullRequestFeature()
        {
            try
            {
                DateTime start = DateTime.Now;

                var properties = new Dictionary<string, string>
                {
                    {"PullRequestWorkItems", (!String.IsNullOrEmpty(FeaturePullRequestWorkItems)).ToString()},
                    {"DeleteLocalBranch", FeatureDeleteLocalBranch.ToString()}
                };
                Logger.Event("PullRequestFeature", properties);

                if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();

                    ShowProgressBar();

                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    
                    var result = gf.PullRequestFeature(SelectedFeature.Name, FeaturePullRequestWorkItems, FeatureDeleteLocalBranch);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideProgressBar();
                    ShowPullRequestFeature = Visibility.Collapsed;
                    UpdateMenus();
                    HideAll();
                    OnPropertyChanged("AllFeatures");
                    Te.Refresh();
                }

                Logger.Metric("Duration-PullRequestFeature", (DateTime.Now - start).Milliseconds);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }
        }

        private void FinishBugfix()
        {
            try
            {
                DateTime start = DateTime.Now;

                var properties = new Dictionary<string, string>
                {
                    {"RebaseOnDevelopmentBranch", BugfixRebaseOnDevelopmentBranch.ToString()},
                    {"DeleteLocalBranch", BugfixDeleteLocalBranch.ToString()},
                    {"DeleteRemoteBranch", BugfixDeleteRemoteBranch.ToString()},
                    {"Squash", BugfixSquash.ToString()},
                    {"NoFastForward", BugfixNoFastForward.ToString()}
                };
                Logger.Event("FinishBugfix", properties);

                if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();

                    ShowProgressBar();

                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.FinishBugfix(SelectedBugfix.Name, BugfixRebaseOnDevelopmentBranch, BugfixDeleteLocalBranch, BugfixDeleteRemoteBranch, BugfixSquash, BugfixNoFastForward);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideProgressBar();
                    ShowFinishBugfix = Visibility.Collapsed;
                    UpdateMenus();
                    HideAll();
                    OnPropertyChanged("AllBugfixes");
                    Te.Refresh();
                }

                Logger.Metric("Duration-FinishBugfix", (DateTime.Now - start).Milliseconds);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }
        }

        private void PullRequestBugfix()
        {
            try
            {
                DateTime start = DateTime.Now;

                var properties = new Dictionary<string, string>
                {
                    {"PullRequestWorkItems", (!String.IsNullOrEmpty(BugfixPullRequestWorkItems)).ToString()},
                    {"DeleteLocalBranch", BugfixDeleteLocalBranch.ToString()}
                };
                Logger.Event("PullRequestBugfix", properties);

                if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();

                    ShowProgressBar();

                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.PullRequestBugfix(SelectedBugfix.Name, BugfixPullRequestWorkItems, BugfixDeleteLocalBranch);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideProgressBar();
                    ShowFinishBugfix = Visibility.Collapsed;
                    UpdateMenus();
                    HideAll();
                    OnPropertyChanged("AllBugfixes");
                    Te.Refresh();
                }

                Logger.Metric("Duration-PullRequestBugfix", (DateTime.Now - start).Milliseconds);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }
        }

        private void FinishRelease()
        {
            try
            { 
			    DateTime start = DateTime.Now;
			    var properties = new Dictionary<string, string>
			    {
				    {"TaggedRelease", (!String.IsNullOrEmpty(ReleaseTagMessage)).ToString()},
				    {"DeleteBranch", ReleaseDeleteBranch.ToString()},
				    {"ForceDeletion", ReleaseForceDeletion.ToString()},
				    {"PushChanges", ReleasePushChanges.ToString()},
					{"NoBackMerge", ReleaseNoBackMerge.ToString()}
				};
			    Logger.Event("FinishRelease", properties);

			    if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();
                    ShowProgressBar();

                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.FinishRelease(SelectedRelease.Name, ReleaseTagMessage, ReleaseDeleteBranch, ReleaseForceDeletion, ReleasePushChanges, ReleaseNoBackMerge);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideAll();
                    HideProgressBar();
                    ShowFinishRelease = Visibility.Collapsed;
                    OnPropertyChanged("AllReleases");
                    UpdateMenus();
                    Te.Refresh();
                }
			    Logger.Metric("Duration-FinishRelease", (DateTime.Now - start).Milliseconds);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }
        }

        private void FinishHotfix()
        {
            try
            { 
			    DateTime start = DateTime.Now;
			    var properties = new Dictionary<string, string>
			    {
				    {"TaggedRelease", (!String.IsNullOrEmpty(HotfixTagMessage)).ToString()},
				    {"DeleteBranch", HotfixDeleteBranch.ToString()},
				    {"ForceDeletion", HotfixForceDeletion.ToString()},
				    {"PushChanges", HotfixPushChanges.ToString()}
			    };
			    Logger.Event("FinishHotfix", properties);

			    if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();
                    ShowProgressBar();

                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.FinishHotfix(SelectedHotfix.Name, HotfixTagMessage, HotfixDeleteBranch, HotfixForceDeletion, HotfixPushChanges);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideAll();
                    HideProgressBar();
                    ShowFinishHotfix = Visibility.Collapsed;
                    OnPropertyChanged("AllHotfixes");
                    UpdateMenus();
                    Te.Refresh();
                }
			    Logger.Metric("Duration-FinishHotfix", (DateTime.Now - start).Milliseconds);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }

        }

        private void PullRequestHotfix()
        {
            try
            {
                DateTime start = DateTime.Now;
                var properties = new Dictionary<string, string>
                {
                    {"PullRequestWorkItems", (!String.IsNullOrEmpty(HotfixPullRequestWorkItems)).ToString()},
                    {"DeleteBranch", HotfixDeleteBranch.ToString()}
                };
                Logger.Event("PullRequesthHotfix", properties);

                if (GitFlowPage.ActiveRepo != null)
                {
                    GitFlowPage.ActiveOutputWindow();
                    ShowProgressBar();

                    var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                    var result = gf.PullRequestHotfix(SelectedHotfix.Name, HotfixPullRequestWorkItems, HotfixDeleteBranch);
                    if (!result.Success)
                    {
                        ShowErrorMessage(result);
                    }

                    HideAll();
                    HideProgressBar();
                    ShowFinishHotfix = Visibility.Collapsed;
                    OnPropertyChanged("AllHotfixes");
                    UpdateMenus();
                    Te.Refresh();
                }
                Logger.Metric("Duration-PullRequesthHotfix", (DateTime.Now - start).Milliseconds);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.ToString());
                Logger.Exception(ex);
            }
        }

        public string ReleaseName
        {
            get { return releaseName; }
            set
            {
                if (value == releaseName) return;
                releaseName = value;
                OnPropertyChanged();
                OnPropertyChanged("CanCreateRelease");
            }
        }

        public string SupportName
        {
            get { return supportName; }
            set
            {
                if (value == supportName) return;
                supportName = value;
                OnPropertyChanged();
                OnPropertyChanged("CanCreateSupport");
            }
        }

        public string SupportBase
        {
            get { return supportBase; }
            set
            {
                if (value == supportBase) return;
                supportBase = value;
                OnPropertyChanged();
                OnPropertyChanged("CanCreateSupport");
            }
        }

        public string HotfixName
        {
            get { return hotfixName; }
            set
            {
                if (value == hotfixName) return;
                hotfixName = value;
                OnPropertyChanged();
                OnPropertyChanged("CanCreateHotfix");
            }
        }

        public string FeatureName
        {
            get { return featureName; }
            set
            {
                if (value == featureName) return;
                featureName = value;
                OnPropertyChanged();
                OnPropertyChanged("CanCreateFeature");
            }
        }

        public string BugfixName
        {
            get { return bugfixName; }
            set
            {
                if (value == bugfixName) return;
                bugfixName = value;
                OnPropertyChanged();
                OnPropertyChanged("CanCreateBugfix");
            }
        }

        public Visibility ShowStartFeature
        {
            get { return showStartFeature; }
            set
            {
                if (value == showStartFeature) return;
                showStartFeature = value;
                OnPropertyChanged();
            }
        }

        public Visibility ShowStartBugfix
        {
            get { return showStartBugfix; }
            set
            {
                if (value == showStartBugfix) return;
                showStartBugfix = value;
                OnPropertyChanged();
            }
        }

        public Visibility ShowStartHotfix
        {
            get { return showStartHotfix; }
            set
            {
                if (value == showStartHotfix) return;
                showStartHotfix = value;
                OnPropertyChanged();
            }
        }

        public Visibility ShowStartRelease
        {
            get { return showStartRelease; }
            set
            {
                if (value == showStartRelease) return;
                showStartRelease = value;
                OnPropertyChanged();
            }
        }

        public Visibility ShowStartSupport
        {
            get { return showStartSupport; }
            set
            {
                if (value == showStartSupport) return;
                showStartSupport = value;
                OnPropertyChanged();
            }
        }

        public Visibility ShowFinishFeature
        {
            get { return showFinishFeature; }
            set
            {
                if (value == showFinishFeature) return;
                showFinishFeature = value;
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                if (gf.IsOnFeatureBranch)
                {
                    SelectedFeature = AllFeatures.First(f => f.Name == gf.CurrentBranchLeafName);
                }

                OnPropertyChanged();
            }
        }

        public Visibility ShowPullRequestFeature
        {
            get { return showPullRequestFeature; }
            set
            {
                if (value == showPullRequestFeature) return;
                showPullRequestFeature = value;
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                if (gf.IsOnFeatureBranch)
                {
                    SelectedFeature = AllFeatures.First(f => f.Name == gf.CurrentBranchLeafName);
                }

                OnPropertyChanged();
            }
        }

        public Visibility ShowFinishBugfix
        {
            get { return showFinishBugfix; }
            set
            {
                if (value == showFinishBugfix) return;
                showFinishBugfix = value;
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                if (gf.IsOnBugfixBranch)
                {
                    SelectedBugfix = AllBugfixes.First(f => f.Name == gf.CurrentBranchLeafName);
                }

                OnPropertyChanged();
            }
        }

        public Visibility ShowPullRequestBugfix
        {
            get { return showPullRequestBugfix; }
            set
            {
                if (value == showPullRequestBugfix) return;
                showPullRequestBugfix = value;
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                if (gf.IsOnBugfixBranch)
                {
                    SelectedBugfix = AllBugfixes.First(f => f.Name == gf.CurrentBranchLeafName);
                }
                OnPropertyChanged();
            }
        }

        public Visibility ShowFinishHotfix
        {
            get { return showFinishHotfix; }
            set
            {
                if (value == showFinishHotfix) return;
                showFinishHotfix = value;
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                if (gf.IsOnHotfixBranch)
                {
                    SelectedHotfix = AllHotfixes.First(f => f.Name == gf.CurrentBranchLeafName);
                }
                OnPropertyChanged();
            }
        }

        public Visibility ShowPullRequestHotfix
        {
            get { return showPullRequestHotfix; }
            set
            {
                if (value == showPullRequestHotfix) return;
                showPullRequestHotfix = value;
                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                if (gf.IsOnHotfixBranch)
                {
                    SelectedHotfix = AllHotfixes.First(f => f.Name == gf.CurrentBranchLeafName);
                }
                OnPropertyChanged();
            }
        }

        public Visibility ShowFinishRelease
        {
            get { return showFinishRelease; }
            set
            {
                if (value == showFinishRelease) return;
                showFinishRelease = value;

                var gf = new GitFlowWrapper(GitFlowPage.ActiveRepoPath);
                if (gf.IsOnReleaseBranch)
                {
                    SelectedRelease = AllReleases.First(f => f.Name == gf.CurrentBranchLeafName);
                }
                OnPropertyChanged();
            }
        }

        #region Feature

        public bool FeatureRebaseOnDevelopmentBranch
        {
            get { return featureRebaseOnDevelopmentBranch; }
            set
            {
                if (value.Equals(featureRebaseOnDevelopmentBranch)) return;
                featureRebaseOnDevelopmentBranch = value;
                OnPropertyChanged();
            }
        }

        public bool FeatureDeleteLocalBranch
        {
            get { return featureDeleteLocalBranch; }
            set
            {
                if (value.Equals(featureDeleteLocalBranch)) return;
                featureDeleteLocalBranch = value;
                OnPropertyChanged();
            }
        }
        public bool FeatureDeleteRemoteBranch
        {
            get { return featureDeleteRemoteBranch; }
            set
            {
                if (value.Equals(featureDeleteRemoteBranch)) return;
                featureDeleteRemoteBranch = value;
                OnPropertyChanged();
            }
        }

        public bool FeatureSquash
        {
            get { return featureSquash; }
            set
            {
                if (value.Equals(featureSquash)) return;
                featureSquash = value;
                OnPropertyChanged();
            }
        }

        public bool FeatureNoFastForward
        {
            get { return featureNoFastForward; }
            set
            {
                if (value.Equals(featureNoFastForward)) return;
                featureNoFastForward = value;
                OnPropertyChanged();
            }
        }

        public string FeaturePullRequestWorkItems
        {
            get { return featurePullRequestWorkItems; }
            set
            {
                if (value == featurePullRequestWorkItems) return;
                featurePullRequestWorkItems = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Bugfix

        public bool BugfixRebaseOnDevelopmentBranch
        {
            get { return bugfixRebaseOnDevelopmentBranch; }
            set
            {
                if (value.Equals(bugfixRebaseOnDevelopmentBranch)) return;
                bugfixRebaseOnDevelopmentBranch = value;
                OnPropertyChanged();
            }
        }

        public bool BugfixDeleteLocalBranch
        {
            get { return bugfixDeleteLocalBranch; }
            set
            {
                if (value.Equals(bugfixDeleteLocalBranch)) return;
                bugfixDeleteLocalBranch = value;
                OnPropertyChanged();
            }
        }
        public bool BugfixDeleteRemoteBranch
        {
            get { return bugfixDeleteRemoteBranch; }
            set
            {
                if (value.Equals(bugfixDeleteRemoteBranch)) return;
                bugfixDeleteRemoteBranch = value;
                OnPropertyChanged();
            }
        }

        public bool BugfixSquash
        {
            get { return bugfixSquash; }
            set
            {
                if (value.Equals(bugfixSquash)) return;
                bugfixSquash = value;
                OnPropertyChanged();
            }
        }

        public bool BugfixNoFastForward
        {
            get { return bugfixNoFastForward; }
            set
            {
                if (value.Equals(bugfixNoFastForward)) return;
                bugfixNoFastForward = value;
                OnPropertyChanged();
            }
        }

        public string BugfixPullRequestWorkItems
        {
            get { return bugfixPullRequestWorkItems; }
            set
            {
                if (value == bugfixPullRequestWorkItems) return;
                bugfixPullRequestWorkItems = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Release

        public bool HotfixTagMessageSelected
        {
            get { return hotfixTagMessageSelected; }
            set
            {
                if (value.Equals(hotfixTagMessageSelected)) return;
                hotfixTagMessageSelected = value;
                OnPropertyChanged();
            }
        }

        public bool ReleaseTagMessageSelected
        {
            get { return releaseTagMessageSelected; }
            set
            {
                if (value.Equals(releaseTagMessageSelected)) return;
                releaseTagMessageSelected = value;
                OnPropertyChanged();
            }
        }

        public string ReleaseTagMessage
        {
            get { return releaseTagMessage; }
            set
            {
                if (value == releaseTagMessage) return;
                releaseTagMessage = value;
                OnPropertyChanged();
            }
        }

        public bool ReleaseDeleteBranch
        {
            get { return releaseDeleteBranch; }
            set
            {
                if (value.Equals(releaseDeleteBranch)) return;
                releaseDeleteBranch = value;
                OnPropertyChanged();
            }
        }

        public bool ReleasePushChanges
        {
            get { return releasePushChanges; }
            set
            {
                if (value.Equals(releasePushChanges)) return;
                releasePushChanges = value;
                OnPropertyChanged();
            }
        }


		public bool ReleaseNoBackMerge
		{
			get { return releaseNoBackMerge; }
			set
			{
				if (value.Equals(releaseNoBackMerge)) return;
				releaseNoBackMerge = value;
				OnPropertyChanged();
			}
		}

		public bool ReleaseForceDeletion
        {
            get { return releaseForceDeletion; }
            set
            {
                if (value.Equals(releaseForceDeletion)) return;
                releaseForceDeletion = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Hotfix

        public string HotfixTagMessage
        {
            get { return hotfixTagMessage; }
            set
            {
                if (value == hotfixTagMessage) return;
                hotfixTagMessage = value;
                OnPropertyChanged();
            }
        }

        public bool HotfixForceDeletion
        {
            get { return hotfixForceDeletion; }
            set
            {
                if (value.Equals(hotfixForceDeletion)) return;
                hotfixForceDeletion = value;
                OnPropertyChanged();
            }
        }

        public bool HotfixPushChanges
        {
            get { return hotfixPushChanges; }
            set
            {
                if (value.Equals(hotfixPushChanges)) return;
                hotfixPushChanges = value;
                OnPropertyChanged();
            }
        }

        public bool HotfixDeleteBranch
        {
            get { return hotfixDeleteBranch; }
            set
            {
                if (value.Equals(hotfixDeleteBranch)) return;
                hotfixDeleteBranch = value;
                OnPropertyChanged();
            }
        }

        public string HotfixPullRequestWorkItems
        {
            get { return hotfixPullRequestWorkItems; }
            set
            {
                if (value == hotfixPullRequestWorkItems) return;
                hotfixPullRequestWorkItems = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Visibility

        //TODO: Refactor this code and introduce BooleanToVisibility converter...
        public Visibility StartFeatureVisible
        {
            get { return OnMainBranch(); }
        }

        public Visibility OtherStartFeatureVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnDevelopBranch || gf.IsOnMasterBranch ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility StartBugfixVisible
        {
            get
			{
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnDevelopBranch || gf.IsOnMasterBranch ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility OtherStartBugfixVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnDevelopBranch || gf.IsOnMasterBranch ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility StartReleaseVisible
        {
            get
            {
                return OnMainBranch();
            }
        }

        public Visibility OtherStartReleaseVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnDevelopBranch || gf.IsOnMasterBranch ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility StartSupportVisible
        {
            get
            {
                return OnMainBranch();
            }
        }

        public Visibility OtherStartSupportVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnDevelopBranch || gf.IsOnMasterBranch ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility StartHotfixVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnDevelopBranch || gf.IsOnMasterBranch || gf.IsOnSupportBranch ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility OtherStartHotfixVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnDevelopBranch || gf.IsOnMasterBranch || gf.IsOnSupportBranch ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility FinishHotfixVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnHotfixBranch ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility OtherFinishHotfixVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnHotfixBranch ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility PullRequestHotfixVisible
        {
            get
            {
                return FinishHotfixVisible;
            }
        }

        public Visibility OtherPullRequestHotfixVisible
        {
            get
            {
                return OtherFinishHotfixVisible;
            }
        }

        public Visibility FinishFeatureVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnFeatureBranch ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility OtherFinishFeatureVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnFeatureBranch ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility PullRequestFeatureVisible
        {
            get
            {
                return FinishFeatureVisible;
            }
        }

        public Visibility OtherPullRequestFeatureVisible
        {
            get
            {
                return OtherFinishFeatureVisible;
            }
        }

        public Visibility FinishBugfixVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnBugfixBranch ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility OtherFinishBugfixVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnBugfixBranch ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility PullRequestBugfixVisible
        {
            get
            {
                return FinishBugfixVisible;
            }
        }

        public Visibility OtherPullRequestBugfixVisible
        {
            get
            {
                return OtherFinishBugfixVisible;
            }
        }

        public Visibility FinishReleaseVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnReleaseBranch ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility OtherFinishReleaseVisible
        {
            get
            {
                var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
                return gf.IsOnReleaseBranch ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private static Visibility OnMainBranch()
        {
            var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepoPath, GitFlowPage.OutputWindow);
            return gf.IsOnDevelopBranch || gf.IsOnMasterBranch ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion

        public void Update()
        {
            UpdateMenus();
        }
    }
}
