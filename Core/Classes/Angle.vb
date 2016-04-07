Namespace Classes
    Public Class Angle
        Private ReadOnly _radians As Double

        Private Sub New(radians As Double)
            If radians < 0 OrElse radians > 2 * Math.PI Then
                Throw New ArgumentOutOfRangeException(NameOf(radians), "Angle should be between 0 and 2*Math.Pi")
            End If
            _radians = radians
        End Sub

        Public ReadOnly Property Radians As Double
            Get
                Return _radians
            End Get
        End Property

        Public Shared Function FromRadians(angle As Double) As Angle
            Return New Angle(angle)
        End Function

    End Class
End Namespace