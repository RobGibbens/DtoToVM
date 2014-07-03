namespace DtoToVM
{
	using AutoMapper;
	using DtoToVM.Dtos;
	using DtoToVM.Models;

	public class Bootstrapper
	{
		public void Automapper()
		{
			Mapper.CreateMap<ConferenceDto, Conference> ()
				.ForMember(dest => dest.Latitude, opt => opt.ResolveUsing<LatitudeResolver>())
				.ForMember(dest => dest.Longitude, opt => opt.ResolveUsing<LongitudeResolver>());
		}
	}

	public class LatitudeResolver : ValueResolver<ConferenceDto, double>
	{
		protected override double ResolveCore(ConferenceDto source)
		{
			return source.Position[0];
		}
	}

	public class LongitudeResolver : ValueResolver<ConferenceDto, double>
	{
		protected override double ResolveCore(ConferenceDto source)
		{
			return source.Position[1];
		}
	}
}