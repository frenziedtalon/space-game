Namespace Classes
    Public Class Range

        Private ReadOnly _values As New List(Of Double)
        Private _calculated As Boolean

        Public Sub New(defaultLowerBound As Double, defaultUpperBound As Double)
            If defaultLowerBound > defaultUpperBound Then
                Throw New ArgumentOutOfRangeException(String.Format("{0} should be greater than {1}", NameOf(defaultUpperBound), NameOf(defaultLowerBound)))
            End If
            _lowerBound = defaultLowerBound
            _upperBound = defaultUpperBound
            _calculated = True
        End Sub

        Public Sub AddValue(value As Double)
            _values.Add(value)
            _calculated = False
        End Sub

        Private _lowerBound As Double
        Public ReadOnly Property LowerBound As Double
            Get
                Return GetLowerBound()
            End Get
        End Property

        Protected Function GetLowerBound() As Double
            If Not _calculated Then
                CalculateValues()
            End If
            Return _lowerBound
        End Function

        Private _upperBound As Double
        Public ReadOnly Property UpperBound As Double
            Get
                Return GetUpperBound()
            End Get
        End Property

        Protected Function GetUpperBound() As Double
            If Not _calculated Then
                CalculateValues()
            End If
            Return _upperBound
        End Function

        Private Sub CalculateValues()
            _values.Sort()
            _lowerBound = _values(0)
            _upperBound = _values(_values.Count - 1)
            _calculated = True
        End Sub
    End Class
End Namespace