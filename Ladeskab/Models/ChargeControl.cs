using System;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class ChargeControl : IChargeControl
    {
        public ChargeControl() 
        {
        }

        public bool Connected { get; set; }


       public void SimulatePhoneConnected(bool phone)
        {
            Connected = phone;
        }
        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

    
    }
}

