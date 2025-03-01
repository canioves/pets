using System.Runtime.CompilerServices;
using System.Text.Json;

namespace currency_converter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(
                "Enter code of base currency. Press 'Enter' to continue (by default, base currency is 'USD'):"
            );
            string baseCurrency = Console.ReadLine();
            if (string.IsNullOrEmpty(baseCurrency))
                baseCurrency = "USD";
            CurrencyApi api = new CurrencyApi(baseCurrency);
            Console.Clear();

            Console.WriteLine("Enter amount of base currency: ");
            float floatBaseAmount = 0;
            bool isValid = false;
            while (!isValid)
            {
                string stringBaseAmount = Console.ReadLine();
                if (float.TryParse(stringBaseAmount, out float floatValue))
                {
                    floatBaseAmount = floatValue;
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input, try again...");
                }
            }
            Console.Clear();

            Console.WriteLine("Enter the code of the currency you want to convert to:");
            string convertingCurrency = Console.ReadLine();
            string json = await api.GetCurrencyValue(convertingCurrency);
            Currency currencyData = JsonSerializer.Deserialize<Currency>(json);
            float convertCurrencyValue = currencyData.Data[convertingCurrency].Value;
            Console.Clear();

            Console.WriteLine(
                $"{floatBaseAmount} {baseCurrency} = {floatBaseAmount * convertCurrencyValue} {convertingCurrency}"
            );
        }
    }
}
