using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Appwidget;
using SharedCountdown.BusinessLayer;
using SharedCountdown.BusinessLayer.Managers;

namespace SharedCountdown.Services
{
    public class UpdateService : Service
    {
 
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            RemoteViews updateViews = buildUpdate(this);
            ComponentName thisWidget = new ComponentName(this, Java.Lang.Class.FromType(typeof(CountdownWidget)).Name);
            AppWidgetManager manager = AppWidgetManager.GetInstance(this);
            manager.UpdateAppWidget(thisWidget, updateViews);

            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public RemoteViews buildUpdate(Context context)
        {
            Countdown countdown = new Countdown();
            RemoteViews updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget_countdown);

            //update the text from the favourite countdown into the text fields.
            IList<Countdown> favourites = CountdownManager.GetFavouriteCountdowns();
            if (favourites.Count > 0)
            {
                updateViews.SetTextViewText(Resource.Id.CountdownTitle, "No Favourite Set");
                updateViews.SetTextViewText(Resource.Id.CountdownType, "");

            } else
            {
                countdown = favourites.Single();
                updateViews.SetTextViewText(Resource.Id.CountdownTitle, countdown.Name);
                updateViews.SetTextViewText(Resource.Id.CountdownType, countdown.GetCountdown().ToString("dd") + " Days");
            }


            return updateViews;

        }
    }
}