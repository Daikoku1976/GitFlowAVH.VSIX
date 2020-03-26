using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TeamFoundation.Git.Extensibility;
using GitFlowAVH.TeamExplorer;
using GitFlowAVH;

namespace GitFlowAVH
{
    [TeamExplorerNavigationItem("7C3F6592-4084-4BBF-B43F-CBEA693EBBE2", 1500, TargetPageId = "1F84BD1D-34B7-4745-BF5E-146299305344")]
    public class GitFlowNavigationItem : TeamExplorerBaseNavigationItem
    {
        private readonly ITeamExplorer teamExplorer;
        private readonly IGitExt gitService;

        [ImportingConstructor]
        public GitFlowNavigationItem([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            try
            {
                UpdateVisible();
                Text = "GitFlow AVH";
                Image = Resources.LinkIcon;
                IsVisible = true;
                teamExplorer = GetService<ITeamExplorer>();
                gitService = (IGitExt)serviceProvider.GetService(typeof(IGitExt));
                teamExplorer.PropertyChanged += TeamExplorerOnPropertyChanged;
            }
            catch (Exception ex)
            {
	            HandleException(ex);
            }
        }

	    private void HandleException(Exception ex)
	    {
		    Logger.Exception(ex);
		    ShowNotification(ex.Message, NotificationType.Error);
	    }

	    protected override void ContextChanged(object sender, ContextChangedEventArgs e)
        {
            UpdateVisible();
            base.ContextChanged(sender, e);
        }

        private void TeamExplorerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateVisible();
        }

        private void UpdateVisible()
        {
            IsVisible = false;
            if (gitService != null)
            {
                IsVisible = true;
            }
        }

        public override void Execute()
        {
	        try
	        {
				Logger.PageView("Navigate");
	        }
	        catch (Exception ex)
	        {
		       Logger.Exception(ex);
		       ShowNotification(ex.Message, NotificationType.Error);
	        }
            teamExplorer.NavigateToPage(new Guid(GuidList.GitFlowPage), null);
        }
    }
}
