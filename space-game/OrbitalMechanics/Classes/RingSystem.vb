Imports Core.Classes

Namespace Classes
    Public Class RingSystem
        Public Property InnerRadius() As Distance
        Public Property OuterRadius() As Distance
        Public Property Textures() As List(Of Texture)

        Public Sub New(innerRadius As Distance, outerRadius As Distance, textures As List(Of Texture))
            Me.InnerRadius = innerRadius
            Me.OuterRadius = outerRadius
            Me.Textures = textures
        End Sub
    End Class
End NameSpace