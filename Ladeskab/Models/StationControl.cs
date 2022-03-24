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
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };
        
        public void SetLadeskabsState(LadeskabState state)
        {
            _state= state;
        }

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _displayerDisplay;
        private IRFIDReader _Rfid;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door, IRFIDReader Rfid, IDisplay display, IChargeControl chargeControl)
        {
            _door = door;
            _Rfid = Rfid;
            _door.DoorChangedEvent += HandleDoorStatusChangedEvent;
            _Rfid.RfidEventDetected += HandleRfidDetected;
            _charger = chargeControl;
            _displayerDisplay = display;
            _state = LadeskabState.Available;

        }

        private void HandleRfidDetected(object? sender, RFIDDetectedEventArgs e)
        {
            RfidDetected(e.Id);
        }

        private void HandleDoorStatusChangedEvent(object? sender, DoorChangedEventArgs e)
        {
            DoorChangedDetected(e.DoorStatus);

        }

        public void AppendTextLock(int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm") + ": Skab låst med RFID: {0}", id);
            }
        }

        public void AppendTextUnlock(int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm") + ": Skab låst op med RFID: {0}", id);
            }
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        AppendTextLock(id);

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
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
                        AppendTextUnlock(id);

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
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
                default:
                    break;
            }
           
        }
        
    }
}
