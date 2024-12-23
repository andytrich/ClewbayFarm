#include <Wire.h>
#include <SensirionI2CScd4x.h>

SensirionI2CScd4x scd4x;
int FAN_PIN = 34;
bool FanIsOn = false;
int HUMIDITY_PIN = 10;
bool HumidityIsOn = false;
int HEATER_PIN = 9;
bool HeaterIsOn = false;

void heaterOn() {
  Serial.println("Switching on Heater");
  digitalWrite(HEATER_PIN, HIGH);
  HeaterIsOn = true;
}
void heaterOff() {
  Serial.println("Switching off Heater");
  digitalWrite(HEATER_PIN, LOW);
  HeaterIsOn = false;
}

void humidityOn() {
  Serial.println("Switching on Humidity");
  digitalWrite(HUMIDITY_PIN, HIGH);
  HumidityIsOn = true;
}
void humidityOff() {
  Serial.println("Switching off Humidity");
  digitalWrite(HUMIDITY_PIN, LOW);
  HumidityIsOn = false;
}

void fanOn() {
  Serial.println("Switching on Fan");
  digitalWrite(FAN_PIN, HIGH);
  FanIsOn = true;
}
void fanOff() {
  Serial.println("Switching off Fan");
  digitalWrite(FAN_PIN, LOW);
  FanIsOn = false;
}

void printUint16Hex(uint16_t value) {
  Serial.print(value < 4096 ? "0" : "");
  Serial.print(value < 256 ? "0" : "");
  Serial.print(value < 16 ? "0" : "");
  Serial.print(value, HEX);
}

void printSerialNumber(uint16_t serial0, uint16_t serial1, uint16_t serial2) {
  Serial.print("Serial: 0x");
  printUint16Hex(serial0);
  printUint16Hex(serial1);
  printUint16Hex(serial2);
  Serial.println();
}


void setup() {

  Serial.begin(115200);


  scd4x.begin(Wire);
  Serial.println("starting up");


  while (!Serial) {
    delay(100);
  }

  //Wire.begin(21, 18);  // SDA, SCL for ESP32
  Wire.begin();

  uint16_t error;
  char errorMessage[256];

  scd4x.begin(Wire);

  // stop potentially previously started measurement
  error = scd4x.stopPeriodicMeasurement();
  if (error) {
    Serial.print("Error trying to execute stopPeriodicMeasurement(): ");
    errorToString(error, errorMessage, 256);
    Serial.println(errorMessage);
  }

  uint16_t serial0;
  uint16_t serial1;
  uint16_t serial2;
  error = scd4x.getSerialNumber(serial0, serial1, serial2);
  if (error) {
    Serial.print("Error trying to execute getSerialNumber(): ");
    errorToString(error, errorMessage, 256);
    Serial.println(errorMessage);
  } else {
    printSerialNumber(serial0, serial1, serial2);
  }

  // Start Measurement
  error = scd4x.startPeriodicMeasurement();
  if (error) {
    Serial.print("Error trying to execute startPeriodicMeasurement(): ");
    errorToString(error, errorMessage, 256);
    Serial.println(errorMessage);
  }

  Serial.println("Waiting for first measurement... (5 sec)");

  pinMode(FAN_PIN, OUTPUT);
  pinMode(HEATER_PIN, OUTPUT);
  pinMode(HUMIDITY_PIN, OUTPUT);
}

void loop() {
  uint16_t error;
  char errorMessage[256];

  delay(100);

  // Read Measurement
  uint16_t co2 = 0;
  float temperature = 0.0f;
  float humidity = 0.0f;
  bool isDataReady = false;
  error = scd4x.getDataReadyFlag(isDataReady);
  if (error) {
    Serial.print("Error trying to execute getDataReadyFlag(): ");
    errorToString(error, errorMessage, 256);
    Serial.println(errorMessage);
    return;
  }
  if (!isDataReady) {
    return;
  }
  error = scd4x.readMeasurement(co2, temperature, humidity);
  if (error) {
    Serial.print("Error trying to execute readMeasurement(): ");
    errorToString(error, errorMessage, 256);
    Serial.println(errorMessage);
  } else if (co2 == 0) {
    Serial.println("Invalid sample detected, skipping.");
  } else {
    Serial.print("Co2:");
    Serial.print(co2);
    Serial.print("\t");
    Serial.print("Temperature:");
    Serial.print(temperature);
    Serial.print("\t");
    Serial.print("Humidity:");
    Serial.print(humidity);
    Serial.print("\t");
    Serial.print("Fan Status:");
    Serial.print(FanIsOn);
    Serial.print("\t");
    Serial.print("Heater Status:");
    Serial.print(HeaterIsOn);
    Serial.print("\t");
    Serial.print("Mister Status:");
    Serial.println(HumidityIsOn);
    if (humidity < 90) {
      if (!HumidityIsOn) {
        humidityOn();
      }
    } else {
      if (HumidityIsOn) {
        humidityOff();
      }
    }
    if (co2 > 800) {
      if (!FanIsOn) {
        fanOn();
      }
    } else {
      if (FanIsOn) {
        fanOff();
        
      }
    }
    if (temperature < 18) {
      if (!HeaterIsOn) {
        heaterOn();
      }
    } else {
      if (HeaterIsOn) {
        heaterOff();
      }
    }
  }
}
