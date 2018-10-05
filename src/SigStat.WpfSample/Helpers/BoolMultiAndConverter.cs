using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SigStat.WpfSample.Helpers
{
    public class BoolMultiAndConverter : IMultiValueConverter
    {
        private bool isNorm;
        private bool isNormChecked;
        private bool isCenteringChecked;
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //parameter: true --> normalization, false --> bias
            isNorm = (bool)parameter;

            if (isNorm)
            {
                isNormChecked = (bool)values[0];
                isCenteringChecked = (bool)values[1];
            }
            else
            {
                isCenteringChecked = (bool)values[0];
                isNormChecked = (bool)values[1];
            }

            return isNorm ? isNormChecked && !isCenteringChecked : isCenteringChecked && !isNormChecked;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            isNorm = (bool)parameter;
            if (isNorm)
                return new object[] { value, isCenteringChecked };
            else
                return new object[] { value, isNormChecked };
        }
    }
}
