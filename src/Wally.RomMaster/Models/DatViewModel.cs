namespace Wally.RomMaster.Models
{
    public class DatViewModel : BaseViewModel
    {
        private int id;
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }
    }
}
