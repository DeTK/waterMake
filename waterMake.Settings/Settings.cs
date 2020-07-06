using CirnoLib.Settings;

namespace waterMake.Settings
{
    public static class Settings
    {
        private static readonly CryptoRegistryComponent Reg = new CryptoRegistryComponent(@"DeTK\waterMake", "DeTK", true);

        public static int FontSize
        {
            get => Reg.GetInt32(nameof(FontSize), 30);
            set => Reg.SetValue(nameof(FontSize), value);
        }

        public static string FontName
        {
            get => Reg.GetString(nameof(FontName), "굴림");
            set => Reg.SetValue(nameof(FontName), value);
        }

        public static int FontStyle
        {
            get => Reg.GetInt32(nameof(FontStyle), 0);
            set => Reg.SetValue(nameof(FontStyle), value);
        }

        public static int FontColor
        {
            get => Reg.GetInt32(nameof(FontColor), 0x000000);
            set => Reg.SetValue(nameof(FontColor), value);
        }
        
        public static int FontOutline
        {
            get => Reg.GetInt32(nameof(FontOutline), 0);
            set => Reg.SetValue(nameof(FontOutline), value);
        }
        
        public static string WaterMarkString
        {
            get => Reg.GetString(nameof(WaterMarkString), "워터마크");
            set => Reg.SetValue(nameof(WaterMarkString), value);
        }
        public static bool Auto
        {
            get => Reg.GetBoolean(nameof(WaterMarkString), false);
            set => Reg.SetValue(nameof(WaterMarkString), value);
        }
    }
}