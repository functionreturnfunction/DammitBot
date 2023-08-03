using System.Linq;
using Lamar;
using Lamar.IoC.Instances;
using log4net;

namespace DammitBot.Ioc;

public class LoggerConvention : ConfiguredInstancePolicy
{
    protected override void apply(IConfiguredInstance instance)
    {
        var parameters = instance.ImplementationType
            .GetConstructors()
            .SelectMany(x => x.GetParameters())
            .Where(param => param.ParameterType == typeof(ILog));

        foreach (var param in parameters)
        {
            instance.Ctor<ILog>().Is(LogManager.GetLogger(instance.ImplementationType));
        }
    }
}