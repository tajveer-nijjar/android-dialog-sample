using System;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using static Android.App.ActionBar;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace DialogBoxSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private AlertDialog.Builder _builder;
        private AlertDialog _loadingDialog;
        private TextView _loadingProgressMessageTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SetSupportActionBar(toolbar);

            var showDialogButton = FindViewById<Button>(Resource.Id.showDialogButton);
            showDialogButton.Click += ShowDialogButton_Click;
        }

        private void ShowDialogButton_Click(object sender, EventArgs e)
        {
            _builder = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            LayoutInflater inflater = this.LayoutInflater;
            View dialogView = inflater.Inflate(Resource.Layout.layout_loading_dialog, null);
            _builder.SetView(dialogView);
            _loadingDialog = _builder.Create();
            _loadingProgressMessageTextView = dialogView.FindViewById<TextView>(Resource.Id.loadingProgressMessageTextView);
            _loadingProgressMessageTextView.Text = "This is loading";
            MoveDialogToRightTopCorner();

            _loadingDialog.Show();
        }

        private void MoveDialogToRightTopCorner()
        {
            Window window = _loadingDialog.Window;
            WindowManagerLayoutParams wlp = window.Attributes;
            wlp.Gravity = GravityFlags.Top | GravityFlags.Right;
            wlp.Width = LayoutParams.MatchParent;
            wlp.Flags &= WindowManagerFlags.DimBehind;
            window.Attributes = wlp;

            int size = CalculateSizeOfToolbar();

            ColorDrawable back = new ColorDrawable(Color.White);
            InsetDrawable inset = new InsetDrawable(back, 0, size, 100, 0);
            _loadingDialog.Window.SetBackgroundDrawable(inset);

        }

        private int CalculateSizeOfToolbar()
        {
            int size = 0;
            TypedValue tv = new TypedValue();
            if (Theme.ResolveAttribute(Android.Resource.Attribute.ActionBarSize, tv, true)) 
            {
                size = TypedValue.ComplexToDimensionPixelSize(tv.Data, Resources.DisplayMetrics);
            }

            return size;
        }
    }
}
