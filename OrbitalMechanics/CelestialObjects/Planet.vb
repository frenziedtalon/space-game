
Imports System.Windows.Media.Media3D

Namespace CelestialObjects
    Public Class Planet
        Inherits BaseCelestialObject

        Public Sub New(name As String,
                       mass As Integer,
                       texture As String,
                       position As Point3D,
                       motion As Vector3D)

            MyBase.New(name, mass, texture, position, motion)
        End Sub

        Public Overrides Sub Update()
            Throw New NotImplementedException()
        End Sub

    End Class
End Namespace