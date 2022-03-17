using System;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class ChargeControl : IChargeControl
    {
        public ChargeControl() 
        {
            usbCharger.CurrentValueEvent += handleNewCurrent;


        }

        private void handleNewCurrent(object? sender, CurrentEventArgs e)
        {
            NewCurrentDetected(e.Current);
        }

        private void NewCurrentDetected(double current)
        {
            if(current <= 0)
            {
                return;
            }
            else if(0< current && current <=5)
            {
                _display.PrintFullyCharged();
            }
            else if(5 < current && current <=500)
            {
                _display.PrintCurrentlyCharging();
            }
            else if(current > 500)
            {
                _usbCharger.StopCharge();
                _display.PrintOverChargedError();

            }
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

