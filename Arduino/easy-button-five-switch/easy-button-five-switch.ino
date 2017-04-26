const int PRE_DEV_PIN = 4;
const int DEV_PIN = 5;
const int QA_PIN = 6;
const int QA2_PIN = 7;
const int PROD_PIN = 8;

const byte PRE_DEV_SIGNAL = 1;
const byte DEV_SIGNAL = 2;
const byte QA_SIGNAL = 3;
const byte QA2_SIGNAL = 4;
const byte PROD_SIGNAL = 5;
const byte STOP_SIGNAL = 6;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  if (digitalRead(PRE_DEV_PIN) == 1) {
    sendSignal(PRE_DEV_SIGNAL);
  }
  else if (digitalRead(DEV_PIN) == 1) {
    sendSignal(DEV_SIGNAL);
  }
  else if (digitalRead(QA_PIN) == 1) {
    sendSignal(QA_SIGNAL);
  }
  else if (digitalRead(QA2_PIN) == 1) {
    sendSignal(QA2_SIGNAL);
  }
  else if (digitalRead(PROD_PIN) == 1) {
    sendSignal(PROD_SIGNAL);
  }
}

void sendSignal(byte bytSignal) {
  Serial.write(bytSignal);
  Serial.write(STOP_SIGNAL);
  delay(1000);
}

