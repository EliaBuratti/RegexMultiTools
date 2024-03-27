using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexMultiTools { 

    public class Tool
    {
        private static readonly Regex EmailPattern = new Regex(@"^[a-zA-Z0-9._+]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", RegexOptions.Compiled);

        private static readonly string UserPatternDetails = @"Nome: (?<nome>\w+) Cognome: (?<cognome>\w+)";

        private static readonly string BadWords = "Vaffanculo|Coglione|Muori|Puttana|Dio";

        private static readonly Regex FiscalCodePattern = new Regex(@"^[A-Z]{6}\d{2}[A-Z]\d{2}[A-Z]\d{3}[A-Z]$");
        
        private static readonly Regex DateFormatPattern = new Regex(@"\b(\d{2})/(\d{2})/(\d{4})\b");

        public static bool IsValidEmail(string email)
        {
            return EmailPattern.IsMatch(email);
        }

        public static Match FindDetails(string phrase)
        {
            Match match = Regex.Match(phrase, UserPatternDetails);
            return match;
        }

        public static string CensorBadWords(string phrase, string words, bool addBasicWord = true)
        {
            string wordlist = BadWords.ToLower();

            if (words.Trim().Length > 0)
            {
                string newWords = Regex.Replace(words.ToLower(), @"\s|\W", "|").Trim('|');

                wordlist = addBasicWord == true ? (wordlist += "|" + newWords) : wordlist;
            }

            string result = Regex.Replace(phrase.ToLower(), @"\b(" + wordlist + @")\b", match => new string('*', match.Length), RegexOptions.IgnoreCase);

            return result;
        }

        public static bool CheckFiscalCode(string fiscalCode) 
        {

            return FiscalCodePattern.IsMatch(fiscalCode);

        }

        public static string ConvertToEUformat (string americanDate)
        {
            var matchDate = DateFormatPattern.Match(americanDate);

            if (matchDate.Success) 
            { 
                return $"{matchDate.Groups[2].Value}/{matchDate.Groups[1].Value}/{matchDate.Groups[3].Value}";
            }
            else
            {
                return "Date format not valid.";
            }
        }


    }
}
