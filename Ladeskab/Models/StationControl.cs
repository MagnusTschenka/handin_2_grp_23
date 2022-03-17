using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;


namespace Ladeskab
{
    public class StationControl
    {
        StationControl(IDoor _door, IRFIDReader _Rfid)
        {
            _door.DoorChangedEvent += HandleDoorStatusChangedEvent;
            _Rfid.RfidEventDetected += HandleRfidDetected;

        }

        private void HandleRfidDetected(object? sender, RFIDDetectedEventArgs e)
        {
            RfidDetected(e.Id);
        }

        private void HandleDoorStatusChangedEvent(object? sender, DoorChangedEventArgs e)
        {
            DoorChangedDetected(e.DoorStatus);

        }

        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor

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
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

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
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

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
            if (!DoorStatus) //DoorClosed = true, DoorOpened = False
            {
                if (LadeskabState.Available == _state)
                {
                    _state = LadeskabState.DoorOpen;
                    //kald display metode der printer
                }

            }
            else if (DoorStatus)
            {
                if (LadeskabState.DoorOpen == _state)
                {
                    _state = LadeskabState.Available;
                    //kald display metode der printer at dør luk
                }
            }
        }
        // Her mangler de andre trigger handlere
    }
}
