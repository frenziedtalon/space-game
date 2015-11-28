Imports Messaging

''' <summary>
''' Base class for all game objects
''' </summary>
Public MustInherit Class BaseGameEntity

    ' every entity has a unique identifier
    Private ReadOnly _id As Guid

    Public ReadOnly Property Id As Guid
        Get
            Return _id
        End Get
    End Property


    Protected Sub New()
        _id = Guid.NewGuid()
        EntityManager.Instance.RegisterEntity(Me)
    End Sub

    ''' <summary>
    ''' all entities must implement an update function
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub Update()

    ''' <summary>
    ''' all subclasses can communicate using messages
    ''' </summary>
    Public Overridable Function HandleMessage(msg As Telegram) As Boolean
        Throw New NotImplementedException
    End Function

    ''' <summary>
    ''' Type of the entity
    ''' </summary>
    Public ReadOnly Property Type As String
        Get
            Return Me.GetType().ToString()
        End Get
    End Property

End Class