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
    }
}

