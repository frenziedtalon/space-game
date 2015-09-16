Imports Messaging

Namespace Classes.BaseEntities

    Public Interface IState(Of T)

        ''' <summary>
        ''' This will execute when the state is entered
        ''' </summary>
        Sub State_Enter(ByRef p As T)

        ''' <summary>
        ''' This is the state's normal update function
        ''' </summary>
        Sub State_Execute(ByRef p As T)

        ''' <summary>
        ''' This will execute when the state is exited
        ''' </summary>
        Sub State_Exit(ByRef p As T)

        ''' <summary>
        ''' This executes if the agent receives a message from the message dispatcher
        ''' </summary>
        Function OnMessage(ByRef p As T, msg As Telegram) As Boolean

    End Interface
End Namespace