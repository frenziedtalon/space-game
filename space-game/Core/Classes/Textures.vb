Namespace Classes
    Public Class Textures
        Implements IEquatable(Of Textures)

        Public High As String
        Public Medium As String
        Public Low As String

        Public Shadows Function Equals(other As Textures) As Boolean Implements IEquatable(Of Textures).Equals
            If other IsNot Nothing Then
                Return High.Equals(other.High) AndAlso Medium.Equals(other.Medium) AndAlso Low.Equals(other.Low)
            End If
            Return False
        End Function
    End Class
End Namespace