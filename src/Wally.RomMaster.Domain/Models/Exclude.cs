using System;
using System.Text.RegularExpressions;

namespace Wally.RomMaster.Domain.Models
{
	public class Exclude
	{
		private Regex _regex;

		public string Pattern { get; set; }

		public bool Match(string file)
		{
			if (_regex == null)
			{
				var pattern = Pattern.Replace(".", "\\.", StringComparison.InvariantCulture)
					.Replace("?", ".", StringComparison.InvariantCulture)

					// .Replace("**", ".*?", StringComparison.InvariantCulture) //TODO reorder
					.Replace("*", ".*", StringComparison.InvariantCulture);

				_regex = new Regex(
					$"^{pattern}$",
					RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
			}
			else
			{
				throw new Exception();
			}

			return _regex.IsMatch(file);
		}
	}
}
