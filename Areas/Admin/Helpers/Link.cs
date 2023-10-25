using System.Text.RegularExpressions;

namespace WebApp.Areas.Admin.Helpers
{
    public class Link
    {
        
        private static Regex purifyUrlRegex = new Regex("[^-a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ0-9_ ]", RegexOptions.Compiled);
        private static Regex dashesRegex = new Regex("[-_ ]+", RegexOptions.Compiled);
        public static string NameUrl(string name)
        {

            return PrepareUrlText(name);
        }
        private static string PrepareUrlText(string urlText)
        {
            // remove all characters that aren't a-z, 0-9, dash, underscore or space
            urlText = purifyUrlRegex.Replace(urlText, "");

            // remove all leading and trailing spaces
            urlText = urlText.Trim();

            // change all dashes, underscores and spaces to dashes
            urlText = dashesRegex.Replace(urlText, "-");

            // return the modified string    
            urlText = StripText(urlText);
            return urlText;
        }
        private static string StripText(string title)
        {
            string output = title.Trim();
            string[,] znakiSpecjalne = {
        { "Ą", "A" }, { "Ć", "C" }, { "Ę", "E" }, { "Ł", "L" }, { "Ń", "N" }, { "Ó", "O" }, { "Ś", "S" }, { "Ź", "Z" }, { "Ż", "Z" },
        { "ą", "a" }, { "ć", "c" }, { "ę", "e" }, { "ł", "l" }, { "ń", "n" }, { "ó", "o" }, { "ś", "s" }, { "ź", "z" }, { "ż", "z" },
        { " ", "-" },
        };
            for (int i = 0; i < znakiSpecjalne.GetLength(0); i++)
            {
                output = output.Replace(znakiSpecjalne[i, 0], znakiSpecjalne[i, 1]);
            }

            return output;
        }
    }
}
