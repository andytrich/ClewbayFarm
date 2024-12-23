#include <WiFi.h>
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include <secret.h>


void setup() {
  Serial.begin(115200);

  // Connect to WiFi
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi...");
  }
  Serial.println("Connected to WiFi");

  // Example sensor data
  float temperature = 22.5;
  float humidity = 55.3;
  int co2Level = 400;

  // Create JSON object
  StaticJsonDocument<200> jsonDoc;
  jsonDoc["temperature"] = temperature;
  jsonDoc["humidity"] = humidity;
  jsonDoc["co2Level"] = co2Level;

  // Serialize JSON object
  String jsonPayload;
  serializeJson(jsonDoc, jsonPayload);

  // Send JSON to the server
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    http.begin(serverUrl);
    http.addHeader("Content-Type", "application/json");

    int httpResponseCode = http.POST(jsonPayload);

    if (httpResponseCode > 0) {
      Serial.print("HTTP Response code: ");
      Serial.println(httpResponseCode);
      String response = http.getString();
      Serial.println("Response: " + response);
    } else {
      Serial.print("Error code: ");
      Serial.println(httpResponseCode);
    }
    http.end();
  }
}


void loop() {
  // Put code here to repeatedly make requests or handle logic
}
