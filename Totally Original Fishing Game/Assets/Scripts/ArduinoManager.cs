using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArduinoManager : MonoBehaviour
{


    private SerialPort _serial;

    [SerializeField] private string serialPort = "COM1";
    [SerializeField] private int baudrate = 9600;


    private static ArduinoManager instance = null;

    public static ArduinoManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        _serial = new SerialPort(serialPort, baudrate);
        _serial.NewLine = "\n";
        _serial.Open();
    }


    public void LightPulse(int light)
    {
        if (!_serial.IsOpen) return;

        _serial.WriteLine(light.ToString());
    }


    // Update is called once per frame
    void Update()
    {

        if (!_serial.IsOpen) return;

        if (_serial.BytesToRead <= 0) return;

        var message = _serial.ReadLine().Trim();


        if (GameManager.Instance.GameState == GameState.FISHING)
        {

            if (message =="1") { InputManager.Instance.Fishing_input = 1; InputManager.Instance.InputProcess(); } //Up input

            else if (message =="2") { InputManager.Instance.Fishing_input = 2; InputManager.Instance.InputProcess(); } //Down input

            else if (message =="3") { InputManager.Instance.Fishing_input = 3; InputManager.Instance.InputProcess(); } //Left input

            else if (message =="4") { InputManager.Instance.Fishing_input = 4; InputManager.Instance.InputProcess(); } //Right input

        }

    }





    private void OnDestroy()
    {
        if (!_serial.IsOpen) return;
        _serial.Close();
    }

}
