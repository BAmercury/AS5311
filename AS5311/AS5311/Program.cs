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
            double oldpos = 0;
            double rev = 0;
            double offset = 0;
            double count = 0;
            bool offsetInitialised = false;
            double encoderCount;

            Console.WriteLine("Stating up Program");
            Console.WriteLine("Connecting to Arduino");
            using (var driver = new ArduinoDriver.ArduinoDriver(Arduino, true))
            {
                AS3511_Chip chip = new AS3511_Chip(driver, DataPin, ChipSelectPin, IndexPin, ClockPin);
                Console.WriteLine("Press ESC to stop");
                //List<double> parts = new List<double>();
                do
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\Merc.MERCURY\Desktop\beep-01a.wav");
                    
                    while (!Console.KeyAvailable)
                    {
                        //long value;
                        //driver.Send(new DigitalWriteRequest(ChipSelectPin, DigitalValue.Low));
                        pos = chip.encoder_value();
                        //Console.Write("Measured: ");
                        //Console.WriteLine(pos);
                        //Console.Write("Old Pos: ");
                        //Console.WriteLine(oldpos);
                        //UInt32 value = 434;
                        //Console.Write("Measured: ");
                        //Console.WriteLine(value);

                        //long pos;
                        //pos = ((((double)value * 2) / 4096) / 10);
                        //UInt32 pos = chip.encoder_position(value);
                        //Console.Write("Pos: ");
                        //Console.WriteLine(pos);
                        if ((pos - oldpos) > 2048)
                        {
                            rev -= 1;
                        }
                        else if ((oldpos - pos) > 2048)
                        {
                            rev += 1;
                        }

                        oldpos = pos;

                        if (offsetInitialised == false)
                        {
                            offset = -pos;
                            offsetInitialised = true;

                        }
                        Console.Write("revolutions: ");
                        Console.WriteLine(rev);
                        //Console.Write("Revolutions: ");
                        //Console.WriteLine(rev);
                        //encoderCount = (rev * 4095) + (pos + offset);
                        //encoderCount = ((encoderCount &~((long)3 << 22));
                        //encoderCount = ((encoderCount & ~((long)3 << 22)) | ((long)magStrength << 22)); //clear the upper two bits of the third byte, insert the two-bit magStrength vale
                        // value = ((((pos + offset) * 2) / 4096) / 10);
                        //Console.Write("Count:  ");
                        //Console.WriteLine(encoderCount);
                        //player.Play();



                        //parts.Add(pos);
                        //Thread.Sleep(100); // again will do something better later on
                    }
                }
                while (Console.ReadKey(true).Key != ConsoleKey.Escape);

                //using (TextWriter tw = new StreamWriter("SavedList.txt"))
                //{
                //    foreach (double s in parts)
                //        tw.WriteLine(s);
                //}








            }


   
        }
    }
}

