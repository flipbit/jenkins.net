using System;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace Hudson.Core
{
    /// <summary>
    /// Wrapper for the Castle Project Windsor IoC Container
    /// </summary>
    public sealed class Windsor : IDisposable
    {
        #region Private

        private static readonly Windsor instance = new Windsor();

        private readonly IWindsorContainer container;

        private Windsor()
        {
            try
            {
                container = new WindsorContainer(new XmlInterpreter(new AssemblyResource("assembly://Hudson.Tray/Hudson.Tray.Configuration.Hudson.config")));            
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        #endregion

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Windsor Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets the value of a given type from the DI container.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public object GetValue(Type type)
        {
            try
            {
                return container[type];
            }
            catch (Exception ex)
            {
                
                throw ex;
            }            
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            container.Dispose();
        }
    }
}