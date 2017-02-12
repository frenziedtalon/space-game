Imports Core.Classes
Imports Core.Extensions

Namespace Classes
    Public Class RingData
        Implements IEquatable(Of RingData)

        Public Property InnerRadius As Distance
        Public Property OuterRadius As Distance
        Public Property Textures As List(Of Texture)

        Public Shadows Function Equals(other As RingData) As Boolean Implements IEquatable(Of RingData).Equals
            If other IsNot Nothing Then
                Return InnerRadius.Equals(other.InnerRadius) AndAlso 
                    OuterRadius.Equals(other.OuterRadius) AndAlso 
                    Textures.IsEquivalent(other.Textures)
            End If
            Return False
        End Function
    End Class
End Namespace