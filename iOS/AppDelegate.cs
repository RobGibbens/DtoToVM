using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace DtoToVM.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			var bootstrapper = new Bootstrapper ();
			bootstrapper.Automapper ();

			Forms.Init ();

			LoadApplication (new App ());
			
			return base.FinishedLaunching (app, options);
		}
	}
}