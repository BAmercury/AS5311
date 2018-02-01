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
    public class AS3511_Chip
    {

        private ArduinoDriver.ArduinoDriver _driver;
        private byte _DataPin;
        private byte _ChipSelectPin;
        private byte _IndexPin;
        private byte _ClockPin;


        public AS3511_Chip(ArduinoDriver.ArduinoDriver driver, int DataPin, int ChipSelectPin, int IndexPin, int ClockPin)
        {

            _driver = driver;
            _DataPin = (byte)DataPin;
            _ChipSelectPin = (byte)ChipSelectPin;
            _IndexPin = (byte)IndexPin;
            _ClockPin = (byte)ClockPin;


        }

        public UInt32 encoder_position()
        {
            UInt32 value;
            value = encoder_value();
            return ((value * 2) / 4096);


        }

        public UInt32 encoder_value()
        {
            return (read_chip() >> 6);

        }


        public UInt32 read_chip()
        {
            UInt32 raw_value = 0;
            UInt16 inputstream = 0;
            UInt16 c;
            using (_driver)
            {
                _driver.Send(new DigitalWriteRequest(_ChipSelectPin, DigitalValue.High));
                _driver.Send(new DigitalWriteRequest(_ClockPin, DigitalValue.High));
                Thread.Sleep(100); //Will later replace with something more efficient
                _driver.Send(new DigitalWriteRequest(_ChipSelectPin, DigitalValue.Low));
                Thread.Sleep(10);
                _driver.Send(new DigitalWriteRequest(_ClockPin, DigitalValue.Low));
                Thread.Sleep(10);
                for (c = 0; c < 18; c++)
                {
                    _driver.Send(new DigitalWriteRequest(_ClockPin, DigitalValue.High));
                    Thread.Sleep(10);
                    inputstream = Convert.ToUInt16(_driver.Send(new DigitalReadRequest(_DataPin)));
                    raw_value = ((raw_value << 1) + inputstream);
                    _driver.Send(new DigitalWriteRequest(_ClockPin, DigitalValue.Low));
                    Thread.Sleep(10);
                }
            }

            return raw_value;

        }
    }
}