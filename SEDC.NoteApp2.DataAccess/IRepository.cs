using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NoteApp2.DataAccess
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
