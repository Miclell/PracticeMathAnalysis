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
        private readonly string URLQ = $"https://api.wolframalpha.com/v1/query?appid=6R3EPU-YGJU7TPXK8&input=";

        private string FunctionBody { get; set; }
        private decimal A { get; set; }
        private decimal B { get; set; }

        /// <summary>
        /// Метод подсчета количества интервалов
        /// </summary>
        public decimal CalculateIntervals()
        {
            decimal diff = B - A;
            decimal result = diff;

            for (int i = 0; i < 2; i++)
                result *= diff;
            
            return result;
        }

        /// <summary>
        /// Метод подсчета определенного интеграла
        /// </summary>
        public async Task<decimal> CalculateIntegral()
        {
            NumberFormatInfo nfi = new() { NumberDecimalSeparator = "." };

            string pattern = $"integrate[{FunctionBody.ToString().Replace(',', '.')},{{x,{A.ToString().Replace(',', '.')},{B.ToString().Replace(',', '.')}}}]";
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(URLQ + pattern);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            result = Regex.Match(result, "[0-9.^]*(?=</plaintext>)").Value;

            if (result.Contains('^'))
            {
                response = await client.GetAsync(URL + result);
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
                if (result.Contains("rds"))
                    return Convert.ToDecimal(Regex.Match(result, "[0-9]*(?= )").Value, nfi) / Convert.ToDecimal(Regex.Match(result, "[0-9]*(?=rds)").Value, nfi);

                if (result.Contains("times"))
                    return Convert.ToDecimal(Regex.Match(result, "[0-9.]*(?= times)").Value, nfi)
                        * Convert.ToDecimal(Math.Pow(10, Convert.ToInt32(Regex.Match(result, "(?<=the )[0-9]*").Value, nfi)));

                if (result.Contains("negative"))
                    return -Convert.ToDecimal(result[(result.IndexOf("negative") + 9)..], nfi);
            }

            return Convert.ToDecimal(result, nfi);
        }

        /// <summary>
        /// Метод подсчета определенного интеграла при помощи формулы Симпсона
        /// </summary>
        /// <param name="intervals">Количество интервалов</param>     
        public async Task<decimal> SimpsonMethod(decimal intervals = 0m, bool trigger = false)
        {
            NumberFormatInfo nfi = new() { NumberDecimalSeparator = "." };

            string pattern;
            if (trigger)
                pattern = $"integrate {FunctionBody.ToString().Replace(',', '.')} using Simpson's rule with {intervals.ToString().Replace(',', '.')} intervals from x = {A.ToString().Replace(',', '.')} to {B.ToString().Replace(',', '.')}";
            else
                pattern = $"integrate {FunctionBody.ToString().Replace(',', '.')} using Simpson's rule with {CalculateIntervals().ToString().Replace(',', '.')} intervals from x = {A.ToString().Replace(',', '.')} to {B.ToString().Replace(',', '.')}";


            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(URLQ + pattern);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            result = Regex.Match(result, "(?<=<plaintext>)[0-9.^]*(?=</plaintext>)").Value;
            
            if (result.Contains('^'))
            {
                response = await client.GetAsync(URL + result);
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
                if (result.Contains("rds"))
                    return Convert.ToDecimal(Regex.Match(result, "[0-9]*(?= )").Value, nfi) / Convert.ToDecimal(Regex.Match(result, "[0-9]*(?=rds)").Value, nfi);

                if (result.Contains("times"))
                    return Convert.ToDecimal(Regex.Match(result, "[0-9.]*(?= times)").Value, nfi)
                        * Convert.ToDecimal(Math.Pow(10, Convert.ToInt32(Regex.Match(result, "(?<=the )[0-9]*").Value, nfi)));

                if (result.Contains("negative"))
                    return -Convert.ToDecimal(result[(result.IndexOf("negative") + 9)..], nfi);
            }

            return Convert.ToDecimal(result, nfi);
        }
    }
}