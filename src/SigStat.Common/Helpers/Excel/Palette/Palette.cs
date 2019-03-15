using System.Drawing;

namespace SigStat.Common.Helpers.Excel.Palette
{
    class Palette
    {
        public Color MainColor { get; set; }
        public Color DarkColor { get; set; }
        public Color LightColor { get; set; }

        public Palette(Color main, Color dark, Color light)
        {
            MainColor = main;
            DarkColor = dark;
            LightColor = light;
        }
    }

}
