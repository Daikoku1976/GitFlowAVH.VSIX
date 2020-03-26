using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GitFlowAVH.ViewModels;

namespace GitFlowAVH.UI
{
    /// <summary>
    /// Interaction logic for HotfixesUI.xaml
    /// </summary>
    public partial class HotfixesUI : UserControl
    {
        public HotfixesUI(HotfixesViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}