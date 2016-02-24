Imports System.Windows.Media.Media3D

Namespace Classes
    Public Interface IOrbit
        ReadOnly Property Angle As Double
        ReadOnly Property Position As Point3D
        ReadOnly Property Radius As Integer
        ReadOnly Property Speed As Double
        Sub Update()
    End Interface
End Namespace
