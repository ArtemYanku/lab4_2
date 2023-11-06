using System;
using System.Collections.Generic;

// Базовий клас "Комп'ютер"
public class Computer
{
    public string IPAddress { get; set; }
    public int Power { get; set; }
    public string OperatingSystem { get; set; }

    public Computer(string ipAddress, int power, string os)
    {
        IPAddress = ipAddress;
        Power = power;
        OperatingSystem = os;
    }
}

// Інтерфейс для підключення до мережі та передачі даних
public interface IConnectable
{
    void Connect(Computer otherComputer);
    void Disconnect(Computer otherComputer);
    void SendData(Computer otherComputer, string data);
    string ReceiveData();
}

// Клас "Сервер" успадковує "Комп'ютер" та реалізує інтерфейс "IConnectable"
public class Server : Computer, IConnectable
{
    private List<Computer> connectedComputers = new List<Computer>();

    public Server(string ipAddress, int power, string os) : base(ipAddress, power, os) { }

    public void Connect(Computer otherComputer)
    {
        connectedComputers.Add(otherComputer);
        Console.WriteLine($"Connected to {otherComputer.IPAddress}");
    }

    public void Disconnect(Computer otherComputer)
    {
        connectedComputers.Remove(otherComputer);
        Console.WriteLine($"Disconnected from {otherComputer.IPAddress}");
    }

    public void SendData(Computer otherComputer, string data)
    {
        Console.WriteLine($"Sending data to {otherComputer.IPAddress}: {data}");
    }

    public string ReceiveData()
    {
        return "Data received from connected computers.";
    }
}

// Клас "Робоча станція" успадковує "Комп'ютер" та реалізує інтерфейс "IConnectable"
public class Workstation : Computer, IConnectable
{
    private List<Computer> connectedServers = new List<Computer>();

    public Workstation(string ipAddress, int power, string os) : base(ipAddress, power, os) { }

    public void Connect(Computer otherComputer)
    {
        connectedServers.Add(otherComputer);
        Console.WriteLine($"Connected to server at {otherComputer.IPAddress}");
    }

    public void Disconnect(Computer otherComputer)
    {
        connectedServers.Remove(otherComputer);
        Console.WriteLine($"Disconnected from server at {otherComputer.IPAddress}");
    }

    public void SendData(Computer otherComputer, string data)
    {
        Console.WriteLine($"Sending data to server at {otherComputer.IPAddress}: {data}");
    }

    public string ReceiveData()
    {
        return "Data received from connected server(s).";
    }
}

// Клас "Мережа" моделює взаємодію між комп'ютерами
public class Network
{
    public static void Main()
    {
        Server server1 = new Server("192.168.1.1", 1000, "Windows Server");
        Server server2 = new Server("192.168.1.2", 1500, "Linux Server");
        Workstation workstation1 = new Workstation("192.168.2.1", 500, "Windows 10");

        server1.Connect(workstation1);
        workstation1.SendData(server1, "Hello, Server!");

        workstation1.Connect(server2);
        server2.SendData(workstation1, "Hi, Workstation!");

        server1.Disconnect(workstation1);
        workstation1.Disconnect(server2);
    }
}
