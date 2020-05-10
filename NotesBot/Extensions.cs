using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesBot
{
  public static class Extensions
  {
    public static string ToString<T>(this List<T> @list, string separete = "")
    {
      var result = new StringBuilder();
      foreach (var item in @list)
      {
        result.Append(item.ToString() + separete);
      }
      var resultStr = result.ToString();
      if (resultStr.Length >= separete.Length)
        resultStr = resultStr.Remove(resultStr.Length - separete.Length);

      return resultStr;
    }

    /// <returns>Item1 - work itself, item2 - all the rest.</returns>
    public static Tuple<string, string> GetFirstWord(this string str, string separator = " ")
    {
      if (!str.Contains(separator))
        return new Tuple<string, string>(str, string.Empty);
      else
      {
        var indexOf = str.IndexOf(separator);
        if (indexOf == -1)
          return new Tuple<string, string>(str, string.Empty);
        var indexOfLastCharSep = indexOf + separator.Length;
        var firstW = str.Substring(0, indexOf);
        var onther = "";
        if (indexOf != -1 && str.Length >= indexOfLastCharSep)
          onther = str.Substring(indexOfLastCharSep, str.Length - (indexOfLastCharSep));
        return new Tuple<string, string>(firstW, onther);
      }
    }
    public static bool IsNullOrWhiteSpace(this string str) => String.IsNullOrWhiteSpace(str);
  }
}
