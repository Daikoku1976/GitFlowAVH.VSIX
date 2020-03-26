using System.Windows.Controls;
using GitFlowAVH.ViewModels;

namespace GitFlowAVH.UI
{
    public partial class InitUi : UserControl
    {
        private readonly InitModel model;

        public InitUi(InitModel model)
        {
			Logger.PageView("Init");

			this.model = model;
            InitializeComponent();
            DataContext = model;
        }

    }
}
