using System.Windows.Input;

namespace CurrencyExchangeCustomer.Models
{
    public class NoticeBoardModel : PropertyChangeNotifier
    {
        bool _isBoardVisible = false;
        string _boardMessage = string.Empty;

        public NoticeBoardModel() { }

        public ICommand CloseNoticeCommand => new Command(() => 
        {
            this.IsBoardVisible = false;
        });

        public bool IsBoardVisible 
        { 
            get => _isBoardVisible;
            set 
            {
                _isBoardVisible = value;
                OnPropertyChanged();
            } 
        }

        public string BoardMessage 
        { 
            get => _boardMessage;
            set 
            {
                _boardMessage = value;
                OnPropertyChanged();
            } 
        }
    }
}
