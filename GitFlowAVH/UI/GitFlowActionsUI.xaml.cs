using System.Windows.Controls;
using GitFlowAVH.ViewModels;

namespace GitFlowAVH.UI
{
    public partial class GitFlowActionsUI : UserControl
    {
        private ActionViewModel model;
        public GitFlowActionsUI(ActionViewModel model)
        {
            this.model = model;
            InitializeComponent();
            DataContext = model;
		}
    }
}
