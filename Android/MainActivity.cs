using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

namespace DtoToVM.Android
{
	[Activity (Label = "DtoToVM.Android.Android", MainLauncher = true)]
	public class MainActivity : FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var bootstrapper = new Bootstrapper ();
			bootstrapper.Automapper ();

			Forms.Init (this, bundle);

			LoadApplication (new App ());
		}
	}
}