using System;
namespace Ladeskab.Interfaces
{

    public class DoorChangedEventArgs : EventArgs
    {
        public bool DoorStatus { get; set; }
    }
    public interface IDoor
    {
        void simulateDoorBeingClosed();
        void simulateDoorBeingOpened();
        void LockDoor();
        void UnlockDoor();

        event EventHandler<DoorChangedEventArgs> DoorChangedEvent;
    }
}

