const int pressureSensor1 = A0; // Pin for the first pressure sensor
const int pressureSensor2 = A1; // Pin for the second pressure sensor
const int led1 = 2;             // Pin for the first LED
const int led2 = 3;             // Pin for the second LED

void setup() {
  pinMode(led1, OUTPUT);        // Set the first LED pin as output
  pinMode(led2, OUTPUT);        // Set the second LED pin as output
  Serial.begin(9600);           // Initialize serial communication at 9600 baud rate
}

void loop() {
  int sensorValue1 = analogRead(pressureSensor1); // Read the value from the first pressure sensor
  int sensorValue2 = analogRead(pressureSensor2); // Read the value from the second pressure sensor

  if (sensorValue1 > 20) {                       // Check if the first pressure sensor value exceeds 512
    digitalWrite(led1, HIGH);                     // Turn on the first LED
    Serial.println("RED");                        // Print "RED" to the Serial Monitor
  } else {
    digitalWrite(led1, LOW);                      // Turn off the first LED if the value does not exceed 512
  }

  if (sensorValue2 > 300) {                       // Check if the second pressure sensor value exceeds 512
    digitalWrite(led2, HIGH);                     // Turn on the second LED
    Serial.println("PINK");                       // Print "PINK" to the Serial Monitor
  } else {
    digitalWrite(led2, LOW);                      // Turn off the second LED if the value does not exceed 512
  }

  delay(1000); // Delay for a second before the next reading
}

