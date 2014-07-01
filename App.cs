namespace DtoToVM
{
	using Xamarin.Forms;
	using DtoToVM.Pages;

	public class App
	{
		public static Page GetMainPage ()
		{	
			return new ConferencesPage ();
		}
	}
}