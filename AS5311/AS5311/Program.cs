using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoDriver;
using ArduinoDriver.SerialProtocol;
using ArduinoUploader.Hardware;
using ArduinoUploader;
using System.Threading;

namespace AS5311
{
    class Program
    {
        static void Main(string[] args)
        {
            const ArduinoModel Arduino = ArduinoModel.UnoR3;

            const int DataPin = 2;
            const int ClockPin = 3;
            const int ChipSelectPin = 4;
            const int IndexPin = 1;
            double pos = 0;

            Console.WriteLine("Stating up Program");
            Console.WriteLine("Connecting to Arduino");
            using (var driver = new ArduinoDriver.ArduinoDriver(Arduino, true))
            {
                AS3511_Chip chip = new AS3511_Chip(driver, DataPin, ChipSelectPin, IndexPin, ClockPin);
                Console.WriteLine("Here");
                while (true)
                {


                    //long value;
                    UInt32 value = chip.encoder_value();
                    //UInt32 value = 434;
                    Console.Write("Measured: ");
                    Console.WriteLine(value);

                    //long pos;
                    pos = pos + ( ((double)value) *  (2/ 4096) );
                    //UInt32 pos = chip.encoder_position(value);
                    Console.Write("Pos: ");
                    Console.WriteLine(pos);

                    Thread.Sleep(100); // again will do something better later on


                    

                }

            }


   
        }
    }
}

