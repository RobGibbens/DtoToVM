namespace DtoToVM
{
	using Xamarin.Forms;
	using DtoToVM.Pages;

	public class App : Application
	{
		public App ()
		{
			MainPage = new ConferencesPage ();
		}
	}
}