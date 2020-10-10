using System;
using System.Text.RegularExpressions;

namespace Promotion_Engine_SCM_CT_v1009.Utilities
{
    public static class PrimaryKeyGenerator
    {
        public static string Generate()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
        }
    }
}
