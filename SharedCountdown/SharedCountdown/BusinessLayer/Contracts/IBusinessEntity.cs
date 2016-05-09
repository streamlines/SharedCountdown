using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCountdown.BusinessLayer.Contracts
{
    public interface IBusinessEntity
    {
        int ID { get; set; }
        bool Favourite { get; set; }
    }
}
