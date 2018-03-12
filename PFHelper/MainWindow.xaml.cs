using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PFHelper.Classes;

namespace PFHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<CombatGridItem> combatGridItems;
        private List<CombatEffect> combatEffects;

        public MainWindow()
        {
            combatEffects = new List<CombatEffect>();
            combatGridItems = new ObservableCollection<CombatGridItem>();

            InitializeComponent();
        }
    }
}
