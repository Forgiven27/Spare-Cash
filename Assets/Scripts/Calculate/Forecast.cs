using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Forecast 
{
    public static float CalculateAverage(List<float> values)
    {
        if (values == null || values.Count == 0)
        {
            return 0;
        }
        return values.Average();
    }
    public static List<float> GenerateAverageForecast(List<float> values, int forecastDays)
    {
        float average = CalculateAverage(values);
        List<float> forecast = new List<float>();
        for (int i = 0;i < forecastDays; i++)
        {
            forecast.Add(average);
            values.AddRange(forecast);
            average = CalculateAverage(values);
        }
        return forecast;
    }
}
