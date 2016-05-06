using System;
using System.Collections.Generic;
using System.Text;
using SharedCountdown.BusinessLayer;
using System.IO;

namespace SharedCountdown.DataAccessLayer
{
    public class CountdownRepository
    {
        DataLayer.CountdownDatabase db = null;
        protected static string dbLocation;
        protected static CountdownRepository me;

        static CountdownRepository ()
        {
            me = new CountdownRepository();
        }

        protected CountdownRepository()
        {
            dbLocation = DatabaseFilePath;
            db = new SharedCountdown.DataLayer.CountdownDatabase(dbLocation);
        }

        public static string DatabaseFilePath
        {
            get
            {
                var sqliteFilename = "CountdownDB.db3";

#if NETFX_CORE
                var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
#else

#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
	            var path = sqliteFilename;
#else

#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
	            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
#else
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "../Library/"); // Library folder
#endif
				var path = Path.Combine (libraryPath, sqliteFilename);
#endif

#endif
                return path;
            }
        }

        public static Countdown GetCountdown(int id)
        {
            return me.db.GetItem<Countdown>(id);
        }

        public static IEnumerable<Countdown> GetCountdowns ()
        {
            return me.db.GetItems<Countdown>();
        }

        public static int SaveCountdown (Countdown item)
        {
            return me.db.SaveItem<Countdown>(item);
        }

        public static int DeleteCountdown(int id)
        {
            return me.db.DeleteItem<Countdown>(id);
        }
    }
}
