Imports Core.Classes

Namespace CelestialObjects
    Public Interface ICelestialObject

        ReadOnly Property Mass As Mass

        ReadOnly Property Name As String

        ReadOnly Property Textures As List(Of Texture)

    End Interface
End Namespace