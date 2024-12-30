void setup() {
  // put your setup code here, to run once:
  pinMode(6,OUTPUT);
  pinMode(7,OUTPUT);
  pinMode(8,OUTPUT);
  pinMode(9,OUTPUT);
  pinMode(2,INPUT);
  pinMode(3,INPUT);
  pinMode(4,INPUT);
  pinMode(5,INPUT);
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  useCaptor(2,6);
  useCaptor(3,7);
  useCaptor(4,8);
  useCaptor(5,9);

}

void useCaptor(int pinIn, int pinOut){
  digitalWrite(pinOut,HIGH);
  delayMicroseconds(10);
  digitalWrite(pinOut,LOW);


  if (pulseIn(pinIn, HIGH)*340/2 /1000 < 500)
    Serial.println(pinIn);

  delay(60);

}