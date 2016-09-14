Imports Core.Classes
Imports Mapster

Public Class Mappings
    Implements IRegister

    Public Sub Register(config As TypeAdapterConfig) Implements IRegister.Register
        config.ForType(Of Integer, Mass).MapWith(Function(src) Mass.FromKilograms(src))
        config.ForType(Of Double, Mass).MapWith(Function(src) Mass.FromKilograms(src))

        config.ForType(Of Integer, Distance).MapWith(Function(src) Distance.FromKilometers(src))
        config.ForType(Of Integer?, Distance).MapWith(Function(src) If(src.HasValue, Distance.FromKilometers(src.Value), Nothing))
        config.ForType(Of Double, Distance).MapWith(Function(src) Distance.FromKilometers(src))
        config.ForType(Of Double?, Distance).MapWith(Function(src) If(src.HasValue, Distance.FromKilometers(src.Value), Nothing))

        config.ForType(Of Textures, Textures)
    End Sub
End Class
