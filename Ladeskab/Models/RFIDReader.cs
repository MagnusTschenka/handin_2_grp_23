using System;
namespace Ladeskab
{
    public class RFIDReader
    {
        public RFIDReader()
        {
        }

        public void SetDoorStatus(bool newDoorStatus)
        {
            if (newDoorStatus != DoorLocked)
            {
                OnTempChanged(new DoorChangedEventArgs { DoorStatus = newDoorStatus });
                DoorLocked = newDoorStatus;
            }
        }

        protected virtual void OnTempChanged(DoorChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }
    }
}

