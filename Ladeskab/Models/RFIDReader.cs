using System;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class RFIDReader : IRFIDReader
    {
       
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


        // ::   Denne bruges ikke   :: 

        //public void SimulateRFIDCardApplied(int id_)
        //{
        //    SetRFIDStatus(id_);
        //}

    }
}

