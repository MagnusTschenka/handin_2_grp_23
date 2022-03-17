using System;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class Display : IDisplay
    {
        public Display()
        {

        }

        public void PrintConnectionError()
        {
            Console.WriteLine("Tilslutningsfejl");
        }

        public void PrintConnectPhone()
        {
            Console.WriteLine("Tilsut telefon");
        }

        public void PrintCurrentlyCharging()
        {
            Console.WriteLine("Telefonen er igang med at oplade");
        }

        public void PrintFullyCharged()
        {
            Console.WriteLine("Telefon er fuldt opladet og kan nu fjernes");
        }

        public void PrintLoadRFID()
        {
            Console.WriteLine("Indlæs RFID");

        }

        public void PrintOccupied()
        {
            Console.WriteLine("Ladeskab optaget");
        }

        public void PrintOverchargeError()
        {
            Console.WriteLine("Teknisk fejl, fjern venligst telefon!");
        }

        public void PrintRemovedPhone()
        {
            Console.WriteLine("Fjern telefon");
        }

        public void PrintRFIDError()
        {
            Console.WriteLine("RFID fejl");
        }
    }
}

