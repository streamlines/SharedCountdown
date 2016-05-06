using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using SharedCountdown.BusinessLayer.Contracts;

namespace SharedCountdown.BusinessLayer
{
    public class Countdown : IBusinessEntity
    {
        public Countdown()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public bool Favourite { get; set; }

        public TimeSpan GetCountdown()
        {
            DateTime now = DateTime.Now;
            TimeSpan result = Date - now;
            return result;
        }
    }
}
