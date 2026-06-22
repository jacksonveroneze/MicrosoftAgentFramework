using Anthropic;
using Microsoft.Extensions.AI;
using SimpleAgent;

var url = new Uri("http://localhost:11434");

var key = "";

// var agent = Factories.FactoryAnthopic(key)
var agent = Factories.FactoryOllama(url)
    .AsAIAgent(
        model: "claude-haiku-4-5",
        instructions:
        """
        Atue como especialista na previsão do tempo.
        """,
        tools:
        [
            AIFunctionFactory.Create(WeatherTool.GetWeather)
        ]
    );

do
{
    Console.WriteLine("Faça uma pergunta");

    var input = Console.ReadLine();

    var prompt = input ?? string.Empty;

    await foreach (var token in agent.RunStreamingAsync(prompt))
    {
        Console.Write(token);
    }

    Console.WriteLine(string.Empty);
    Console.WriteLine("-> END");
    Console.WriteLine(string.Empty);
} while (true);