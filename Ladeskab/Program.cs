using Ladeskab.Models;

namespace Ladeskab
{

    class Program
    {

        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            Door _Door = new Door();
            RFIDReader _RFIDReader = new RFIDReader();
            Display _display = new Display();
            UsbChargerSimulator usbChargerSimulator = new UsbChargerSimulator();
            ChargeControl chargeControl = new ChargeControl(usbChargerSimulator, _display);
            LogFile logfile = new LogFile();
            StationControl stationControl = new StationControl(_Door, _RFIDReader,_display, chargeControl, logfile);

            usbChargerSimulator.SimulateConnected(true);
            //stationControl.SetLadeskabsState(StationControl.LadeskabState.DoorOpen);

            bool finish = false;
            do
            {
                System.Console.WriteLine("Indtast E, O, C, R: ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        _Door.simulateDoorBeingOpened();
                        break;

                    case 'C':
                        _Door.simulateDoorBeingClosed();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        _RFIDReader.SetRFIDStatus(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }



}