using lab8.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab8.Services.Repository
{
    public class SqLiteRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly SQLiteConnection _database;

        public SqLiteRepository(SQLiteConnection sqLiteConnection)
        {
            _database = sqLiteConnection;
            _database.CreateTable<T>(); // Si la table existe déjà ne fait rien. 
        }
        public IEnumerable<T> GetAll()
        {

            return _database.Table<T>().ToList();
        }

        public T GetById(int id)
        {
            return _database.Find<T>(id);
        }

        public void Delete(T entity)
        {
            _database.Delete(entity);
        }

        public void Add(T entity)
        {
            _database.Insert(entity);
        }

        public void Update(T entity)
        {
            _database.Update(entity);
        }
    }
}
