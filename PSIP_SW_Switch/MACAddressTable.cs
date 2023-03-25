using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpPcap;

namespace PSIP_SW_Switch
{
    static class MACAddressTable
    {

        public static object? MacAddressTableLock = new();
        public static BindingList<EndDeviceRecord>? MacAddressTable = new();
        //public static ObservableCollection<EndDeviceRecord>? MacAddressTable = new();

        public static int timerValue = 30;
        public static object? TimerLock = new();

        public static void Init(bool clear = false)
        {
            GUIController.MacGUI.StopTimer();
            MacAddressTable.Clear();

            if (!clear)
            {
                GUIController.MacGUI.InitNumeric();
            }

            GUIController.MacGUI.Init();
            GUIController.MacGUI.StartTimer();
        }

        public static void StopTimer()
        {
            GUIController.MacGUI.StopTimer();
        }

        public static void Clear()
        {
            GUIController.MacGUI.DeleteAll();
        }
        public static void AddRecord(PacketForQueue packet)
        {
            //if (packet.ethPacket.DestinationHardwareAddress.Equals(PhysicalAddress.Parse("ff:ff:ff:ff:ff:ff")))
            //{
            //    return;
            //}

            if (InterfaceController.d1.MacAddress.Equals(packet.ethPacket.SourceHardwareAddress)
                ||
                InterfaceController.d2.MacAddress.Equals(packet.ethPacket.SourceHardwareAddress))
            {
                return;
            }

            bool isInTable = false;

            lock (MacAddressTableLock)
            {
                isInTable = IsInTable(packet, addRecord: true);
            }

            if (isInTable)
            {
                return;
            }

            var endDeviceInfo =
                    new EndDeviceRecord(packet, InterfaceController.deviceMap[packet.device],
                        0);

            GUIController.MacGUI.AddRecord(endDeviceInfo);
            
        }

        public static bool IsInTable(PacketForQueue packet, bool addRecord)
        {
            var mac = addRecord ? packet.ethPacket.SourceHardwareAddress : packet.ethPacket.DestinationHardwareAddress;

            // If checking to what port to send (addRecord == false) and destination MAC is Broadcast send it to all interfaces as not in table
            if (!addRecord &&
                packet.ethPacket.DestinationHardwareAddress.Equals(PhysicalAddress.Parse("ff:ff:ff:ff:ff:ff")))
            {
                return false;
            }

            foreach (var record in MacAddressTable)
            {
                if (record.MacAddress.Equals(mac))
                {
                    if (addRecord)
                    {
                        if (record.Port != InterfaceController.deviceMap[packet.device])
                        {
                            // Device moved from one port to another

                            SysLog.Log(SysLogSeverity.Alert,
                                $"[MAC] Device {EndDeviceRecord.FormatMAC(record.MacAddress)} moved from port [{record.Port}] to port [{InterfaceController.deviceMap[packet.device]}]");
                            Console.WriteLine(
                                $"[MAC] Device {EndDeviceRecord.FormatMAC(record.MacAddress)} moved from port [{record.Port}] to port [{InterfaceController.deviceMap[packet.device]}]");
                            MacAddressTable[MacAddressTable.IndexOf(record)].Port =
                                InterfaceController.deviceMap[packet.device];
                        }

                        record.Timer = 0;
                        //GUIController.MacGUI.Refresh();
                        //Console.WriteLine(
                        //    $"[MAC] Device {EndDeviceRecord.FormatMAC(record.MacAddress)} refreshed on port [{record.Port}] to port");
                    }

                    return true;
                }
            }

            if (addRecord)
                {
                    Console.WriteLine($"[MAC] NEW Device {EndDeviceRecord.FormatMAC(packet.ethPacket.SourceHardwareAddress)} on port [{InterfaceController.deviceMap[packet.device]}]");

                    SysLog.Log(SysLogSeverity.Informational,
                        $"[MAC] New device [{EndDeviceRecord.FormatMAC(packet.ethPacket.SourceHardwareAddress)}] on port [{InterfaceController.deviceMap[packet.device]}]");


                /*StackTrace stackTrace = new StackTrace(); // create a new StackTrace object
                Console.WriteLine("\t\tCall Stack:");

                // Loop through each stack frame and print the method name
                for (int i = 0; i < 3; i++)
                {
                    StackFrame stackFrame = stackTrace.GetFrame(i);
                    Console.WriteLine("\t\t{0}",
                        stackFrame.GetMethod().ToString());
                }*/


                return false;
                }
            
            //Console.WriteLine($"[MAC] AddRecord = {addRecord} Not in table not Added {EndDeviceRecord.FormatMAC(packet.ethPacket.SourceHardwareAddress)} on port [{InterfaceController.deviceMap[packet.device]}]");
            // TODO Tu netreba nieco s tym? 
            return false;
        }

