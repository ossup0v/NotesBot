using Microsoft.EntityFrameworkCore;
using NotesBot.Context;
using NotesBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Database.MySQLRepository
{
  public class SqlNoteRepository : IRepository<Note>
  {
    private NotesBotDBContext _context;
    public SqlNoteRepository()
    {
      _context = new NotesBotDBContext();
    }

    public void Add(Note v)
    {
      _context.Notes.Add(v);
    }

    public void Delete(int id)
    {
      var v = GetById(id);
      if (v != null)
        _context.Notes.Remove(v);
    }

    public void Dispose()
    {
      _context.Dispose();
      GC.SuppressFinalize(this);
    }

    public IEnumerable<Note> GetAll()
    {
      return _context?.Notes;
    }

    public Note GetById(int id)
    {
      return _context.Notes.Find(id);
    }

    public int Save()
    {
      _context.SaveChanges();
      return GetLast().Id;
    }

    public void Update(Note v)
    {
      _context.Entry(v).State = EntityState.Modified;
    }

    private Note GetLast()
    {
      return _context.Notes.OrderByDescending(v => v.Id).FirstOrDefault();
    }
  }
}
