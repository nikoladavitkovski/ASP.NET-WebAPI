using System;
using System.Collections.Generic;
using System.Text;

namespace NotesApp.DataAccess
{
    public interface IRepository<T>
    {
        List<T> GetAll();

        T GetById(int id);

        void Insert(T entity);

        void Update(T update);

        void Delete(int id);
    }
}
