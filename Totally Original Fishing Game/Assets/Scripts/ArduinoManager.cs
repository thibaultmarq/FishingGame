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

    bool sent;
    private static ArduinoManager instance = null;
    private float angle = -1;
    private float distance = -1;
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
        sent = false;
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

    public void Send(String message)
    {
        if (!_serial.IsOpen) return;
        _serial.WriteLine(message);
    }

    // Update is called once per frame
    void Update()
    {

        if (!_serial.IsOpen) return;

        if (_serial.BytesToRead <= 0) return;
 
        if (GameManager.Instance.GameState == GameState.FISHING && sent == false)
        {
            Send("f");
            sent = true;
        }
        else if (GameManager.Instance.GameState != GameState.FISHING && sent == true)
        {
            Send("d");
            sent = false;
        }

        var message = _serial.ReadLine().Trim();
        if (message.Split(' ').Length != 2)
        {
            Debug.Log(message);
        }

        if (GameManager.Instance.GameState == GameState.FISHING && message.Split(' ').Length<2)
        {

            if (message == "1") { InputManager.Instance.Fishing_input = 1; InputManager.Instance.InputProcess(); } //Up input

            else if (message == "2") { InputManager.Instance.Fishing_input = 2; InputManager.Instance.InputProcess(); } //Down input

            else if (message == "3") { InputManager.Instance.Fishing_input = 3; InputManager.Instance.InputProcess(); } //Left input

            else if (message == "4") { InputManager.Instance.Fishing_input = 4; InputManager.Instance.InputProcess(); } //Right input

        }
        else if ( message.Split(' ').Length == 2)
        {
            if (GameManager.Instance.GameState == GameState.ANGULARSELECTION)
            {
                float.TryParse(message.Split(" ")[0], out angle);
            }
            else if (GameManager.Instance.GameState == GameState.DISTANCESELECTION)
            {
                float.TryParse(message.Split(" ")[1], out distance);
            }
        }

    }


    public Vector2 GetFishrodOrientation()
    {
        return new Vector2(Mathf.Cos(angle/512 *Mathf.PI), Mathf.Sin(angle / 512 * Mathf.PI));
    }

    public float GetFishrodDistance()
    {
        return distance/1024;
    }

    private void OnDestroy()
    {
        if (!_serial.IsOpen) return;
        _serial.Close();
    }

}
