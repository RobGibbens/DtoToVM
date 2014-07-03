namespace DtoToVM.Models
{
	using System;
	using SQLite.Net.Attributes;

	public class Conference
	{
		[PrimaryKey, AutoIncrement, Column ("_id")]
		public int Id { get; set; }

		[Unique]
		public string Slug { get; set; }

		public string Name { get; set; }

		public DateTime Start { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }
	}
}