#include "AS5311.h"

<<<<<<< HEAD
AS5311 myAS5311(4,3,2,1); // data, clock, chip select, index
=======
AS5311 myAS5311(2,3,4,1); // data, clock, chip select, index
long pos = 0;
>>>>>>> ff4a015bec9fd18313fbbb0727dbba27a39ad8c6

void setup()
{
  Serial.begin(9600);
}

void loop()
{
  long value;
  value = myAS5311.encoder_value();
  Serial.print("measured value: ");
  Serial.println(value);
  pos = myAS5311.encoder_position();
  Serial.print("measured position: ");
  Serial.println(pos);
//  if (myAS5311.encoder_error())
//  {
//    Serial.println("error detected.");
//    if (myAS5311.err_value.DECn) Serial.println("DECn error");
//    if (myAS5311.err_value.INCn) Serial.println("INCn error");
//    if (myAS5311.err_value.COF) Serial.println("COF error");
//    if (myAS5311.err_value.OCF) Serial.println("OCF error");
//    if (myAS5311.err_value.LIN) Serial.println("LIN error");
//  }
  delay(2000);
}
