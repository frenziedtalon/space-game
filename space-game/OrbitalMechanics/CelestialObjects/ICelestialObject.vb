Imports Core.Classes

Namespace CelestialObjects
    Public Interface ICelestialObject

        ReadOnly Property Mass As Mass

        ReadOnly Property Name As String

        ReadOnly Property Texture As Textures

    End Interface
End Namespace