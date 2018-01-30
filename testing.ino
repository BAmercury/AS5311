

int DataPin = 4;
int ClockPin = 3;
int ChipSelectPin = 2;
int IndexPin = 5;





void setup() {
  // put your setup code here, to run once:

  Serial.begin(115200);

  pinMode(DataPin, INPUT);
  pinMode(ClockPin, OUTPUT);
  pinMode(ChipSelectPin, OUTPUT);
  pinMode(IndexPin, INPUT);

}

void loop() {
  // put your main code here, to run repeatedly:

  long value;
  value = encoder_value();
  Serial.print("Measured: ");
  Serial.println(value);

  //value = encoder_position();
  //Serial.print("Position: ");
  //Serial.println(value);

  delay(1000);
  
}


long encoder_position()
{
  return ((encoder_value() * 2)/4096);
}

long encoder_value()
{
  return (read_chip() >> 6);

}

unsigned int read_chip()
{

  unsigned int raw_value = 0;
  unsigned int inputstream = 0;
  unsigned int c;

  digitalWrite(ChipSelectPin, HIGH);
  digitalWrite(ClockPin, HIGH);
  delay(100);
  digitalWrite(ChipSelectPin, LOW);
  delay(10);
  digitalWrite(ClockPin, LOW);
  delay(10);
  for (c = 0; c < 18; c++)
  {
    digitalWrite(ClockPin, HIGH);
    delay(10);
    inputstream = digitalRead(DataPin);
    raw_value = ( (raw_value << 1) + inputstream);
    digitalWrite(ClockPin, LOW);
    delay(10);

  }
  return raw_value;

}
