namespace DtoToVM.Dtos
{
	using System;

	public class ConferenceDto
	{
		public string Slug { get; set; }

		public string Name { get; set; }

		public DateTime Start { get; set; }

		public double[] Position { get; set; }
	}
}