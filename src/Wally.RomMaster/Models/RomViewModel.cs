using System;

namespace Wally.RomMaster.Models
{
	public class RomViewModel : BaseViewModel
	{
		private int _id;

		public int Id
		{
			get
			{
				return _id;
			}

			set
			{
				_id = value;
				NotifyPropertyChanged();
			}
		}

		private string _name;

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				NotifyPropertyChanged();
			}
		}

		public long Size { get; internal set; }

		public string Crc { get; internal set; }

		public string Sha1 { get; internal set; }

		public string Md5 { get; internal set; }
	}
}
