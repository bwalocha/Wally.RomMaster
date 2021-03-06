using System;
using System.Text.RegularExpressions;

using Wally.RomMaster.BackgroundServices.Models;

namespace Wally.RomMaster.BackgroundServices.Extensions;

public static class ExcludeSettingsExtensions
{
	// TODO: to improve
	public static bool Match(this ExcludeSettings exclude, string file)
	{
		var pattern = exclude.Pattern.Replace(".", "\\.", StringComparison.InvariantCulture)
			.Replace("?", ".", StringComparison.InvariantCulture)

			// .Replace("**", ".*?", StringComparison.InvariantCulture) //TODO reorder
			.Replace("*", ".*", StringComparison.InvariantCulture);

		var regex = new Regex(
			$"^{pattern}$",
			RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

		return regex.IsMatch(file);
	}
}
