Imports Core.Classes

Namespace Data
    Public Class PhysicalData
        Implements IPhysicalData

        Sub New(name As String,
                radius As Distance,
                mass As Mass)
            Me.Radius = radius
            Me.Name = name
            Me.Mass = mass
        End Sub

        Public Property Name As String Implements IPhysicalData.Name
        Public Property Mass As Mass Implements IPhysicalData.Mass
        Public Property Radius As Distance Implements IPhysicalData.Radius
    End Class
End Namespace