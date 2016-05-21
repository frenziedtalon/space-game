Imports Data

Public Class DataProvider
    Implements IDataProvider

    Private _solarSystem As SolarSystemData
    Public ReadOnly Property SolarSystem As ISolarSystemData Implements IDataProvider.SolarSystem
        Get
            If _solarSystem Is Nothing Then
                _solarSystem = New SolarSystemData()
            End If
            Return _solarSystem
        End Get
    End Property
End Class
