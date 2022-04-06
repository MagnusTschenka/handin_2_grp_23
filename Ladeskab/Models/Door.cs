using Ladeskab.Interfaces;
using System;
namespace Ladeskab
{
    public class Door : IDoor
    {
        public event EventHandler<DoorChangedEventArgs>? DoorChangedEvent;
        private bool oldDoorStatus;
        private bool isDoorLocked; //use to check in station control
        public void SetDoorStatus(bool newDoorStatus)
        {
            if (newDoorStatus != oldDoorStatus)
            {
                OnDoorChanged(new DoorChangedEventArgs { DoorStatus = newDoorStatus });
                oldDoorStatus = newDoorStatus;
            }
        }

        protected virtual void OnDoorChanged(DoorChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }

        public void simulateDoorBeingOpened()
        {
            SetDoorStatus(true);
        }

        public void simulateDoorBeingClosed()
        {
            SetDoorStatus(false);
        }

        public void LockDoor()
        {
            isDoorLocked = true;

        }

        public void UnlockDoor()
        {
            isDoorLocked = false;

        }

        public bool GetDoorLockedStatus()
        {
            return isDoorLocked;
        }
        
        public bool GetDoorStatus()
        {
            return oldDoorStatus;
        }
    }
}

