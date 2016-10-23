Namespace Classes
    Public Class DistanceRange
        Inherits Range
        Public Sub New(defaultLowerBound As Distance, defaultUpperBound As Distance)
            MyBase.New(defaultLowerBound.Kilometers, defaultUpperBound.Kilometers)
        End Sub

        Public Shadows Sub AddValue(value As Distance)
            MyBase.AddValue(value.Kilometers)
        End Sub

        Public Shadows ReadOnly Property LowerBound As Distance
            Get
                Return Distance.FromKilometers(GetLowerBound())
            End Get
        End Property

        Public Shadows ReadOnly Property UpperBound As Distance
            Get
                Return Distance.FromKilometers(GetUpperBound())
            End Get
        End Property
    End Class
End Namespace