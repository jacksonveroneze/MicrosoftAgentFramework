using System.ComponentModel;

namespace SimpleAgent;

public static class WeatherTool
{
    private const string DescriptionClass = "Obtém a temperatura atual de uma determinada localização.";
    private const string DescriptionFunc = "Cidade de onde deseja obter a temperatura.";

    [Description(DescriptionClass)]
    public static string GetWeather(
        [Description(DescriptionFunc)] string location)
    {
        return $"A temperatura em {location} é 35 gráus.";
    }
}