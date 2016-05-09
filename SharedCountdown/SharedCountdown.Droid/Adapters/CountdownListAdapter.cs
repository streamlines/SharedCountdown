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

namespace SharedCountdown.Droid.Adapters
{
    public class CountdownListAdapter : BaseAdapter<Countdown>
    {
        protected Activity _context = null;
        protected IList<Countdown> _countdowns = new List<Countdown>();

        public CountdownListAdapter(Activity context, IList<Countdown> countdowns) : base()
        {
            this._context = context;
            _countdowns = countdowns;
        }

        public override Countdown this[int position]
        {
            get
            {
                return _countdowns[position];
            }
        }

        public override int Count
        {
            get
            {
                return _countdowns.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var item = _countdowns[position];
            View view;

            if (convertView == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.CountdownListItem, null);
            } else
            {
                view = convertView;
            }

            // TODO: Only Mark the Favourites - Once Widget is built.

            CheckedTextView nameLabel = view.FindViewById<CheckedTextView>(Resource.Id.lblName);
            nameLabel.Text = item.Name;
            nameLabel.Checked = false;
            nameLabel.SetCheckMarkDrawable(null);
            if (item.Favourite)
            {
                nameLabel.Checked = true;
                // @android:drawable/btn_star_big_on
                nameLabel.SetCheckMarkDrawable(Android.Resource.Drawable.ButtonStarBigOn);
            }


            var dateLabel = view.FindViewById<TextView>(Resource.Id.lblDate);
            dateLabel.Text = item.GetCountdown().ToString("dd") + " Days";

            return view;
        }

    }
}