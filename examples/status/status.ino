#include "AS5311.h"


AS5311 myAS5311(2,3,4,1); // data, clock, chip select, index
long pos = 0;
long oldpos = 0;
long rev = 0;
long offset = 0;
bool offset_t = false;

void setup()
{
  Serial.begin(115200);
}
void loop()
{
  pos = myAS5311.encoder_value();

  if ( (pos - oldpos) > 2048)
  {
    rev -= 1;
  }
  else if ( (oldpos - pos) > 2048)
  {
    rev += 1;
  }

  oldpos = pos;

  if (offset_t == false)
  {
    offset = -pos;
    offset_t = true;
    
  }

  Serial.print("Rev: ");
  Serial.println(rev);

  //long count = (rev * 4092) + (pos + offset);
  //count = (count &~((long)3 << 22));
  //Serial.print("Count: ");
  //Serial.println(count);

  //delay(2000);
}
