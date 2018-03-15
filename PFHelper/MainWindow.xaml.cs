using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;
using PFHelper.Classes;

namespace PFHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Interface Properties

        public bool EncounterGroup
        {
            get { return CbxEncounterGroup.IsChecked == true; }
            set { CbxEncounterGroup.IsChecked = value; }
        }
        
        public bool EncounterNPC
        {
            get { return CbxEncounterNPC.IsChecked == true; }
            set { CbxEncounterNPC.IsChecked = value; }
        }

        public bool EncounterTime
        {
            get { return CbxEncounterTime.IsChecked == true; }
            set { CbxEncounterTime.IsChecked = value; }
        }

        public bool EncounterZone
        {
            get { return CbxEncounterZone.IsChecked == true; }
            set { CbxEncounterZone.IsChecked = value; }
        }

        public bool RationsInfinite
        {
            get { return CbxRationsInfinite.IsChecked == true; }
            set { CbxRationsInfinite.IsChecked = value; }
        }

        public int ContinentId
        {
            get
            {
                if (LbxContinent.SelectedItem != null)
                    return (int)LbxContinent.SelectedValue;
                return 0;
            }
            set
            {
                LbxContinent.SelectedValue = value; 
            }
        }

        public int TimeId
        {
            get
            {
                if (LbxTime.SelectedItem != null)
                    return (int)LbxTime.SelectedValue;
                return 0;
            }
            set
            {
                LbxTime.SelectedValue = value;
            }
        }

        public int PlaneId
        {
            get
            {
                if (LbxPlane.SelectedItem != null)
                    return (int)LbxPlane.SelectedValue;
                return 0;
            }
            set
            {
                LbxPlane.SelectedValue = value;
            }
        }

        public int TerrainId
        {
            get
            {
                if (LbxTerrain.SelectedItem != null)
                    return (int)LbxTerrain.SelectedValue;
                return 0;
            }
            set
            {
                LbxTerrain.SelectedValue = value;
            }
        }

        public int CombatRound
        {
            get { return _combatRound; }
            set { LblCombatRound.Content = _combatRound = value; }
        }
        private int _combatRound;

        private int CgiInit
        {
            get
            {
                int.TryParse(TxtCombatInit.Text, out int ret);
                return ret;
            }
            set { TxtCombatInit.Text = value.ToString(); }
        }

        private string CgiName
        {
            get { return TxtCombatName.Text; }
            set { TxtCombatName.Text = value; }
        }

        private bool CgiPC
        {
            get { return CbxCombatPC.IsChecked == true; }
            set { CbxCombatPC.IsChecked = value; }
        }

        private int CgiHP
        {
            get
            {
                int.TryParse(TxtCombatHP.Text, out int ret);
                return ret;
            }
            set { TxtCombatHP.Text = value.ToString(); }
        }

        private int CgiAC
        {
            get
            {
                int.TryParse(TxtCombatAC.Text, out int ret);
                return ret;
            }
            set { TxtCombatAC.Text = value.ToString(); }
        }

        private int CgiFlat
        {
            get
            {
                int.TryParse(TxtCombatFlat.Text, out int ret);
                return ret;
            }
            set { TxtCombatFlat.Text = value.ToString(); }
        }

        private int CgiTouch
        {
            get
            {
                int.TryParse(TxtCombatTouch.Text, out int ret);
                return ret;
            }
            set { TxtCombatTouch.Text = value.ToString(); }
        }

        private int CgiFort
        {
            get
            {
                int.TryParse(TxtCombatFort.Text, out int ret);
                return ret;
            }
            set { TxtCombatFort.Text = value.ToString(); }
        }

        private int CgiRef
        {
            get
            {
                int.TryParse(TxtCombatRef.Text, out int ret);
                return ret;
            }
            set { TxtCombatRef.Text = value.ToString(); }
        }

        private int CgiWill
        {
            get
            {
                int.TryParse(TxtCombatWill.Text, out int ret);
                return ret;
            }
            set { TxtCombatWill.Text = value.ToString(); }
        }

        private int CefRounds
        {
            get { return IntCombatEffectRounds.Value ?? 0; }
            set { IntCombatEffectRounds.Value = value; }
        }

        private string CefName
        {
            get { return TxtCombatEffect.Text; }
            set { TxtCombatEffect.Text = value; }
        }

        #endregion

        #region Data Properties

        public ObservableCollection<CombatGridItem> combatGridItems;
        public ObservableCollection<DisplayResult> randomEncounterItems;
        private List<CombatEffectItem> combatEffects;
        private List<DisplayValues> encounterResults;
        
        private Random random;
        private FantasyDate date;
        private string saveDataPath = Path.Combine(System.Environment.CurrentDirectory, "pfdat.dat");

        #endregion

        #region Constructor

        public MainWindow()
        {
            combatEffects = new List<CombatEffectItem>();
            combatGridItems = new ObservableCollection<CombatGridItem>
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

            LbxD20.DisplayMemberPath = LbxEncounterCreatures.DisplayMemberPath = "Display";
            LbxD20.SelectedValuePath = LbxEncounterCreatures.SelectedValuePath = "Result";
            LbxD4.DisplayMemberPath = LbxEncounterCreatures.DisplayMemberPath = "Display";
            LbxD4.SelectedValuePath = LbxEncounterCreatures.SelectedValuePath = "Result";
            LbxD6.DisplayMemberPath = LbxEncounterCreatures.DisplayMemberPath = "Display";
            LbxD6.SelectedValuePath = LbxEncounterCreatures.SelectedValuePath = "Result";
            LbxD8.DisplayMemberPath = LbxEncounterCreatures.DisplayMemberPath = "Display";
            LbxD8.SelectedValuePath = LbxEncounterCreatures.SelectedValuePath = "Result";
            LbxD10.DisplayMemberPath = LbxEncounterCreatures.DisplayMemberPath = "Display";
            LbxD10.SelectedValuePath = LbxEncounterCreatures.SelectedValuePath = "Result";
            LbxD12.SelectedValuePath = LbxEncounterCreatures.SelectedValuePath = "Result";
            LbxD12.DisplayMemberPath = LbxEncounterCreatures.DisplayMemberPath = "Display";

            var c = ConfigurationManager.ConnectionStrings.Count;

            DgCombatGrid.ItemsSource = combatGridItems;
            DgCombatEffects.ItemsSource = combatEffects;
            LbxEncounterCreatures.ItemsSource = randomEncounterItems;
            random = new Random();
            date = new FantasyDate();

            if (File.Exists(saveDataPath))
                LoadSavedData();
            else
                SaveData();
        }

        #endregion

        #region Methods

        private void LoadSavedData()
        {

        }

        private void SaveData()
        {
            
        }

        private List<DisplayResult> DiceRoll(int d, int num, int add, bool addPos)
        {
            var ret = new List<DisplayResult>();
            string op = addPos ? "+" : "-";

            for(int i = 0; i < num; i++)
            {
                var roll = random.Next(d + 1);
                var result = addPos ? (roll + add) : (roll - add);
                var itm = new DisplayResult
                {
                    Display = $"{roll} {op} {add} = {result}",
                    Result = result
                };

                ret.Add(itm);
            }

            return ret;
        }

        private void GenEncounters()
        {
            var difficulty = new string[] { "[E] ", "[M] ", "[H] " };
            encounterResults = new List<DisplayValues>();
            GenMysterious();
            LblChanceEncounter.Content = random.Next(100).ToString("D2");
            LblChanceNPC.Content = random.Next(100).ToString("D2");
            int apl = 1;
            int.TryParse(TxtAPL.Text, out apl);

            apl--;

            for (int t = 0; t < 3; t++)
            {
                var CRs = new List<int[]>();
                for (int i = 0; i < 5; i++)
                {
                    int r = random.Next(6);
                    switch (r)
                    {
                        case 0:
                            CRs.Add(new int[] { apl });
                            break;
                        case 1:
                            CRs.Add(new int[] { apl - 2, apl - 2 });
                            break;
                        case 2:
                            CRs.Add(new int[] { apl - 4, apl - 4, apl - 4, apl - 4 });
                            break;
                        case 3:
                            CRs.Add(new int[] { apl - 2, apl - 3, apl - 3 });
                            break;
                        case 4:
                            CRs.Add(new int[] { apl - 2, apl - 4, apl - 4, apl - 4 });
                            break;
                        case 5:
                            CRs.Add(new int[] { apl - 5, apl - 5, apl - 5, apl - 5, apl - 5, apl - 5 });
                            break;
                    }
                }
                foreach (var cr in CRs)
                {
                    var sb = new StringBuilder();
                    foreach (var c in cr)
                    {
                        switch (c)
                        {
                            case (-4):
                                sb.Append("1/8");
                                sb.Append(", ");
                                break;
                            case (-3):
                                sb.Append("1/6");
                                sb.Append(", ");
                                break;
                            case (-2):
                                sb.Append("1/4");
                                sb.Append(", ");
                                break;
                            case (-1):
                                sb.Append("1/3");
                                sb.Append(", ");
                                break;
                            case (0):
                                sb.Append("1/2");
                                sb.Append(", ");
                                break;
                            default:
                                sb.Append(c);
                                sb.Append(", ");
                                break;
                        }
                    }
                    sb.Remove(sb.Length - 2, 2);
                    encounterResults.Add(new DisplayValues() { Display = difficulty[t] + sb.ToString(), Values = CRs });
                }
                apl++;
            }
        }

        private void GenMysterious()
        {
            if (random.Next(100) == 0)
            {
                var m = random.Next(100);

                if (m == 1)
                {
                    LblChanceMysterious.Foreground = Brushes.Blue;
                    LblChanceMysterious.Content = "VGD";
                }
                else if (m == 0)
                {
                    LblChanceMysterious.Foreground = Brushes.Red;
                    LblChanceMysterious.Content = "VBD";
                }
                else if (m <= 10)
                {
                    LblChanceMysterious.Foreground = Brushes.Green;
                    LblChanceMysterious.Content = "GUD";
                }
                else if (m >= 90)
                {
                    LblChanceMysterious.Foreground = Brushes.Orange;
                    LblChanceMysterious.Content = "BAD";
                }
                else
                {
                    LblChanceMysterious.Foreground = Brushes.Black;
                    LblChanceMysterious.Content = "N/A";
                }

            }
            else
            {
                LblChanceMysterious.Foreground = Brushes.Black;
                LblChanceMysterious.Content = "N/A";
            }
        }

        private void LoadMonsters(int[] crs)
        {
            var req = new RandomEncounterRequest
            {
                Crs = crs
            };
            var result = DBClient.GetEncounters(req);
            if (result.Success)
            {
                foreach (var mon in result.EncounterItems)
                {
                    var entry = new DisplayResult
                    {
                        Result = mon.BestiaryId,
                        Display = mon.Name + " [" + ParseCR(mon.Cr) + "]"
                    };

                    randomEncounterItems.Add(entry);
                }
            }
            else
                MessageBox.Show("WARNING - Unable to connect to DB for LoadMonsters");
        }

        private string ParseCR(int cr)
        {
            switch (cr)
            {
                case 0:
                    return "1/2";
                case -1:
                    return "1/3";
                case -2:
                    return "1/4";
                case -3:
                    return "1/6";
                case -4:
                    return "1/8";
                default:
                    return cr.ToString();
            }
        }

        private void AddCreatureToCombat()
        {
            if (LbxEncounterCreatures.SelectedItems != null)
            {
                foreach (DisplayResult item in LbxEncounterCreatures.SelectedItems)
                {
                    var b = DBClient.GetBestiary(item.Result);
                    combatGridItems.Add(new CombatGridItem(b));
                }
            }
        }

        private void CombatNextRound()
        {
            CombatRound++;

            var removeItems = new List<CombatEffectItem>();

            foreach (var item in combatEffects)
            {
                if (item.Rounds <= 0)
                    removeItems.Add(item);
                else
                    item.Rounds--;
            }

            foreach (var item in removeItems)
            {
                combatEffects.Remove(item);
            }
        }

        private void CombatEnd()
        {
            CombatRound = 0;
            combatEffects.Clear();

            var removeItems = new List<CombatGridItem>();
            foreach (var item in combatGridItems)
            {
                if (!item.PC)
                    removeItems.Add(item);
            }
            foreach (var item in removeItems)
            {
                combatGridItems.Remove(item);
            }
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

        private void BtnGenerateEncounters_Click(object sender, RoutedEventArgs e)
        {
            GenEncounters();
        }

        private void BtnClearEncounters_Click(object sender, RoutedEventArgs e)
        {
            randomEncounterItems.Clear();
        }

        private void BtnAddToCombat_Click(object sender, RoutedEventArgs e)
        {
            AddCreatureToCombat();
        }

        private void LbxEncounterCreatures_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AddCreatureToCombat();
        }

        private void LbxEncounterCRs_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (LbxEncounterCRs.SelectedItem != null)
            {
                LoadMonsters((int[])LbxEncounterCRs.SelectedValue);
            }
        }

        private void BtnCombatNextRound_Click(object sender, RoutedEventArgs e)
        {
            CombatNextRound();
        }

        private void BtnCombatEnd_Click(object sender, RoutedEventArgs e)
        {
            CombatEnd();
        }

        private void BtnCombatAddNew_Click(object sender, RoutedEventArgs e)
        {
            var cgi = new CombatGridItem()
            {
                Name = CgiName,
                AC = CgiAC,
                ACFlat = CgiFlat,
                ACTouch = CgiTouch,
                BestiaryId = 0,
                Fort = CgiFort,
                Ref = CgiRef,
                Will = CgiWill,
                HP = CgiHP,
                Init = CgiInit,
                PC = CgiPC
            };

            combatGridItems.Add(cgi);
        }

        private void BtnCombatAddEffect_Click(object sender, RoutedEventArgs e)
        {
            var cef = new CombatEffectItem()
            {
                Rounds = CefRounds,
                Effect = CefName
            };

            combatEffects.Add(cef);
        }
    }
}
