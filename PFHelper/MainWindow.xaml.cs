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
        #region Properties
        
        public ObservableCollection<CombatGridItem> _combatGridItems;
        private List<CombatEffectItem> combatEffects;
        
        private Random random;
        private FantasyDate date;

        #endregion

        #region Constructor

        public MainWindow()
        {
            combatEffects = new List<CombatEffectItem>();
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

            LbxD20.DisplayMemberPath = "MathString";
            LbxD20.SelectedValuePath = "Result";

            DgCombatGrid.ItemsSource = _combatGridItems;
            random = new Random();
            date = new FantasyDate();
        }

        #endregion

        #region Methods

        private List<DiceRollResult> DiceRoll(int d, int num, int add, bool addPos)
        {
            var ret = new List<DiceRollResult>();
            string op = addPos ? "+" : "-";

            for(int i = 0; i < num; i++)
            {
                var roll = random.Next(d + 1);
                var result = addPos ? (roll + add) : (roll - add);
                var itm = new DiceRollResult
                {
                    MathString = $"{roll} {op} {add} = {result}",
                    Result = result
                };

                ret.Add(itm);
            }

            return ret;
        }

        #endregion

        #region Events

        private void BtnRollD20_Click(object sender, RoutedEventArgs e)
        {
            var diceList = DiceRoll(20, IntNumD20.Value ?? 0, IntAddD20.Value ?? 0, RadPlusD20.IsChecked ?? false);
            LbxD20.ItemsSource = diceList;

            LblAvgD20.Content = diceList.Average(x => x.Result);
        }

        #endregion

        private void BtnRollD4_Click(object sender, RoutedEventArgs e)
        {
            var diceList = DiceRoll(4, IntNumD4.Value ?? 0, IntAddD4.Value ?? 0, RadPlusD4.IsChecked ?? false);
            LbxD4.ItemsSource = diceList;

            LblTotalD4.Content = diceList.Sum(x => x.Result);
        }

        private void BtnRollD6_Click(object sender, RoutedEventArgs e)
        {
            var diceList = DiceRoll(6, IntNumD6.Value ?? 0, IntAddD6.Value ?? 0, RadPlusD6.IsChecked ?? false);
            LbxD6.ItemsSource = diceList;

            LblTotalD6.Content = diceList.Sum(x => x.Result);
        }

        private void BtnRollD8_Click(object sender, RoutedEventArgs e)
        {
            var diceList = DiceRoll(8, IntNumD8.Value ?? 0, IntAddD8.Value ?? 0, RadPlusD8.IsChecked ?? false);
            LbxD8.ItemsSource = diceList;

            LblTotalD8.Content = diceList.Sum(x => x.Result);
        }

        private void BtnRollD10_Click(object sender, RoutedEventArgs e)
        {
            var diceList = DiceRoll(10, IntNumD10.Value ?? 0, IntAddD10.Value ?? 0, RadPlusD10.IsChecked ?? false);
            LbxD10.ItemsSource = diceList;

            LblTotalD10.Content = diceList.Sum(x => x.Result);
        }

        private void BtnRollD12_Click(object sender, RoutedEventArgs e)
        {
            var diceList = DiceRoll(12, IntNumD12.Value ?? 0, IntAddD12.Value ?? 0, RadPlusD12.IsChecked ?? false);
            LbxD12.ItemsSource = diceList;

            LblTotalD12.Content = diceList.Sum(x => x.Result);
        }

        private void BtnRollD100_Click(object sender, RoutedEventArgs e)
        {
            LblTotalD100.Content = random.Next(101);
        }
    }
}
