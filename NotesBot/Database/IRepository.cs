using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBot.Database
{
  interface IRepository<T> :IDisposable where T : class
  {
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T v);
    void Update(T v);
    void Delete(int id);
    int Save();
  }
}
