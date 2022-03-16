using System;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class RFIDReader : IRFIDReader
    {
        private int ID;
        public event EventHandler<RFIDDetectedEventArgs> RfidEventDetected;


        public RFIDReader()
        {
        }

        public void SetRFIDStatus(int id)
        {
             OnRFIDApplied(new RFIDDetectedEventArgs { Id = id });
        }

        protected virtual void OnRFIDApplied(RFIDDetectedEventArgs e)
        {
            RfidEventDetected?.Invoke(this, e);
        }

        public void SimulateRFIDCardApplied(int id_)
        {
            SetRFIDStatus(id_);
        }

    }
}

