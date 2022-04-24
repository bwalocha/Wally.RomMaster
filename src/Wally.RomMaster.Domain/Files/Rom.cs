using Wally.Lib.DDD.Abstractions.DomainModels;

namespace Wally.RomMaster.Domain.Files
{
	public class Rom : Entity
	{
		// Hide public .ctor
#pragma warning disable CS8618
		private Rom()
#pragma warning restore CS8618
		{
		}
		
		public string Name { get; private set; }

		public long Size { get; private set; }

		public string Crc { get; private set; }

		public string Sha1 { get; private set; }

		public string Md5 { get; private set; }

		// public string Merge { get; set; }

		// public string Status { get; set; }

		// public string Date { get; set; }
	}
}
