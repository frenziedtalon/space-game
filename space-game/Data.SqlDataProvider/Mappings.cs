using Core.Extensions;
using Core.Helpers;
using Data.Classes;
using Data.SqlDataProvider.Model;
using Mapster;
using System.Collections.Generic;
using CelestialObjectType = Data.SqlDataProvider.Model.CelestialObjectType;

namespace Data.SqlDataProvider
{
    public class Mappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CelestialObject, OrbitData>()
                    .Map(dest => dest.SemiMajorAxis, src => src.SemiMajorAxis)
                    .Map(dest => dest.Eccentricity, src => src.Eccentricity)
                    .Map(dest => dest.Inclination, src => src.Inclination)
                    .Map(dest => dest.ArgumentOfPeriapsis, src => src.ArgumentOfPeriapsis)
                    .Map(dest => dest.LongitudeOfAscendingNode, src => src.LongitudeOfAscendingNode)
                    .Map(dest => dest.MeanAnomalyZero, src => src.MeanAnomalyZero);

            config.ForType<CelestialObject, PhysicalData>()
                    .Map(dest => dest.Name, src => src.Name)
                    .Map(dest => dest.Radius, src => src.Radius)
                    .Map(dest => dest.Mass, src => src.Mass)
                    .Map(dest => dest.Type, src => src.CelestialObjectType)
                    .Map(dest => dest.Textures, src => src.TextureGroup)
                    .Map(dest => dest.Rings, src => src.RingSystem);

            config.ForType<CelestialObjectType, global::Data.Classes.CelestialObjectType>()
                    .MapWith(src => src.Name.ToEnum<global::Data.Classes.CelestialObjectType>().Value);

            config.ForType<TextureGroup, List<Core.Classes.Texture>>()
                    .MapWith(src => MapTextureGroupToListOfTexture(src, config));

            config.ForType<TextureQuality, Core.Enums.TextureQuality>()
                    .MapWith(src => src.Quality.ToEnum<Core.Enums.TextureQuality>().Value);

            config.ForType<TextureType, Core.Enums.TextureType>()
                    .MapWith(src => src.Type.ToEnum<Core.Enums.TextureType>().Value);

            config.ForType<CelestialObjectType, global::Data.Classes.CelestialObjectType>();

            config.ForType<CelestialObject, CelestialObject>()
                    .ShallowCopyForSameType(true);
            
            config.ForType<RingSystem, RingData>()
                    .Map(dest => dest.InnerRadius, src => src.InnerRadius)
                    .Map(dest => dest.OuterRadius, src => src.OuterRadius)
                    .Map(dest => dest.Textures, src => src.TextureGroup);
        }

        private List<Core.Classes.Texture> MapTextureGroupToListOfTexture(TextureGroup input, TypeAdapterConfig config)
        {
            List<Core.Classes.Texture> result = new List<Core.Classes.Texture>();
            if (input != null)
            {
                foreach (TextureGroupToTexture t in input.TextureGroupToTextures)
                {
                    Core.Classes.Texture texture = new Core.Classes.Texture
                    {
                        Path = PathHelper.SitePathCombine(t.Texture.TexturePath.Path, t.Texture.Name),
                        QualityEnum = t.Texture.TextureQuality.Adapt<Core.Enums.TextureQuality>(config),
                        TypeEnum = t.Texture.TextureType.Adapt<Core.Enums.TextureType>(config)
                    };
                    result.Add(texture);
                }
            }
            return result;
        }
    }
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
