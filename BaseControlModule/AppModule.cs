using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseControlModule
{
    public class AppModule : IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;
        IUnityContainer _container;
        public AppModule(IRegionViewRegistry registry,IUnityContainer container)
        {
            this.regionViewRegistry = registry;
            _container = container;
        }

        public void Initialize()
        {
          
            //regionViewRegistry.RegisterViewWithRegion("Button", typeof(Views.ButtonControl));
            //regionViewRegistry.RegisterViewWithRegion("TextBox", typeof(Views.TextBoxControl));

            _container.RegisterType<object, Views.TextBoxControl>("TextBox");
            _container.RegisterType<object, Views.ButtonControl>("Button");
        }
    }
}
