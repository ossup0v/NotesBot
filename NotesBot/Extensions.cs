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
  }
}
