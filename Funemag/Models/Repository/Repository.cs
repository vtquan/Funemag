using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Funemag.Models.Repository
{
    public class Repository<T> where T : class
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected DbSet<T> DbSet { get; set; }

        public Repository()
        {
            DbSet = db.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T Get(int? id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void DeleteById(int id)
        {
            DbSet.Remove(Get(id));
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public DbEntityEntry<T> Entry(T entity)
        {
            return db.Entry(entity);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}