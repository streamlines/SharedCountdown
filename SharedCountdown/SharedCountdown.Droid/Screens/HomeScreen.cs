using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedCountdown.BusinessLayer;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SharedCountdown.Droid.Screens
{
    [Activity(Label = "Countdowns", MainLauncher =true)]
    public class HomeScreen : Activity
    {
        protected Adapters.CountdownListAdapter countdownList;
        protected IList<Countdown> countdowns;
        protected ListView CountdownListView = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            RequestWindowFeature(WindowFeatures.ActionBar);
            SetContentView(Resource.Layout.HomeScreen);
            CountdownListView = FindViewById<ListView>(Resource.Id.lstCountdowns);

            //wire up the list click
            if (CountdownListView != null)
            {
                CountdownListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
                {
                    Intent countdownDetails = new Intent(this, typeof(CountdownDetailsScreen));
                    countdownDetails.PutExtra("CountdownID", countdowns[e.Position].ID);
                    StartActivity(countdownDetails);
                };
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            countdowns = SharedCountdown.BusinessLayer.Managers.CountdownManager.GetCountdowns().OrderBy(o=>o.Date).ToList();

            countdownList = new Adapters.CountdownListAdapter(this, countdowns);
            CountdownListView.Adapter = countdownList;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_homescreen, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_add_countdown:
                    StartActivity(typeof(CountdownDetailsScreen));
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}