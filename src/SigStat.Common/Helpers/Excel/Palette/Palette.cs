using System.Drawing;

namespace SigStat.Common.Helpers.Excel
{
    /// <summary>
    /// 
    /// </summary>
    public class Palette
    {
        /// <summary>
        /// Gets or sets the main color used in the palette
        /// </summary>
        public Color MainColor { get; set; }
        /// <summary>
        /// Gets or sets the color for rendering darker elements
        /// </summary>
        public Color DarkColor { get; set; }
        /// <summary>
        /// Gets or sets the color for rendering bright elements
        /// </summary>
        public Color LightColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Palette"/> class.
        /// </summary>
        /// <param name="main">The main color</param>
        /// <param name="dark">The dark color</param>
        /// <param name="light">The light color</param>
        public Palette(Color main, Color dark, Color light)
        {
            MainColor = main;
            DarkColor = dark;
            LightColor = light;
        }
    }

}
