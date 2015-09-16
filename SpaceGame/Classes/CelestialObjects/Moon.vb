Imports Messaging
Imports SpaceGame.Classes.BaseEntities

Namespace Classes.CelestialObjects
    Public Class Moon
        Inherits BaseCelestialObject

        Public Sub New(name As String, mass As Integer)
            MyBase.New(name, mass)
        End Sub

        Public Overrides Sub Update()
            Throw New NotImplementedException()
        End Sub

    End Class
End Namespace