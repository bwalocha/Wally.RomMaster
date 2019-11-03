using System;

namespace Wally.RomMaster.Models
{
    public class RomViewModel : BaseViewModel
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

        public long Size { get; internal set; }

        public string Crc { get; internal set; }

        public string Sha1 { get; internal set; }

        public string Md5 { get; internal set; }
    }
}
