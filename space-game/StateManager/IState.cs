using Messaging;

namespace StateManager
{
    public interface IState<T>
    {
        /// <summary>
        /// This will execute when the state is entered
        /// </summary>
        void State_Enter(ref T p);

        /// <summary>
        /// This is the state's normal update function
        /// </summary>
        void State_Execute(ref T p);

        /// <summary>
        /// This will execute when the state is exited
        /// </summary>
        void State_Exit(ref T p);

        /// <summary>
        /// This executes if the agent receives a message from the message dispatcher
        /// </summary>
        bool OnMessage(ref T p, Telegram msg);
    }
}
