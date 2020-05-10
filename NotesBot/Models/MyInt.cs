using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesBot.Models
{
    [Serializable]
    [Table("MyInt2s")]
    public class MyInt2
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Int")]
        public int Int { get; set; }
    }
}