        public static void UpdateRecordExpirations()
        {
            //Console.WriteLine("Updating Records");
            //GUIController.MacGUI.SuspendLayout();

            lock (MacAddressTableLock)
            {
                foreach (var record in MacAddressTable)
                {
                    record.Timer++;
                }

                var expiredRecords = MacAddressTable.Where(item => item.Timer >= timerValue).ToList();
                //Console.WriteLine($"We have {expiredRecords.Count} expired records");
                foreach (var expiredRecord in expiredRecords)
                {
                    MacAddressTable.Remove(expiredRecord);
                }
            }
            //GUIController.MacGUI.ResumeLayout();
            //GUIController.MacGUI.Refresh();
        }

        public static void RemoveInvalidRecords(ILiveDevice? dev)
        {
            Console.WriteLine($"Removing Invalid Records\nCalled by: {(new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name}");
            
            if (dev == null)
            {
                return; 
            }

            lock(MacAddressTableLock)
            {
                var invalidRecords = MacAddressTable.Where(item => item.Port == InterfaceController.deviceMap[dev]).ToList();
                foreach (var invalidRec in invalidRecords)
                {
                    MacAddressTable.Remove(invalidRec);
                }
            }
        }


    }


    public class EndDeviceRecord : INotifyPropertyChanged
    {
        private PhysicalAddress _macAddress;

        [Browsable(true)]
        public PhysicalAddress MacAddress
        {
            get { return _macAddress; }
            set
            {
                _macAddress = value;
                NotifyPropertyChanged("MacAddress");
            }
        }

        private int _port;

        [Browsable(true)]
        public int Port
        {
            get { return _port; }
            set
            {
                
                if (_port != value)
                {
                    _port = value;
                    NotifyPropertyChanged("Port");
                }
                
            }
        }

        private int _timer;

        [Browsable(true)]
        public int Timer
        {
            get { return _timer; }
            set
            {
                if (_timer != value)
                {
                    _timer = value;
                    NotifyPropertyChanged("Timer");
                }
                
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        public EndDeviceRecord(PacketForQueue packet, int port, int timer)
        {
            MacAddress = packet.ethPacket.SourceHardwareAddress;
            Port = port;
            Timer = timer;
        }



        /* public static string FormatMAC<T>(T mac)
         {
             string macAddress;

             if (mac is PhysicalAddress m)
             {
                 macAddress = m.ToString();
             }
             else if (mac is string s)
             {
                 if (s.Length != 12)
                 {
                     throw new InvalidDataException("Invalid type of MAC Address");
                 }
                 macAddress = s;
             }
             else
             {
                 throw new InvalidDataException("Invalid type of MAC Address");
             }

             macAddress = macAddress.ToUpper();
             string formattedMacAddress = "";
             for (int i = 0; i < macAddress.Length; i += 2)
             {
                 formattedMacAddress += macAddress.Substring(i, 2);
                 if (i < macAddress.Length - 2)
                 {
                     formattedMacAddress += ":";
                 }
             }
             return formattedMacAddress;
         }*/

        public static string FormatMAC<T>(T mac)
        {
            if (!(mac is PhysicalAddress m || mac is string s && s.Length == 12))
            {
                throw new InvalidDataException("Invalid type of MAC Address");
            }

            string macAddress = mac.ToString().ToUpper();

            var sb = new StringBuilder(17); // 17 characters for 12 bytes and 5 colons
            for (int i = 0; i < macAddress.Length; i += 2)
            {
                if (i > 0) sb.Append(':');
                sb.Append(macAddress, i, 2);
            }
            return sb.ToString();
        }

    }
}
