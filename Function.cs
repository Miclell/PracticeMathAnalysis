using System.Globalization;
using System.Text.RegularExpressions;

namespace PracticeMathAnalysis
{
    internal class Function
    {
        public Function(string functionBody, decimal a, decimal b)
        {
            FunctionBody = functionBody.Replace("+", " plus ").Replace("-", " minus ");
            A = a;
            B = b;
        }

        private readonly string URL = $"https://api.wolframalpha.com/v1/result?appid=6R3EPU-YGJU7TPXK8&i=";

        private string FunctionBody { get; set; }
        private decimal A { get; set; }
        private decimal B { get; set; }

        /// <summary>
        /// Метод подстановки и вычесления значения функции
        /// </summary>
        private async Task<decimal> Subs(decimal parametr)
        {
            NumberFormatInfo nfi = new() { NumberDecimalSeparator = "." };

            string pattern = $"{FunctionBody},x=={Convert.ToString(parametr, nfi)}";

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(URL + pattern);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();

            if (result.Contains("rds"))
                return Convert.ToDecimal(Regex.Match(result, "[0-9]*(?= )").Value, nfi) / Convert.ToDecimal(Regex.Match(result, "[0-9]*(?=rds)").Value, nfi);

            if (result.Contains("times"))
                return Convert.ToDecimal(Regex.Match(result, "[0-9.]*(?= times)").Value, nfi)
                    * Convert.ToDecimal(Math.Pow(10, Convert.ToInt32(Regex.Match(result, "(?<=the )[0-9]*").Value, nfi)));

            return Convert.ToDecimal(result, nfi);
        }

        /// <summary>
        /// Подсчет определенного интеграла
        /// </summary>
        public async Task<decimal> CalculateIntegral()
        {
            NumberFormatInfo nfi = new() { NumberDecimalSeparator = "." };

            string pattern = $"Integrate[{FunctionBody},{{x,{A},{B}}}]";
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(URL + pattern);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            pattern = result;
            response = await client.GetAsync(URL + pattern);
            response.EnsureSuccessStatusCode();
            result = await response.Content.ReadAsStringAsync();

            if (result.Contains("rds"))
                return Convert.ToDecimal(Regex.Match(result, "[0-9]*(?= )").Value, nfi) / Convert.ToDecimal(Regex.Match(result, "[0-9]*(?=rds)").Value, nfi);

            if (result.Contains("times"))
                return Convert.ToDecimal(Regex.Match(result, "[0-9.]*(?= times)").Value, nfi)
                    * Convert.ToDecimal(Math.Pow(10, Convert.ToInt32(Regex.Match(result, "(?<=the )[0-9]*").Value, nfi)));

            return Convert.ToDecimal(result, nfi);
        }

        /// <summary>
        /// Метод подсчета определенного интеграла при помощи формулы Симпсона
        /// </summary>
        /// <returns></returns>
        public decimal SimpsonMethod() =>
            (B - A) / 6 * (Subs(A).Result + Subs((A + B) / 2).Result * 4m + Subs(B).Result);        
    }
}