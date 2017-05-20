const int PRE_DEV_PIN = 5;
const int DEV_PIN = 7;
const int QA_PIN = 9;
const int QA2_PIN = 11;
const int PROD_PIN = 13;

const byte PRE_DEV_SIGNAL = 1;
const byte DEV_SIGNAL = 2;
const byte QA_SIGNAL = 3;
const byte QA2_SIGNAL = 4;
const byte PROD_SIGNAL = 5;
const byte STOP_SIGNAL = 6;

void setup() {
  // put your setup code here, to run once:
  pinMode(PRE_DEV_PIN, INPUT);
  pinMode(DEV_PIN, INPUT);
  pinMode(QA_PIN, INPUT);
  pinMode(QA2_PIN, INPUT);
  pinMode(PROD_PIN, INPUT);
  pinMode(12, OUTPUT);
  Serial.begin(9600);
  
}

void loop() {
  // put your main code here, to run repeatedly:
  if (digitalRead(PRE_DEV_PIN) == HIGH) {
    sendSignal(PRE_DEV_SIGNAL);
  }
  else if (digitalRead(DEV_PIN) == HIGH) {
    sendSignal(DEV_SIGNAL);
  }
  else if (digitalRead(QA_PIN) == HIGH) {
    sendSignal(QA_SIGNAL);
  }
  else if (digitalRead(QA2_PIN) == HIGH) {
    sendSignal(QA2_SIGNAL);
  }
  else if (digitalRead(PROD_PIN) == HIGH) {
    sendSignal(PROD_SIGNAL);
  }
}

void sendSignal(byte bytSignal) {
  Serial.print(bytSignal);
  Serial.print(STOP_SIGNAL);
  Serial.flush();
  digitalWrite(12, HIGH);
  delay(1000);
  digitalWrite(12, LOW);
}

