Imports Messaging

''' <summary>
''' Base class for all game objects
''' </summary>
Public MustInherit Class BaseGameEntity

    ' every entity has a unique identifier
    Private ReadOnly _id As Guid
    Protected ReadOnly _entityManager As IEntityManager

    Public ReadOnly Property Id As Guid
        Get
            Return _id
        End Get
    End Property


    Protected Sub New(entityManager As IEntityManager)

        If entityManager Is Nothing Then
            Throw New ArgumentNullException(NameOf(entityManager))
        End If

        _id = Guid.NewGuid()
        _entityManager = entityManager
        _entityManager.RegisterEntity(Me)
    End Sub

    ''' <summary>
    ''' All entities must implement an update function
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub Update()

    ''' <summary>
    ''' All subclasses can communicate using messages
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

    ''' <summary>
    ''' Whether the entity can be used as a camera target
    ''' </summary>
    Public Overridable Property CameraTarget As Boolean = True

End Class