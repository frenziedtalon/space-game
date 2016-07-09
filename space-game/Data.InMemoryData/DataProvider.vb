Imports Data
Imports Data.Classes

Public Class DataProvider
    Implements IDataProvider

    Private _solarSystem As List(Of CelestialObjectData)

    Public ReadOnly Property SolarSystem As List(Of CelestialObjectData) Implements IDataProvider.SolarSystem
        Get
            If _solarSystem Is Nothing Then
                _solarSystem = New SolarSystemData().SolSystem()
            End If
            Return _solarSystem
        End Get
    End Property
End Class
