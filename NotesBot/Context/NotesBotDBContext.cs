using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotesBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Context
{
  public class NotesBotDBContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(Configurator.SqlConnectionString);
    }
  }
}
