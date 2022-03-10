using Ladeskab.Interfaces;
using System;
namespace Ladeskab
{
    public class Door : IDoor
    {
        public event EventHandler<DoorChangedEventArgs> DoorChangedEvent;
        private bool OldDoorStatus;
        public void SetDoorStatus(bool newDoorStatus)
        {
            if (newDoorStatus != OldDoorStatus)
            {
                OnTempChanged(new DoorChangedEventArgs { DoorStatus = newDoorStatus });
                OldDoorStatus = newDoorStatus;
            }
        }

        protected virtual void OnTempChanged(DoorChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }

        public void LockDoor()
        {
            throw new NotImplementedException();
        }

        public void UnlockDoor()
        {
            throw new NotImplementedException();
        }
    }
}

