﻿using System.Text.RegularExpressions;

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
                    .Replace(".", "\\.")
                    .Replace("?", ".")
                    // .Replace("**", ".*?") //TODO reorder
                    .Replace("*", ".*");

                regex = new Regex($"^{pattern}$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            }
            else
            {
                throw new System.Exception();
            }

            return regex.IsMatch(file);
        }
    }
}
