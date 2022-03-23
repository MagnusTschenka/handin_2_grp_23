using System;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _usbCharger;
        private IDisplay _display;
        public bool Connected { get; set; }

        public ChargeControl(IUsbCharger usbCharger, IDisplay display) 
        {
            _usbCharger = usbCharger;
            _display = display;
            usbCharger.CurrentValueEvent += HandleNewCurrent;


        }

        private void HandleNewCurrent(object? sender, CurrentEventArgs e)
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
                _display.PrintOverchargeError();

            }
        }




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

