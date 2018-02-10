using Mapster;
using MassFrom = global::Data.ConfigDataProvider.Classes.Mass;
using MassTo = global::Core.Classes.Mass;

namespace Data.ConfigDataProvider
{
    public class Mappings: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<MassFrom, MassTo>();
            //.MapWith(src => MapMass(src));

            //config.ForType<MassTo, MassFrom>()
            //    .MapWith(src => MapMassReverse(src));

            //.Map(dest => dest.Kilograms, src => src.Kilograms);

            //config.ForType<SolarSystem, List<CelestialObjectData>>()
            //    .MapWith(src => src.System.Adapt<List<CelestialObjectData>>());


            //.Map(dest => dest.Kilograms, src => src.Kilograms)
            //.Map(dest => dest.EarthMasses, src => src.EarthMasses)
            //.Map(dest => dest.SolarMasses, src => src.SolarMasses);
        }


        private static MassTo MapMass(MassFrom input)
        {
            MassTo result;
            if (!input.Kilograms.Equals(0))
            {
                result = MassTo.FromKilograms(input.Kilograms);
            }
            else if (!input.EarthMasses.Equals(0))
            {
                result = MassTo.FromEarthMasses(input.EarthMasses);
            }
            else
            {
                result = MassTo.FromSolarMasses(input.SolarMasses);
            }

            return result;
        }

        //private static MassTo MapMass(MassFrom input)
        //{
        //    MassTo result;

        //    result = MassTo.FromEarthMasses(1);
        //    return result;
        //}

        //private static MassFrom MapMassReverse(MassTo input)
        //{
        //    return new MassFrom() { Kilograms = 1 };
        //}
    }
}
