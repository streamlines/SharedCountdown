using System;
using System.Collections.Generic;
using SharedCountdown.BusinessLayer;
using SharedCountdown.DataAccessLayer;

namespace SharedCountdown.BusinessLayer.Managers
{
    public static class CountdownManager
    {
        static CountdownManager()
        {

        }

        public static Countdown GetCountdown(int id)
        {
            return DataAccessLayer.CountdownRepository.GetCountdown(id);
        }

        public static IList<Countdown> GetCountdowns ()
        {
            return new List<Countdown>(DataAccessLayer.CountdownRepository.GetCountdowns());
        }

        public static IList<Countdown> GetFavouriteCountdowns()
        {
            return new List<Countdown>(DataAccessLayer.CountdownRepository.GetFavouriteCountdowns());
        }

        public static int SaveCountdown (Countdown item)
        {
            return DataAccessLayer.CountdownRepository.SaveCountdown(item);
        }

        public static int DeleteCountdown (int id)
        {
            return DataAccessLayer.CountdownRepository.DeleteCountdown(id);
        }

    }
}
