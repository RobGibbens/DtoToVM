namespace DtoToVM.Data
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using SQLite.Net.Async;
	using Xamarin.Forms;
	using DtoToVM.Data;
	using DtoToVM.Models;

	public class SQLiteClient
	{
		private static readonly AsyncLock Mutex = new AsyncLock ();
		private readonly SQLiteAsyncConnection _connection;

		public SQLiteClient ()
		{
			_connection = DependencyService.Get<ISQLite> ().GetConnection ();
			CreateDatabaseAsync ();
		}

		public async Task CreateDatabaseAsync ()
		{
			using (await Mutex.LockAsync ().ConfigureAwait (false)) {
				await _connection.CreateTableAsync<Conference> ().ConfigureAwait (false);
			}
		}

		public async Task<List<Conference>> GetConferencesAsync ()
		{
			List<Conference> conferences = new List<Conference> ();
			using (await Mutex.LockAsync ().ConfigureAwait (false)) {
				conferences = await _connection.Table<Conference> ().ToListAsync ().ConfigureAwait (false);
			}

			return conferences;
		}

		public async Task Save (Conference conference)
		{
			using (await Mutex.LockAsync ().ConfigureAwait (false)) {
				// Because our conference model is being mapped from the dto,
				// we need to check the database by name, not id
				var existingConference = await _connection.Table<Conference> ()
						.Where (x => x.Slug == conference.Slug)
						.FirstOrDefaultAsync ();

				if (existingConference == null) {
					await _connection.InsertAsync (conference).ConfigureAwait (false);
				} else {
					conference.Id = existingConference.Id;
					await _connection.UpdateAsync (conference).ConfigureAwait (false);
				}
			}
		}

		public async Task SaveAll (IEnumerable<Conference> conferences)
		{
			foreach (var conference in conferences) {
				await Save (conference);
			}
		}

	}
}

