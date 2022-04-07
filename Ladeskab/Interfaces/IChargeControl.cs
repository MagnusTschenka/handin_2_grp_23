using System;
namespace Ladeskab.Interfaces
{

    public interface IChargeControl
    {
        public bool Connected();
        void StartCharge();
        void StopCharge();

      
    }
}

