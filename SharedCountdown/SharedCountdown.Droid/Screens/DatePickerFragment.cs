using System;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace SharedCountdown.Droid.Screens
{
    public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();
        Action<DateTime> _dateSelectedHandler = delegate { };

        public void OnDateSet(DatePicker view, int year, int MonthOfYear, int DayOfMonth)
        {
            DateTime selectedDate = new DateTime(year, MonthOfYear + 1, DayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month,
                currently.Day);
            return dialog;
        }
    }
}