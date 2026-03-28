using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities.SystemSeeding
{
    public class SystemCountryCodes
    {
        /// <summary>
        /// Dictionary of International Direct Dialing (IDD) codes with '+' prefix.
        /// Key: The dial code (string), Value: Country Name (string).
        /// </summary>
        public static readonly Dictionary<string, string> CallingCodes = new Dictionary<string, string>
        {
            { "+1", "USA Canada Caribbean" },
            { "+20", "Egypt" },
            { "+27", "South Africa" },
            { "+30", "Greece" },
            { "+31", "Netherlands" },
            { "+32", "Belgium" },
            { "+33", "France" },
            { "+34", "Spain" },
            { "+39", "Italy" },
            { "+40", "Romania" },
            { "+41", "Switzerland" },
            { "+43", "Austria" },
            { "+44", "United Kingdom" },
            { "+45", "Denmark" },
            { "+46", "Sweden" },
            { "+47", "Norway" },
            { "+48", "Poland" },
            { "+49", "Germany" },
            { "+52", "Mexico" },
            { "+54", "Argentina" },
            { "+55", "Brazil" },
            { "+60", "Malaysia" },
            { "+61", "Australia" },
            { "+62", "Indonesia" },
            { "+63", "Philippines" },
            { "+64", "New Zealand" },
            { "+65", "Singapore" },
            { "+66", "Thailand" },
            { "+81", "Japan" },
            { "+82", "South Korea" },
            { "+84", "Vietnam" },
            { "+86", "China" },
            { "+90", "Turkey" },
            { "+91", "India" },
            { "+92", "Pakistan" },
            { "+93", "Afghanistan" },
            { "+94", "Sri Lanka" },
            { "+95", "Myanmar" },
            { "+98", "Iran" },
            { "+212", "Morocco" },
            { "+351", "Portugal" },
            { "+353", "Ireland" },
            { "+358", "Finland" },
            { "+852", "Hong Kong" },
            { "+880", "Bangladesh" },
            { "+966", "Saudi Arabia" },
            { "+971", "United Arab Emirates" },
        };

        public static string GetCountryByPhoneCode(string dialCode)
        {
            // Ensure the input starts with + for the dictionary lookup
            if (!dialCode.StartsWith("+")) dialCode = "+" + dialCode;

            return CallingCodes.TryGetValue(dialCode, out var country) ? country : "Unknown";
        }
    }
}
