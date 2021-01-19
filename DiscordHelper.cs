using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot
{
    public static class DiscordHelper
    {
        //Discord character limit is 2000
        public static string TruncateString(string value)
        {
            if (string.IsNullOrEmpty(value)) return  value;
            return value.Length <= 2000 ? value : value.Substring(0, 2000);
        }

        public static string[] parseCommands(string args)
        {
            return args.Split(":");
        }
    }
}
