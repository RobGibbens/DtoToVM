namespace DtoToVM.Pages
{
	using System.Threading.Tasks;
	using Xamarin.Forms;
	using DtoToVM.ViewModels;

	public class ConferencesPage : ContentPage
	{
		ListView _conferencesListView;

		public ConferencesPage ()
		{
			this.Content = new Label { 
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Text = "Loading"
			};

			Init ();
		}

		private async Task Init ()
		{
			_conferencesListView = new ListView { 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
			};

			var cell = new DataTemplate (typeof(TextCell));
			cell.SetBinding (TextCell.TextProperty, "Name");
			cell.SetBinding (TextCell.DetailProperty, new Binding (path: "Start", stringFormat: "{0:MM/dd/yyyy}"));

			_conferencesListView.ItemTemplate = cell;

			var viewModel = new ConferencesViewModel ();
			await viewModel.GetConferences ();
			_conferencesListView.ItemsSource = viewModel.Conferences;

			this.Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (
					left: 0, 
					right: 0, 
					bottom: 0, 
					top: Device.OnPlatform (iOS: 20, Android: 0, WinPhone: 0)),
				Children = { 
					_conferencesListView 
				}
			};
		}
	}
}