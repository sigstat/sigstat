using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SigStat.WpfSample.Helpers
{
    public class FeatureBoolConverter : IValueConverter
    {
        private List<FeatureDescriptor> target;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            target = (List<FeatureDescriptor>)value;
            return target.Contains((FeatureDescriptor)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var featureDescriptor = (FeatureDescriptor)parameter;
            if((bool) value)
            {
                if (!target.Contains(featureDescriptor))
                    target.Add(featureDescriptor);
                return target;
            }
            else
            {
                if (target.Contains(featureDescriptor))
                    target.Remove(featureDescriptor);
                return target;
            }
        }

        
    }
}
