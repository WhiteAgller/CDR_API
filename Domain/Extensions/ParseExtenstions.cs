using System.Globalization;

namespace Domain.Extensions;

public static class ParseExtenstions
{
    public static DateTime ParseToDateTime(this string dateTime)
    {
        try
        {
            return DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cannot convert value {dateTime} to DateTime.");
            throw;
        }
    }
    
    public static TimeSpan ParseToTimeSpan(this string time)
    {
        try
        {
            return TimeSpan.Parse(time);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cannot convert value {time} to TimeSpan.");
            throw;
        }
    }
    
    public static T ParseToCurrency<T>(this string currency) where T : struct
    {
        try
        {
            return Enum.Parse<T>(currency);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Invalid currency {currency}.");
            throw;
        }
    }
    
    public static int ParseToInt(this string number)
    {
        try
        {
            return Convert.ToInt32(number); 
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cannot convert value {number} to int.");
            throw;
        }
    }
    
    public static decimal ParseToDecimal(this string number)
    {
        try
        {
            return decimal.Parse(number, NumberStyles.Number, CultureInfo.InvariantCulture); 
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cannot convert value {number} to decimal.");
            throw;
        }
    }
    
    
}