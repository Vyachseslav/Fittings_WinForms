using System.ComponentModel;

namespace Fittings
{
    public class Project : INotifyPropertyChanged
    {
        private string title;
        private string company;
        private double price;
        private double _fittingsPrice;

        public int Id { get; set; }

        public string Name
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Name");
            }
        }
        public string Description
        {
            get { return company; }
            set
            {
                company = value;
                OnPropertyChanged("Description");
            }
        }
        public double TotalPrice
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        public double FittingsPrice
        {
            get { return _fittingsPrice; }
            set
            {
                _fittingsPrice = value;
                OnPropertyChanged("FittingsPrice");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
