bool isFishing = false;

void setup() {
  // put your setup code here, to run once:

  //Start detection (trigger)
  pinMode(3,OUTPUT);
  pinMode(5,OUTPUT);
  pinMode(7,OUTPUT);
  pinMode(9,OUTPUT);

  //Detection result (echo)
  pinMode(2,INPUT);
  pinMode(4,INPUT);
  pinMode(6,INPUT);
  pinMode(8,INPUT);

  // Potentiometer
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);

  //LEDs
  pinMode(10, OUTPUT);
  pinMode(11, OUTPUT);
  pinMode(12, OUTPUT);
  pinMode(13, OUTPUT);

  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  if (isFishing){
  useCaptor(2,3);
  useCaptor(4,5);
  useCaptor(6,7);
  useCaptor(8,9);
  }
  else {
  Serial.println(String(analogRead(A0)) + " " + String(analogRead(A1)));
  }
}

void useCaptor(int pinIn, int pinOut){
  digitalWrite(pinOut,HIGH);
  delayMicroseconds(10);
  digitalWrite(pinOut,LOW);


  if (pulseIn(pinIn, HIGH)*340/2 /1000 < 500)
    Serial.println(pinIn/2);

  delay(60);

}

void serialEvent() {

  String message = Serial.readStringUntil('\n');
  if (message == "f") {
    isFishing = true;
  }
  else  if (message == "d") {
    isFishing = false;
  }
  else if (message == "p") {
    perfectLanding();
  }
  else {
  digitalWrite(message.toInt(), HIGH);
  delay(100);
  digitalWrite(message.toInt(), LOW);
  }
}

void perfectLanding(){
  for (int i = 0; i<3; i++){
    for (int j = 10; j<14; j++){
      digitalWrite(j, HIGH);
    }
      delay(100);
    for (int j = 10; j<14; j++){
      digitalWrite(j, LOW);
    }
      delay(100);
  }
}