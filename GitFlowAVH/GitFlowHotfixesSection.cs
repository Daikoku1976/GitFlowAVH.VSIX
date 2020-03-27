using GitFlowAVH.UI;
using GitFlowAVH.ViewModels;
using Microsoft.TeamFoundation.Controls;
using GitFlowAVH.TeamExplorer;

namespace GitFlowAVH
{
    [TeamExplorerSection(GuidList.GitFlowHotfixesSection, GuidList.GitFlowPage, 115)]
    public class GitFlowHotfixesSection : TeamExplorerBaseSection, IGitFlowSection
    {
        private readonly HotfixesViewModel model;

        public GitFlowHotfixesSection()
        {
            Title = "Current Hotfixes";
            IsVisible = false;
            model = new HotfixesViewModel(this);
            UpdateVisibleState();
        }

        public override void Refresh()
        {
            var service = GetService<ITeamExplorerPage>();
            service.Refresh();
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
                    SectionContent = new HotfixesUI(model);
                    IsVisible = true;
                }
                model.Update();
            }
            else
            {
                IsVisible = false;
            }
        }

        public void ShowErrorNotification(string message)
        {
            ShowNotification(message, NotificationType.Error);
        }
    }
}