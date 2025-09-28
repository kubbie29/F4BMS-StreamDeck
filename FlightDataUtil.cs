using BarRaider.SdTools;
using Common.Math;
using F4SharedMem;
using F4SharedMem.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace F4BMS_StreamDeck
{
    public class FlightDataUtil
    {
        private Reader _sharedMemReader = new Reader();
        private FlightData _lastFlightData;
                
        private string ChaffLowString {  get; set; }
        private string FlareLowString { get; set; }

        public FlightDataUtil() 
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, "Start SharedMem Process");
            _lastFlightData = _sharedMemReader.GetCurrentData();
            ChaffLowString = "CHAFF" + Environment.NewLine + "LO";
            FlareLowString = "FLARE" + Environment.NewLine + "LO";

        }

        public FlightData ReadSharedMem()
        {
            return _lastFlightData = _sharedMemReader.GetCurrentData();
        }

        public string GetCMDSFlightData(string selectedData)
        {
            /**
             * TODO: 
             * Multiline the Chaff/Flare Low
             * On destruction/shutdown, clear out the buttons. (Do in the action class)
             * 
             * When Low is selected, show an option to let custom text be entered
             * On Mode selected, allow keypress button bind.
            **/
            string data = "";
            var lightBits2 = (LightBits2)_lastFlightData.lightBits2;
            switch (selectedData)
            {
                case "cc":
                    data = _lastFlightData.ChaffCount >= 0 ? _lastFlightData.ChaffCount.FormatDecimal(decimalPlaces: 0) : "0";
                    break;
                case "fc":
                    data = _lastFlightData.FlareCount >= 0 ? _lastFlightData.FlareCount.FormatDecimal(decimalPlaces: 0) : "0";
                    break;
                case "cl":
                    if ((lightBits2 & LightBits2.ChaffLo) == LightBits2.ChaffLo)
                    {
                        data = ChaffLowString;
                    }
                    else data = "";
                    break;
                case "fl":
                    if ((lightBits2 & LightBits2.FlareLo) == LightBits2.FlareLo)
                    {
                        data = FlareLowString;
                    }
                    else data = "";
                    break;
                case "mode":
                    switch(_lastFlightData.cmdsMode)
                    {
                        case 0:
                            data = "OFF";
                            break;
                        case 1:
                            data = "STBY";
                            break;
                        case 2:
                            data = "MAN";
                            break;
                        case 3:
                            data = "SEMI";
                            break;
                        case 4:
                            data = "AUTO";
                            break;
                        case 5:
                            data = "BYP";
                            break;
                    }
                    
                    break;

            }

            return data;
        }
    }
}
