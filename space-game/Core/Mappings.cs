using Core.Classes;
using Mapster;

namespace Core
{
    public class Mappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<int, Mass>().MapWith(src => Mass.FromKilograms(src));
            config.ForType<double, Mass>().MapWith(src => Mass.FromKilograms(src));

            config.ForType<int, Distance>().MapWith(src => Distance.FromKilometers(src));
            config.ForType<int?, Distance>().MapWith(src => src.HasValue ? Distance.FromKilometers(src.Value) : null);
            config.ForType<double, Distance>().MapWith(src => Distance.FromKilometers(src));
            config.ForType<double?, Distance>().MapWith(src => src.HasValue ? Distance.FromKilometers(src.Value) : null);

            config.ForType<int, Angle>().MapWith(src => Angle.FromDegrees(src));
            config.ForType<int?, Angle>().MapWith(src => src.HasValue ? Angle.FromDegrees(src.Value) : null);
            config.ForType<double, Angle>().MapWith(src => Angle.FromDegrees(src));
            config.ForType<double?, Angle>().MapWith(src => src.HasValue ? Angle.FromDegrees(src.Value) : null);
        }
    }
}
