using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PFDBCommon.ConnectModels
{
  public class CombatEffectItem : INotifyPropertyChanged
    {
        public int Rounds
        {
            get { return _rounds; }
            set
            {
                _rounds = value;
                NotifyPropertyChanged();
            }
        }
        public string Effect
        {
            get { return _effect; }
            set
            {
                _effect = value;
                NotifyPropertyChanged();
            }
        }
        public string Target
        {
            get { return _target; }
            set
            {
                _target = value;
                NotifyPropertyChanged();
            }
        }

        private int _rounds;
        private string _effect;
        private string _target;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
