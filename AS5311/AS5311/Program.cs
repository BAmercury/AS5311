using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoDriver;
using ArduinoDriver.SerialProtocol;
using ArduinoUploader.Hardware;
using ArduinoUploader;

namespace AS5311
{
    class Program
    {
        static void Main(string[] args)
        {
            const ArduinoModel Arduino = ArduinoModel.UnoR3;

            const int DataPin = 4;
            const int ClockPin = 3;
            const int ChipSelectPin = 2;
            const int IndexPin = 1;


            using (var driver = new ArduinoDriver.ArduinoDriver(Arduino, true))
            {

                driver.Send(new PinModeRequest(DataPin, PinMode.Input));
                driver.Send(new PinModeRequest(ClockPin, PinMode.Output));
                driver.Send(new PinModeRequest(ChipSelectPin, PinMode.Output));
                driver.Send(new PinModeRequest(IndexPin, PinMode.Input));


                while (true)
                {
                    AS3511_Chip chip = new AS3511_Chip(driver,DataPin,ChipSelectPin,IndexPin,ClockPin));

                    long value;
                    value = chip.encoder_value();
                    Console.Write("Measured: ");
                    Console.WriteLine(value);

                    long pos;
                    pos = chip.encoder_position
                    

                }

            }


   
        }
    }
}

