using System;
namespace Ladeskab.Interfaces
{

    public class RFIDDetectedEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
    public interface IRFIDReader
    {
        event EventHandler<RFIDDetectedEventArgs> RfidEventDetected;

    }
}

