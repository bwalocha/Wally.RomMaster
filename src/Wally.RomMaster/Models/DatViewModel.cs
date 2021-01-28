using System;
using System.Collections.Generic;
using System.Linq;

namespace Wally.RomMaster.Models
{
	public class DatViewModel : BaseViewModel
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

		public string Author { get; internal set; }

		public string Category { get; internal set; }

		public DateTime? Date { get; internal set; }

		public string Description { get; internal set; }

		public string Version { get; internal set; }

		public IEnumerable<GameViewModel> Games { get; internal set; }

		public int GameCount
		{
			get { return Games.Count(); }
		}
	}
}
