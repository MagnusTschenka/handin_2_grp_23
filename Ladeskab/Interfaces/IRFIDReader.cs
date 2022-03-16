using System;
namespace Ladeskab.Interfaces
{

    public class RFIDDetectedEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
    public interface IRFIDReader
    {
        public void SimulateRFIDCardApplied(int id_);
        event EventHandler<RFIDDetectedEventArgs> RfidEventDetected;

    }
}

