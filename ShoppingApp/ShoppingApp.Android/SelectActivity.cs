using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ShoppingApp.ViewModels;
using System.Collections.Generic;

namespace ShoppingApp.Droid
{
    [Activity(Label = "خرید از فروشگاه", Icon = "@mipmap/icon")]
    public class DetailsActivity : Activity
    {
        public static List<StoreViewModel> Items = new List<StoreViewModel>();

        protected override void OnCreate(Bundle bundle)
        {
            Items.Add(new StoreViewModel { Name = "سوپرمارکت 1" });
            Items.Add(new StoreViewModel{Name = "سوپرمارکت 2"});
            Items.Add(new StoreViewModel{Name = "سوپرمارکت 3"});

            base.OnCreate(bundle);
            //SetContentView(Resource.Layout.SelectView);

            //var lv = FindViewById<ListView>(Resource.Id.listView);

            //lv.Adapter = new ArrayAdapter<StoreViewModel>(this, Android.Resource.Layout.SimpleListItem1, Android.Resource.Id.Text1, Items);

            //lv.ItemClick += OnItemClick;
        }

        void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(DetailsActivity));

            intent.PutExtra("ItemPosition", e.Position); // e.Position is the position in the list of the item the use touched

            StartActivity(intent);
        }
    }
}