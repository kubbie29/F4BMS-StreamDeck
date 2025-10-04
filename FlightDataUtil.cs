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

        public FlightDataUtil() 
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, "Start SharedMem Process");
            _lastFlightData = _sharedMemReader.GetCurrentData();

        }

        public FlightData ReadSharedMem()
        {
            return _lastFlightData = _sharedMemReader.GetCurrentData();
        }

        public int GetCMDSFlightData(string selectedData)
        {
            /**
             * TODO: 
             * On destruction/shutdown, clear out the buttons. (Do in the action class)
             * 
             * On Mode selected, allow keypress button bind.
            **/
            int data = -1;
            var lightBits2 = (LightBits2)_lastFlightData.lightBits2;
            switch (selectedData)
            {
                case "cc":
                    data = Convert.ToInt32(_lastFlightData.ChaffCount);
                    break;
                case "fc":
                    data = Convert.ToInt32(_lastFlightData.FlareCount);
                    break;
                case "go":
                    if ((lightBits2 & LightBits2.Go) == LightBits2.Go)
                    {
                        data = 1;
                    }
                    else data = 0;
                    break;
                case "nogo":
                    if ((lightBits2 & LightBits2.NoGo) == LightBits2.NoGo)
                    {
                        data = 1;
                    }
                    else data = 0;
                    break;
                case "rdy":
                    if ((lightBits2 & LightBits2.Rdy) == LightBits2.Rdy)
                    {
                        data = 1;
                    }
                    else data = 0;
                    break;
                case "cl":
                    if ((lightBits2 & LightBits2.ChaffLo) == LightBits2.ChaffLo)
                    {
                        data = 1;
                    }
                    else data = 0;
                    break;
                case "fl":
                    if ((lightBits2 & LightBits2.FlareLo) == LightBits2.FlareLo)
                    {
                        data = 1;
                    }
                    else data = 0;
                    break;
                case "mode":
                    data = _lastFlightData.cmdsMode;
                    break;

                }

            return data;
        }
    }
}
