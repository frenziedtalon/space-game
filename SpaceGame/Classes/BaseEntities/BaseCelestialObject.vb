

Namespace Classes.BaseEntities

    Public MustInherit Class BaseCelestialObject
        Inherits BaseGameEntity
        Implements ICelestialObject

        Private ReadOnly _mass As Integer

        Public Sub New(mass As Integer)
            _mass = mass
        End Sub

        Public ReadOnly Property Mass As Integer Implements ICelestialObject.Mass
            Get
                Return _mass
            End Get
        End Property
    End Class
End Namespace