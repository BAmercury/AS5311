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
using System.Text.RegularExpressions;

namespace AS5311
{
    class Program
    {
        static void Main(string[] args)
        {

            // eww
            List<List<double>> data = new List<List<double>>();

            Console.WriteLine("Stating up Program");


            //Find Serial Port arduino is connected too
            string serialPortName = "COM9";
            var allPortNames = SerialPortStream.GetPortNames();
            var distinctPorts = allPortNames.Distinct().ToList();

            // If we don't specify a COM port, automagically select one if there is only a single match.
            if (distinctPorts.SingleOrDefault() != null)
            {
                serialPortName = distinctPorts.Single();
            }

            //Console.Write(serialPortName);


            // Upload HEX Uno file
            /*var uploader = new ArduinoSketchUploader(
                new ArduinoSketchUploaderOptions()
                {
                    FileName = @"C:\Users\Merc.MERCURY\Documents\mechanica\mechanica\mechanica_arduino\mechanica_arduino.ino.standard.hex",
                    PortName = serialPortName,
                    ArduinoModel = ArduinoModel.UnoR3
                }
                );

            uploader.UploadSketch();
            Console.WriteLine("Uploaded hex");
            */


                //Set up communications with Arduino
                SerialPortStream port = new SerialPortStream(serialPortName, 115200);
                port.Open();
                bool quick = true;
                while (quick)
                {

                    if (port.BytesToRead > 0)
                    {
                        string s = port.ReadLine();
                        s = Regex.Replace(s, @"\r", string.Empty);
                        Console.WriteLine("Connecting to Arduino");
                        if (s == "ready")
                        {
                            quick = false;
                            Console.WriteLine("Device ready");
                            break;
                        }

                    }
                  
                }
            Console.WriteLine("Want to start test?");
            string start_test = Console.ReadLine();
            Console.WriteLine("Input Desired Displacement Rate");
            string displacement_rate = Console.ReadLine();
            Console.WriteLine("Input Desired Distnace");
            string distance = Console.ReadLine();
            Console.WriteLine("Reset System Instead? ");
            string reset = Console.ReadLine();
            port.Write("<" + start_test + "," + displacement_rate + "," + distance + "," + reset + ">");
            if (reset == "1")
            {
                quick = false;
            }
            else if(start_test == "1")
            {
                quick = true;

            }
            while (quick)
            {

                    if (port.BytesToRead > 0)
                    {
                        string s = port.ReadLine();
                        if (s == "done")
                    {
                        quick = false;
                        break;
                    }
                        string[] message = s.Split(',');
                        List<double> temp = new List<double>();
                        //temp.Add(Convert.ToDouble(s));
                        foreach (string element in message)
                        {

                            //Console.WriteLine(element);

                            double value = Convert.ToDouble(element);
                            temp.Add(value);
                            Console.WriteLine(element);

                        }
                        data.Add(temp);

                    }




                }
            if (start_test == "1")
            {
                Console.WriteLine("Saving Data");
                using (TextWriter tw = new StreamWriter("SavedList.csv"))
                {
                    foreach (List<double> member in data)
                    {
                        //tw.Write(member);

                        foreach (double guy in member)
                        {
                            tw.Write(guy);
                            tw.Write(",");
                        }
                        tw.WriteLine();
                    }
                }

            }



        }
    }
}

