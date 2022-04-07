using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;


namespace Ladeskab
{
    public class StationControl
    {

        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState  
        {
            Available,
            Locked,
            DoorOpen
        };
        

        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _displayerDisplay;
        private IRFIDReader _Rfid;
        private ILogFile _logFile;


        public StationControl(IDoor door, IRFIDReader Rfid, IDisplay display, IChargeControl chargeControl, ILogFile logFile)
        {
            _door = door;
            _Rfid = Rfid;
            _door.DoorChangedEvent += HandleDoorStatusChangedEvent;
            _Rfid.RfidEventDetected += HandleRfidDetected;
            _charger = chargeControl;
            _displayerDisplay = display;
            _state = LadeskabState.Available;
            _logFile = logFile;

        }

        private void HandleRfidDetected(object? sender, RFIDDetectedEventArgs e)
        {
            RfidDetected(e.Id);
        }

        private void HandleDoorStatusChangedEvent(object? sender, DoorChangedEventArgs e)
        {
            DoorChangedDetected(e.DoorStatus);

        }

      

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        _logFile.AppendTextLock(id);
                        _displayerDisplay.PrintLockedLocker();
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _displayerDisplay.PrintPhoneConnectionError();
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _logFile.AppendTextUnlock(id);

                        _displayerDisplay.PrintTakePhoneShutDoor();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _displayerDisplay.PrintRFIDError();
                    }

                    break;
            }
        }

        private void DoorChangedDetected(bool DoorStatus)
        {
            switch (DoorStatus)
            {
                case false:
                    if (LadeskabState.Available == _state)
                    {
                        _state = LadeskabState.DoorOpen;
                        _displayerDisplay.PrintConnectPhone();
                    }
                    break;
                case true:
                    if (LadeskabState.DoorOpen == _state)
                    {
                        _state = LadeskabState.Available;
                        _displayerDisplay.PrintLoadRFID();
                    }
                    break;
                
            }
           
        }
        
    }
}
