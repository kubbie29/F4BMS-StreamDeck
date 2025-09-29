using BarRaider.SdTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.ServiceModel;
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
                instance.cmdsDataType = String.Empty;
                instance.chaffLowText = "CHAFF" + Environment.NewLine + "LO";
                instance.flareLowText = "FLARE" + Environment.NewLine + "LO";

                return instance;
            }

            [JsonProperty(PropertyName = "cmdsDataType")]
            public string cmdsDataType { get; set; }

            [JsonProperty(PropertyName = "chaffLowText")]
            public string chaffLowText { get; set; }

            [JsonProperty(PropertyName = "flareLowText")]
            public string flareLowText { get; set; }


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
            SaveSettings();
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
                string formattedData = FormatFlightData(_flightDataUtil.GetCMDSFlightData(settings.cmdsDataType));
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

        private string FormatFlightData(int data)
        {
            string formattedData = "";

            switch(settings.cmdsDataType)
            {
                case "cc":
                case "fc":
                    formattedData = data >= 0 ? data.ToString() : "0";
                    break;
                case "cl":
                    if (data == 1)
                    {
                        formattedData = settings.chaffLowText;
                    }
                    else formattedData = "";
                    break;
                case "fl":
                    if (data == 1)
                    {
                        formattedData = settings.flareLowText;
                    }
                    else formattedData = "";
                    break;
                case "mode":
                    switch (data)
                    {
                        case 0:
                            formattedData = "OFF";
                            break;
                        case 1:
                            formattedData = "STBY";
                            break;
                        case 2:
                            formattedData = "MAN";
                            break;
                        case 3:
                            formattedData = "SEMI";
                            break;
                        case 4:
                            formattedData = "AUTO";
                            break;
                        case 5:
                            formattedData = "BYP";
                            break;
                    }
                    break;
                default: formattedData = "_";
                    break;
            }
            return formattedData;
        }

        #endregion
    }
}
