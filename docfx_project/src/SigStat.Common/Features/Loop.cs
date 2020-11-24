using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;
using SigStat.Common.Helpers.Serialization;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a loop in a signature
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Loop
    {
        /// <summary>
        /// The geometrical center of the looop
        /// </summary>
        
        public PointF Center { get; set; }
        /// <summary>
        /// The bounding rectangle of the loop
        /// </summary>

        [JsonConverter(typeof(RectangleFConverter))]
        public RectangleF Bounds { get; set; }
        /// <summary>
        /// A list of defining points of the loop
        /// </summary>
        
        public List<PointF> Points { get; set; }

        /// <summary>
        /// Creates a <see cref="Loop"/> instance
        /// </summary>
        public Loop()
        {

        }

        /// <summary>
        /// Creates a <see cref="Loop"/> instance and initializes the <see cref="Center"/> property
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        public Loop(float centerX, float centerY)
        {
            Center = new PointF(centerX, centerY);
        }
        /// <summary>
        /// Returns a string representation of the loop
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Loop " + Center;
        }
    }
}
