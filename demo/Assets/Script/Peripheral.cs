using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO.Ports;

public class Peripheral : MonoBehaviour
{
    public bool connectOnStart;
    public string portName;
    public int baudRate;
    public int[] readings;

    private SerialPort com;
    private string buffer;
    private Thread updateBufferThread;

    private const string FRAME_PATTERN =
        @"FRAME\t([0-9]+)\t([0-9]+)\t([0-9]+)\t([0-9]+)\t([0-9]+)";
    private const string NUMBER_PATTERN = "[0-9]+";

    void Start()
    {
        // Initialise readings
        readings = new int[5];
        for (var i = 0; i <= 4; i++) readings[i] = 0;

        // Serial port is slow compared with frames
        // So it should be updated in another thread 
        updateBufferThread = new Thread(new ThreadStart(UpdateBuffer));

        // Display possible serial ports
        string portsMessage = "[PERIPHERALS]\n";
        foreach (string portName in SerialPort.GetPortNames())
            portsMessage += portName + "\n";
        print(portsMessage);

        if (connectOnStart) SetupNewSerialConnection();
    }

    public void SetupNewSerialConnection()
    {
        if (portName != null && baudRate > 0)
        {
            com = new SerialPort();
            com.PortName = portName;
            com.BaudRate = baudRate;
            com.Open();
            updateBufferThread.Start();
        }
        else print("Invalid port configuration");
    }

    void UpdateBuffer()
    {
        // Keep read a new line into buffer
        while (true) buffer = com.ReadLine();
    }

    void Update()
    {
        if (buffer == null) return;

        int readingIndex = 0;
        if (Regex.IsMatch(buffer, FRAME_PATTERN))
            foreach (Match match in Regex.Matches(buffer, NUMBER_PATTERN))
            {
                readings[readingIndex] = Convert.ToInt32(match.Value);
                readingIndex++;
            }
    }

    ~Peripheral()
    {
        print("END");
        updateBufferThread.Abort();
        com.Close();
    }
}
