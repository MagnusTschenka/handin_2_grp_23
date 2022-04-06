using System;
namespace Ladeskab.Interfaces
{
    public interface IDisplay
    {
        public void PrintConnectPhone();
        public void PrintConnectionError();
        public void PrintLoadRFID();
        public void PrintRFIDError();
        public void PrintOccupied();
        public void PrintRemovedPhone();
        public void PrintFullyCharged();
        public void PrintCurrentlyCharging();
        public void PrintOverchargeError();
        public void PrintLockedLocker();
        public void PrintPhoneConnectionError();
        public void PrintTakePhoneShutDoor();
    }
}

