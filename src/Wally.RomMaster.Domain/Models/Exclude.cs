using System;
using System.Text.RegularExpressions;

namespace Wally.RomMaster.Domain.Models
{
    public class Exclude
    {
        private Regex regex;

        public string Pattern { get; set; }

        public bool Match(string file)
        {
            if (regex == null)
            {
                var pattern = Pattern
                    .Replace(".", "\\.", StringComparison.InvariantCulture)
                    .Replace("?", ".", StringComparison.InvariantCulture)
                    // .Replace("**", ".*?", StringComparison.InvariantCulture) //TODO reorder
                    .Replace("*", ".*", StringComparison.InvariantCulture);

                regex = new Regex($"^{pattern}$",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            }
            else
            {
                throw new Exception();
            }

            return regex.IsMatch(file);
        }
    }
}
