using ConsolePixie.Image;
using ConsolePixie.Input;
using System.Drawing;

namespace ConsolePixie
{
    internal class Program
    {
        protected static States currentState;
        protected static States lastState = States.Start;
        public static Action? stateUpdate;
        public static States CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                stateUpdate?.Invoke();
            }
        }
        public static States LastState
        {
            get { return lastState; }
            set { lastState = value; }
        }
        
        static void Main(string[] args)
        {
            ConsoleInput consoleInput = new();
            CurrentState = States.Start;
        }
    }
}
