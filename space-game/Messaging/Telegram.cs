namespace Messaging
{
    public class Telegram
    {
        private int _sender;
        private int _receiver;
        private MessageType _msg;
        private object _extraInfo;

        private int _dispatchTurn;
        // the entity that sent this telegram
        public int Sender
        {
            get { return _sender; }
        }

        // the entity that is to receive this telegram
        public int Receiver
        {
            get { return _receiver; }
        }

        // the message itself
        public MessageType Msg
        {
            get { return _msg; }
        }

        // messages can be dispatched immediately or delayed for a specified number of turns
        public int DispatchTurn
        {
            get { return _dispatchTurn; }
        }

        // any additional information that may accompany the message
        public object ExtraInfo
        {
            get { return _extraInfo; }
        }


        public Telegram(int sender, int receiver, MessageType msg, int delayForTurns, object extraInfo = null)
        {
            _sender = sender;
            _receiver = receiver;
            _msg = msg;
            // TODO: identify current turn number
            _dispatchTurn = delayForTurns;
            // + current turn number
            _extraInfo = extraInfo;

        }
    }
}
