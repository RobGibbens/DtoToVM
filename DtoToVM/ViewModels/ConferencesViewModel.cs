namespace DtoToVM.ViewModels
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using PropertyChanged;
	using DtoToVM.Data;
	using DtoToVM.Models;
	using DtoToVM.Services;

	[ImplementPropertyChanged]
	public class ConferencesViewModel
	{
		readonly SQLiteClient _db;

		public ConferencesViewModel ()
		{
			_db = new SQLiteClient ();
		}

		public List<Conference> Conferences { get; set; }

		public async Task GetConferences ()
		{
			await GetLocalConferences ();
			await GetRemoteConferences ();
			await GetLocalConferences ();
		}

		private async Task GetLocalConferences()
		{
			var conferences = await _db.GetConferencesAsync ();
			this.Conferences = conferences.OrderBy(x => x.Name).ToList();
		}

		private async Task GetRemoteConferences()
		{
			var remoteClient = new TekConfClient ();
			var conferences = await remoteClient.GetConferences ().ConfigureAwait(false);
			await _db.SaveAll (conferences).ConfigureAwait(false);
		}
	}
}