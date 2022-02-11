using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace RSP.Game
{
    public class GameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register all ActionEffects
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Effect"))
                .AsSelf()
                .InstancePerLifetimeScope();
       
            base.Load(builder);
        }
    }
}