// ? Editor always prompts "Arduino.h" does not exist
#include <Arduino.h>

void setup()
{
    Serial.begin(9600);
}

void loop()
{
    // Obtain 5 voltages from analogue ports
    int voltages[5];
    voltages[0] = analogRead(A0);
    voltages[1] = analogRead(A1);
    voltages[2] = analogRead(A2);
    voltages[3] = analogRead(A3);
    voltages[4] = analogRead(A4);

    // Send 5 voltages to serial port
    char dataframe[100];
    sprintf(
        dataframe,
        "FRAME\t%d\t%d\t%d\t%d\t%d\n",
        voltages[0],
        voltages[1],
        voltages[2],
        voltages[3],
        voltages[4]
    );
    Serial.write(dataframe);

    delay(20);
}