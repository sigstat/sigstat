using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public class PenUpDuration
    {
        private double _startTime;
        private double _endTime;
        private bool isStartInitialized = false;
        private bool isEndInitialized = false;

        public double StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                isStartInitialized = true;
                if (isEndInitialized)
                {
                    Length = _endTime - _startTime;
                }
            }
        }
        public double EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                isEndInitialized = true;
                if (isStartInitialized)
                {
                    Length = _endTime - _startTime;
                }
            }
        }

        public double Length { get; private set; }

    }
}
