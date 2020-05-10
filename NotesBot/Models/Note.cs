using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Models
{
  [Serializable]
  [Table("Notes")]
  public class Note
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string EntryName { get; set; }
    public string Tag { get; set; }
    public string Entry { get; set; }

    public Note() { }

    public Note(User creator, string name, string entry, string tag = "")
    {
      UserId = creator?.UserId;
      UserName = creator?.UserName;
      EntryName = name;
      Entry = entry;
      Tag = tag;
    }
  }
}
