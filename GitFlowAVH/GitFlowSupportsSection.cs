using GitFlowAVH.UI;
using GitFlowAVH.ViewModels;
using Microsoft.TeamFoundation.Controls;
using GitFlowAVH.TeamExplorer;

namespace GitFlowAVH
{
    [TeamExplorerSection(GuidList.GitFlowSupportsSection, GuidList.GitFlowPage, 125)]
    public class GitFlowSupportsSection : TeamExplorerBaseSection, IGitFlowSection
    {
        private readonly SupportsViewModel model;

        public GitFlowSupportsSection()
        {
            Title = "Current Supports";
            IsVisible = false;
            model = new SupportsViewModel(this);
            UpdateVisibleState();
        }

        public void UpdateVisibleState()
        {
            if (!GitFlowPage.GitFlowIsInstalled || GitFlowPage.ActiveRepo == null)
            {
                IsVisible = false;
                return;
            }

            var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepo.RepositoryPath, GitFlowPage.OutputWindow);
            if (gf.IsInitialized)
            {
                if (!IsVisible)
                {
                    SectionContent = new SupportsUI(model);
                    IsVisible = true;
                }
                model.Update();
            }
            else
            {
                IsVisible = false;
            }
        }

    }
}