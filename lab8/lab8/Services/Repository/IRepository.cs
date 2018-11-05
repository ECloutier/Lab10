using lab8.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab8.Services.Repository
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
    }
}
