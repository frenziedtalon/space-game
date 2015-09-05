
Imports SpaceGame.Classes.Messaging

Namespace Classes.BaseEntities

    ''' <summary>
    ''' Base class for all game objects
    ''' </summary>
    Public MustInherit Class BaseGameEntity

        ' every entity has a unique identifying number
        Private _id As Integer

        ' this is the next valid ID. Each time a BaseGameEntity is instantiated this is incremented
        Private _nextValidId As Integer = 0

        Private _lock As Object

        Private Sub SetId()
            SyncLock _lock
                _id = _nextValidId
                _nextValidId += 1
            End SyncLock
        End Sub

        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
        End Property


        Public Sub New()
            SetId()
        End Sub

        ''' <summary>
        ''' all entities must implement an update function
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub Update()

        ''' <summary>
        ''' all subclasses can communicate using messages
        ''' </summary>
        Public MustOverride Function HandleMessage(msg As Telegram) As Boolean

    End Class

End Namespace

