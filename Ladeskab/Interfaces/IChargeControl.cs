using System;
namespace Ladeskab.Interfaces
{
    public interface IChargeControl
    {
        public bool IsConnected { get; set; }

        void SimulatePhoneConnected(bool phone);
        void StartCharge();
        void StopCharge();
    }
}

