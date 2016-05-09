using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SharedCountdown.BusinessLayer;
using SharedCountdown.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SharedCountdown.Droid.Screens
{
    [Activity(Label = "Countdown Details")]
    public class CountdownDetailsScreen : Activity
    {
        protected Countdown _countdown = new Countdown();
        protected EditText _notesTextEdit;
        protected EditText _nameTextEdit;
        protected Button _dateSelectButton;
        protected CheckBox _FavouriteCheckBox;
         
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.ActionBar);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            // Create your application here
            int CountdownID = Intent.GetIntExtra("CountdownID", 0);
            if (CountdownID > 0 )
            {
                _countdown = BusinessLayer.Managers.CountdownManager.GetCountdown(CountdownID);
            }

            // Sort out layout
            SetContentView(Resource.Layout.CountdownDetail);
            _notesTextEdit = FindViewById<EditText>(Resource.Id.txtNotes);
            _nameTextEdit = FindViewById<EditText>(Resource.Id.txtName);
            _dateSelectButton = FindViewById<Button>(Resource.Id.btnDateSelect);
            _FavouriteCheckBox = FindViewById<CheckBox>(Resource.Id.chkboxFavourite);

            if(_nameTextEdit != null)
            {
                _nameTextEdit.Text = _countdown.Name;
            }

            if(_notesTextEdit != null)
            {
                _notesTextEdit.Text = _countdown.Notes;
            }

            if(_dateSelectButton != null)
            {
                _dateSelectButton.Click += DateSelect_OnClick;
                if(_countdown.Date != null)
                {
                    _dateSelectButton.Text = _countdown.Date.ToLongDateString();
                }
            }

            if(_FavouriteCheckBox != null)
            {
                _FavouriteCheckBox.Selected = _countdown.Favourite;
            }

        }

        protected void Save()
        {
            _countdown.Name = _nameTextEdit.Text;
            _countdown.Notes = _notesTextEdit.Text;
            _countdown.Favourite = _FavouriteCheckBox.Selected;
            if (_countdown.Favourite)
            {
                resetFavorites();
            }

            SharedCountdown.BusinessLayer.Managers.CountdownManager.SaveCountdown(_countdown);
            Finish();
        }

        private void resetFavorites()
        {
            IList<Countdown> countdowns = BusinessLayer.Managers.CountdownManager.GetFavouriteCountdowns();
            if (countdowns.Count > 0)
            {
                foreach (Countdown item in countdowns)
                {
                    item.Favourite = false;
                    BusinessLayer.Managers.CountdownManager.SaveCountdown(item);
                }
            }
        }

        protected void CancelDelete()
        {
            if(_countdown.ID != 0)
            {
                SharedCountdown.BusinessLayer.Managers.CountdownManager.DeleteCountdown(_countdown.ID);
            }
            Finish();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_detailsscreen, menu);
            IMenuItem menuItem = menu.FindItem(Resource.Id.menu_delete_countdown);
            menuItem.SetTitle(_countdown.ID == 0 ? "Cancel" : "Delete");
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_save_countdown:
                    Save();
                    return true;

                case Resource.Id.menu_delete_countdown:
                    CancelDelete();
                    return true;

                default:
                    Finish();
                    return base.OnOptionsItemSelected(item);

            }
        }

        void DateSelect_OnClick(object sender, EventArgs eventargs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                _countdown.Date = time;
                _dateSelectButton.Text = _countdown.Date.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
    }
}