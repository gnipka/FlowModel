using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FlowModel
{
    internal class InventoryValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = (string)value;
            double parsedNumber;
            double parsedNumberPoint;

            if (value == null || str == "")
                return new ValidationResult(false,
               "Заполните поле");
            else if (str.Contains(","))
            {
                return new ValidationResult(false,
                "Введите значение через точку");
            }
            else if (str[str.Length - 1] == '.' || str[str.Length - 1] == ',')
            {
                return new ValidationResult(false,
                   "Введите корректное число");
            }
            else if (double.TryParse(str, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out parsedNumber))
            {
                if (parsedNumber <= 0)
                {
                    return new ValidationResult(false,
                   "Введите корректное число");
                }
                return ValidationResult.ValidResult;
            }
            else if (double.TryParse(str, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out parsedNumberPoint))
            {
                if (parsedNumberPoint <= 0)
                {                    
                    return new ValidationResult(false,
                   "Введите корректное число");
                }
                value = parsedNumberPoint;
                InventoryPattern = parsedNumberPoint.ToString();
                return ValidationResult.ValidResult;
            }
            else
                return new ValidationResult(false,
               "Введите корректное число");
        }

        public string? InventoryPattern { get; set; }
    }
}
