using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SharedCountdown.BusinessLayer.Contracts
{
    public abstract class BusinessEntityBase : IBusinessEntity
    {
        public BusinessEntityBase()
        {
        }
        /// <summary>
        /// Gets or Sets the Database ID
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
