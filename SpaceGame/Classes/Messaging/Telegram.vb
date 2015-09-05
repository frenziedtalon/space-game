
Namespace Classes.Messaging

    Public Class Telegram
        Private _sender As Integer
        Private _receiver As Integer
        Private _msg As MessageType
        Private _extraInfo As Object
        Private _dispatchTurn As Integer

        ' the entity that sent this telegram
        Public ReadOnly Property Sender As Integer
            Get
                Return _sender
            End Get
        End Property

        ' the entity that is to receive this telegram
        Public ReadOnly Property Receiver As Integer
            Get
                Return _receiver
            End Get
        End Property

        ' the message itself
        Public ReadOnly Property Msg As Integer
            Get
                Return _msg
            End Get
        End Property

        ' messages can be dispatched immediately or delayed for a specified number of turns
        Public ReadOnly Property DispatchTurn As Integer
            Get
                Return _dispatchTurn
            End Get
        End Property

        ' any additional information that may accompany the message
        Public ReadOnly Property ExtraInfo As Object
            Get
                Return _extraInfo
            End Get
        End Property

        Public Sub New(sender As Integer,
                       receiver As Integer,
                       msg As MessageType,
                       delayForTurns As Integer,
                       Optional extraInfo As Object = Nothing)

            _sender = sender
            _receiver = receiver
            _msg = msg
            ' TODO: identify current turn number
            _dispatchTurn = delayForTurns ' + current turn number
            _extraInfo = extraInfo

        End Sub
    End Class
End Namespace

