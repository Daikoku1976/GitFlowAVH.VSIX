using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GitFlowAVH.ViewModels;

namespace GitFlowAVH.UI
{
    /// <summary>
    /// Interaction logic for FeaturesUI.xaml
    /// </summary>
    public partial class SupportsUI : UserControl
    {
        public SupportsUI(SupportsViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

    }
}
