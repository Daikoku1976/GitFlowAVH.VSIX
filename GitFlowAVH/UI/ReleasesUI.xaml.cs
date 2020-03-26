using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GitFlowAVH.ViewModels;

namespace GitFlowAVH.UI
{
    /// <summary>
    /// Interaction logic for FeaturesUI.xaml
    /// </summary>
    public partial class ReleasesUI : UserControl
    {
        public ReleasesUI(ReleasesViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

    }
}
