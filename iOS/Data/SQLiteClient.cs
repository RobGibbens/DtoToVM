using Xamarin.Forms;
using DtoToVM.iOS.Data;

[assembly: Dependency (typeof(SQLiteClient))]
namespace DtoToVM.iOS.Data
{
	using System;
	using DtoToVM.Data;
	using SQLite.Net.Async;
	using System.IO;
	using SQLite.Net.Platform.XamarinIOS;
	using SQLite.Net;

	public class SQLiteClient : ISQLite
	{
		public SQLiteAsyncConnection GetConnection ()
		{
			var sqliteFilename = "Conferences.db3";
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var libraryPath = Path.Combine (documentsPath, "..", "Library");
			var path = Path.Combine (libraryPath, sqliteFilename);
		
			var platform = new SQLitePlatformIOS ();

			var connectionWithLock = new SQLiteConnectionWithLock (
				                          platform,
				                          new SQLiteConnectionString (path, true));

			var connection = new SQLiteAsyncConnection (() => connectionWithLock);

			return connection;
		}
	}
}