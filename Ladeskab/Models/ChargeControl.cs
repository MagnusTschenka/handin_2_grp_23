using System;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class ChargeControl : IChargeControl
    {
        public ChargeControl()
        {
        }

        public bool IsConnected { get; set; }

       public void SimulatePhoneConnected(bool phone)
        {
            IsConnected = phone;
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

