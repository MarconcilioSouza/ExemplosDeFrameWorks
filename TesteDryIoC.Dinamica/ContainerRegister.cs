using DryIoc;
using FluentValidation;
using System.Linq;
using System.Reflection;

namespace TesteDryIoC.Generic
{
    public static class ContainerRegister
    {
        public static Container getContainer()
        {
            Container container = new Container();

            var implementingClasses =
               Assembly.GetExecutingAssembly()
               .GetTypes().Where(type =>
               type.ImplementsServiceType(typeof(IValidator))
               );

            foreach (var implementingClass in implementingClasses)
            {
                var interfaceValidator = implementingClass.GetImplementedInterfaces()[0];
                container.Register(interfaceValidator, implementingClass);
            }

            return container;
        }
    }
}
