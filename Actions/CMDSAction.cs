using BarRaider.SdTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace F4BMS_StreamDeck.Actions
{
    [PluginActionId("com.kubbie.bms.cmdsaction")]
    public class CMDSAction : KeypadBase
    {
        static private FlightDataUtil _flightDataUtil = new FlightDataUtil();
        private PluginSettings settings;
        private class PluginSettings
        {
            public static PluginSettings CreateDefaultSettings()
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, "CreateDefaultSettings started");
                PluginSettings instance = new PluginSettings();
                instance.cmdsDataItem = String.Empty;
                //instance.chaffLowString = "CHAFF" + Environment.NewLine + "LO";
                //instance.flareLowString = "FLARE" + Environment.NewLine + "LO";

                return instance;
            }

            [JsonProperty(PropertyName = "cmdsDataItem")]
            public string cmdsDataItem { get; set; }

            //[JsonProperty(PropertyName = "chaffLowString")]
            //public string chaffLowString { get; set; }

            //[JsonProperty(PropertyName = "flareLowString")]
            //public string flareLowString { get; set; }

        }

        public CMDSAction(ISDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            if (payload.Settings == null || payload.Settings.Count == 0)
            {
                this.settings = PluginSettings.CreateDefaultSettings();
                Connection.SetSettingsAsync(JObject.FromObject(settings));
            }
            else
            {
                this.settings = payload.Settings.ToObject<PluginSettings>();
            }
        }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) {}

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            Tools.AutoPopulateSettings(settings, payload.Settings);
        }
        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, "Destructor called");
        }

        public override void KeyPressed(KeyPayload payload)
        {
        }

        public override void KeyReleased(KeyPayload payload){}

        public async override void OnTick()
        {
            if (_flightDataUtil.ReadSharedMem() != null)
            {
                string data = _flightDataUtil.GetCMDSFlightData(settings.cmdsDataItem);
                await Connection.SetTitleAsync(data);
            }
            else
            {
                await Connection.SetTitleAsync("_");
            }
        }

    }
}
