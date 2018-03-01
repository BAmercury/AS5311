#include <Encoder.h>

// Change these pin numbers to the pins connected to your encoder.
//   Best Performance: both pins have interrupt capability
//   Good Performance: only the first pin has interrupt capability
//   Low Performance:  neither pin has interrupt capability
//   avoid using pins with LEDs attached

//int index_pin = 2;
int chip_select = 4;
int pinA = 2;
int pinB = 3;

Encoder der(3,2);

void setup() {
  Serial.begin(115200);
  pinMode(chip_select, OUTPUT);
  digitalWrite(chip_select, LOW);

  delay(50);
  Serial.print("Device Ready");
  digitalWrite(chip_select, HIGH);
}

long oldPos  = -999;

void loop() {
  long newPos;
  newPos = der.read();
  
  if (newPos != oldPos) {
    Serial.print("New Pos (mm) = ");
    double pos = (newPos / 1024.0) * 2.0;
    Serial.println(pos);
    oldPos = newPos;
  }
}
