﻿Imports Messaging
Imports SpaceGame.Classes.BaseEntities

Namespace Classes.CelestialObjects
    Public Class Planet
        Inherits BaseCelestialObject

        Public Sub New(name As String, mass As Integer)
            MyBase.New(name, mass)
        End Sub

        Public Overrides Sub Update()
            Throw New NotImplementedException()
        End Sub

        Public Overrides Function HandleMessage(msg As Telegram) As Boolean
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace