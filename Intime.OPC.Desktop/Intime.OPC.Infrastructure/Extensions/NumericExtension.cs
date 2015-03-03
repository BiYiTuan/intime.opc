using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.Extensions
{
    public static class NumericExtension
    {
        private static string[] UpperNumbers ={ "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖", "拾" };
        private static string[] Units ={ "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "万" };
        private static string[] CurrencyUnits ={ "角", "分" };

        public static string ToChinese(this double number)
        {
            bool containsDecimal = false;//是否含有小数，默认没有(0则视为没有)
            bool containsInteger = true;//是否含有整数,默认有(0则视为没有)

            string wholeString;//整个数字字符串
            string integerPart;//整数部分
            string decimalPart = "";//小数部分
            string currentNumericChar;//当前的数字字符
            string result = "";//返回的字符串

            number = Math.Round(number, 2);//四舍五入取两位

            //各种非正常情况处理
            if (number < 0) throw new ArgumentException("不支持负数的转换", "number");
            if (number > 9999999999999.99) throw new ArgumentException("超过最大支持的数值", "number");
            if (number == 0)

                return UpperNumbers[0];

            //判断是否有整数
            if (number < 1.00)
                containsInteger = false;

            wholeString = number.ToString();

            integerPart = wholeString;//默认只有整数部分
            if (integerPart.Contains("."))
            {//分开整数与小数处理
                integerPart = wholeString.Substring(0, wholeString.IndexOf("."));
                decimalPart = wholeString.Substring((wholeString.IndexOf(".") + 1), (wholeString.Length - wholeString.IndexOf(".") - 1));
                containsDecimal = true;
            }


            if (decimalPart == "" || int.Parse(decimalPart) <= 0)
            {//判断是否含有小数部分
                containsDecimal = false;
            }

            if (containsInteger)
            {//整数部分处理
                integerPart = Reverse(integerPart);//反转字符串

                for (int a = 0; a < integerPart.Length; a++)
                {//整数部分转换
                    currentNumericChar = integerPart.Substring(a, 1);
                    if (int.Parse(currentNumericChar) != 0)
                        result = UpperNumbers[int.Parse(currentNumericChar)] + Units[a] + result;
                    else if (a == 0 || a == 4 || a == 8)
                    {
                        if (integerPart.Length > 8 && a == 4)
                            continue;
                        result = Units[a] + result;
                    }
                    else if (int.Parse(integerPart.Substring(a - 1, 1)) != 0)
                        result = UpperNumbers[int.Parse(currentNumericChar)] + result;

                }

                if (!containsDecimal)
                    return result + "整";
            }

            for (int b = 0; b < decimalPart.Length; b++)
            {//小数部分转换
                currentNumericChar = decimalPart.Substring(b, 1);
                if (int.Parse(currentNumericChar) != 0)
                    result += UpperNumbers[int.Parse(currentNumericChar)] + CurrencyUnits[b];
                else if (b != 1 && containsInteger)
                    result += UpperNumbers[int.Parse(currentNumericChar)];
            }

            return result;
        }

        private static string Reverse(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
