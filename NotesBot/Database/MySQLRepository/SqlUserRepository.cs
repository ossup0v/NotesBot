using Microsoft.EntityFrameworkCore;
using NotesBot.Context;
using NotesBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Database.MySQLRepository
{
  public class SqlUserRepository : IRepository<User>
  {
    private NotesBotDBContext _context;
    public SqlUserRepository()
    {
      _context = new NotesBotDBContext();
    }

    public void Add(User v)
    {
      _context.Users.Add(v);
    }

    public void Delete(int id)
    {
      var v = GetById(id);
      if (v != null)
        _context.Users.Remove(v);
    }

    public void Dispose()
    {
      _context.Dispose();
      GC.SuppressFinalize(this);
    }

    public IEnumerable<User> GetAll()
    {
      return _context.Users;
    }

    public User GetById(int id)
    {
      return _context.Users.Find(id);
    }

    public User GetByUserId(string userId)
    {
      return _context.Users.FirstOrDefault(user=> user.UserId == userId);
    }

    public int Save()
    {
      _context.SaveChanges();
      return GetLast().Id;
    }

    public void Update(User v)
    {
      _context.Entry(v).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    private User GetLast()
    {
      return _context.Users.OrderByDescending(v => v.Id).FirstOrDefault();
    }
  }
}

