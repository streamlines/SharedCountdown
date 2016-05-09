using SQLite;
using SharedCountdown.BusinessLayer;
using System.Collections.Generic;
using System.Linq;

namespace SharedCountdown.DataLayer
{
    /// <summary>
    /// CountdownDatabase builds on SQLite.Net and represents the database
    /// </summary>
    public class CountdownDatabase : SQLiteConnection
    {
        static object locker = new object();

        /// <summary>
        /// Initalise the database
        /// </summary>
        /// <param name='path'>
        /// Path
        /// </param>
        public CountdownDatabase (string path) : base (path)
        {
            CreateTable<Countdown>();
        }

        public IEnumerable<T> GetItems<T> () where T : BusinessLayer.Contracts.IBusinessEntity, new ()
        {
            lock (locker)
            {
                return (from i in Table<T>() select i).ToList();
            }
        }

        public IEnumerable<T> GetItems<T>(bool favourite) where T : BusinessLayer.Contracts.IBusinessEntity, new()
        {
            lock (locker)
            {
                return (from i in Table<T>()
                        where i.Favourite.Equals(favourite)
                        select i).ToList();
            }
        }

        public T GetItem<T> (int id) where T : BusinessLayer.Contracts.IBusinessEntity, new ()
        {
            lock (locker)
            {
                return Table<T>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveItem<T> (T item) where T : BusinessLayer.Contracts.IBusinessEntity, new()
        {
            lock(locker)
            {
                if (item.ID != 0)
                {
                    Update(item);
                    return item.ID;
                } else
                {
                    return Insert(item);
                }
            }
        }

        public int DeleteItem<T>(int id) where T: BusinessLayer.Contracts.IBusinessEntity, new()
        {
            lock (locker)
            {
                return Delete(new T() { ID = id });
            }
        }
    }
}
