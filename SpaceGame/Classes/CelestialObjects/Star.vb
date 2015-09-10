Imports SpaceGame.Classes.BaseEntities
Imports SpaceGame.Classes.Messaging

Namespace Classes.CelestialObjects
    Public Class Star
        Inherits BaseCelestialObject

        Private ReadOnly _surfaceTemperatureK As Integer

        Public Sub New(mass As Integer, surfaceTemperatureK As Integer)
            MyBase.New(mass)
            _surfaceTemperatureK = surfaceTemperatureK
        End Sub

        Public Overrides Sub Update()
            Throw New NotImplementedException()
        End Sub

        Public Overrides Function HandleMessage(msg As Telegram) As Boolean
            Throw New NotImplementedException()
        End Function

        Private _classification As StarClassification


        Public ReadOnly Property Classification() As StarClassification
            Get
                Select Case SurfaceTemperatureK
                    Case >= 33000
                        Return StarClassification.O
                    Case >= 10500
                        Return StarClassification.B
                    Case >= 7500
                        Return StarClassification.A
                    Case >= 6000
                        Return StarClassification.F
                    Case >= 5500
                        Return StarClassification.G
                    Case >= 4000
                        Return StarClassification.K
                    Case Else
                        Return StarClassification.M
                End Select
            End Get
        End Property

        Public ReadOnly Property SurfaceTemperatureK() As Integer
            Get
                Return _surfaceTemperatureK
            End Get
        End Property
    End Class


    Public Enum StarClassification
        O
        B
        A
        F
        G
        K
        M
    End Enum
End Namespace