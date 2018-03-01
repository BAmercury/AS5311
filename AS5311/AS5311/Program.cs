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
using System.IO;
using RJCP.IO.Ports;
namespace AS5311
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Stating up Program");
            Console.WriteLine("Connecting to Arduino");

            //Find Serial Port arduino is connected too
            string serialPortName = "";
            var allPortNames = SerialPortStream.GetPortNames();
            var distinctPorts = allPortNames.Distinct().ToList();

            // If we don't specify a COM port, automagically select one if there is only a single match.
            if (distinctPorts.SingleOrDefault() != null)
            {
                serialPortName = distinctPorts.Single();
            }

            //Console.Write(serialPortName);


            // Upload HEX Uno file
            var uploader = new ArduinoSketchUploader(
                new ArduinoSketchUploaderOptions()
                {
                    FileName = @"C:\Users\Merc.MERCURY\Documents\AS5311\examples\encoder_lib\encoder_lib\encoder_lib.ino.standard.hex",
                    PortName = serialPortName,
                    ArduinoModel = ArduinoModel.UnoR3
                }
                );

            uploader.UploadSketch();

            Console.WriteLine("Press ESC to stop");

            do
            {

                //Set up communications with Arduino
                SerialPortStream port = new SerialPortStream(serialPortName, 115200);
                port.Open();
                while (!Console.KeyAvailable)
                {
                    string s = port.ReadLine();
                    Console.WriteLine(s);


                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);



        }
    }
}

