using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        public ObservableCollection<CombatGridItem> _combatGridItems;
        private List<CombatEffect> combatEffects;

        public MainWindow()
        {
            combatEffects = new List<CombatEffect>();
            _combatGridItems = new ObservableCollection<CombatGridItem>
            {
                new CombatGridItem()
                {
                    AC = 14,
                    ACFlat = 13,
                    ACTouch = 11,
                    Fort = 2,
                    HP = 10,
                    Init = 11,
                    Name = "Test PC",
                    Note = "Test",
                    PC = true,
                    Ref = 1,
                    Subd = 0,
                    Will = 2
                }
            };

            InitializeComponent();

            CombatGrid.ItemsSource = _combatGridItems;
        }
    }
}
