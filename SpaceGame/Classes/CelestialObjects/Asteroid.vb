Imports SpaceGame.Classes.BaseEntities
Imports SpaceGame.Classes.Messaging

Namespace Classes.CelestialObjects
    Public Class Asteroids
        Inherits BaseCelestialObject

        Public Overrides Sub Update()
            Throw New NotImplementedException()
        End Sub

        Public Overrides Function HandleMessage(msg As Telegram) As Boolean
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace