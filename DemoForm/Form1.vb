Imports System.Windows.Media.Media3D
Imports OrbitalMechanics.CelestialObjects

Public Class MainForm
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initialise()
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub pnlViewPort_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlViewPort.MouseDown

    End Sub

    Private Sub pnlViewPort_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlViewPort.MouseUp

    End Sub


    Private SolarSystem As List(Of ICelestialObject)
    Private Sub Initialise()

        ' create all the objects with their positions and speeds
        SolarSystem = New List(Of ICelestialObject)

        Dim sol As New Star("Sol", 100000, 5000, "sol.jpg", New Point3D(0, 0, 0), New Vector3D(0, 0, 0), 100)

        SolarSystem.Add(sol)



    End Sub
End Class
