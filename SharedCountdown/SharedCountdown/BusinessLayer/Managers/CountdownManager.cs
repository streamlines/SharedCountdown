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
            var fullList = CountdownRepository.GetCountdowns();
            List<Countdown> result = new List<Countdown>();

            foreach (Countdown item in fullList)
            {
                if (item.Favourite)
                {
                    result.Add(item);
                }
            }

            return result;
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
