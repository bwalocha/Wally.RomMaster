using System;

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

        public string Author { get; internal set; }
        public string Category { get; internal set; }
        public DateTime? Date { get; internal set; }
        public string Description { get; internal set; }
        public string Version { get; internal set; }
    }
}
