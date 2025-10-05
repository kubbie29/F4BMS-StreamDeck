using BarRaider.SdTools;
using Common.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F4BMS_StreamDeck.Actions
{
    [PluginActionId("com.kubbie.bms.engineaction")]
    internal class EngineAction : KeypadBase
    {
        static private FlightDataUtil _flightDataUtil = new FlightDataUtil();
        private PluginSettings settings;

        private class PluginSettings
        {
            public static PluginSettings CreateDefaultSettings()
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, "CreateDefaultSettings started");

                PluginSettings instance = new PluginSettings();
                instance.engineDataType = String.Empty;
                instance.decimalFormat = "n";

                return instance;
            }
            [JsonProperty(PropertyName = "engineDataType")]
            public string engineDataType { get; set; }

            [JsonProperty(PropertyName = "decimalFormat")]
            public string decimalFormat { get; set; }
        }
        public EngineAction(ISDConnection connection, InitialPayload payload) : base(connection, payload)
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

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            Tools.AutoPopulateSettings(settings, payload.Settings);
            SaveSettings();
        }

        public override void Dispose()
        {
            
        }

        public override void KeyPressed(KeyPayload payload)
        {
            
        }

        public override void KeyReleased(KeyPayload payload)
        {
            
        }

        public async override void OnTick()
        {
            if (_flightDataUtil.ReadSharedMem() != null)
            {
                string formattedData = FormatFlightData(_flightDataUtil.GetEngineFlightData(settings.engineDataType));
                await Connection.SetTitleAsync(formattedData);
            }
            else
            {
                await Connection.SetTitleAsync("_");
            }
        }
        #region Private Methods

        private Task SaveSettings()
        {
            return Connection.SetSettingsAsync(JObject.FromObject(settings));
        }

        private string FormatFlightData(float data)
        {
            string formattedData = "0.00";
            switch (settings.decimalFormat)
            {
                case "0":
                    formattedData = data >= 0 ? data.FormatDecimal(0) : "0.00";
                    break;
                case "1":
                    formattedData = data >= 0 ? data.FormatDecimal(1) : "0.0";
                    break;
                case "2":
                    formattedData = data >= 0 ? data.FormatDecimal(2) : "0.00";
                    break;
                case "n":
                    formattedData = data >= 0 ? data.ToString() : "0.00";
                    break;
            }
            return formattedData;
        }

        #endregion
    }
}
