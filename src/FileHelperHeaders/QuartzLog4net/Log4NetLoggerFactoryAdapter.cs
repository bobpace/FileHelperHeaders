using System;
using System.Collections.Specialized;
using System.IO;
using Common.Logging;
using Common.Logging.Factory;
using log4net.Config;
using ILog = log4net.ILog;
using LogManager = log4net.LogManager;

namespace FileHelperHeaders.QuartzLog4net
{
    public class Log4NetLoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter
    {
        readonly ILog4NetRuntime _runtime;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="properties">configuration properties, see <see cref="Log4NetLoggerFactoryAdapter"/> for more.</param>
        public Log4NetLoggerFactoryAdapter(NameValueCollection properties)
            : this(properties, new Log4NetRuntime())
        {
        }

        /// <summary>
        /// Constructor accepting configuration properties and an arbitrary 
        /// <see cref="ILog4NetRuntime"/> instance.
        /// </summary>
        /// <param name="properties">configuration properties, see <see cref="Log4NetLoggerFactoryAdapter"/> for more.</param>
        /// <param name="runtime">a log4net runtime adapter</param>
        protected Log4NetLoggerFactoryAdapter(NameValueCollection properties, ILog4NetRuntime runtime)
            : base(true)
        {
            if (runtime == null)
            {
                throw new ArgumentNullException("runtime");
            }
            _runtime = runtime;

            // parse config properties
            var configType = properties.GetValue("configType", string.Empty).ToUpper();
            var configFile = properties.GetValue("configFile", string.Empty);

            // app-relative path?
            if (configFile.StartsWith("~/") || configFile.StartsWith("~\\"))
            {
                configFile = string.Format("{0}/{1}", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\'), configFile.Substring(2));
            }

            if (configType == "FILE" || configType == "FILE-WATCH")
            {
                if (configFile == string.Empty)
                {
                    throw new ConfigurationException("Configuration property 'configFile' must be set for log4Net configuration of type 'FILE' or 'FILE-WATCH'.");
                }

                if (!File.Exists(configFile))
                {
                    throw new ConfigurationException("log4net configuration file '" + configFile + "' does not exists");
                }
            }

            switch (configType)
            {
                case "INLINE":
                    _runtime.XmlConfiguratorConfigure();
                    break;
                case "FILE":
                    _runtime.XmlConfiguratorConfigure(configFile);
                    break;
                case "FILE-WATCH":
                    _runtime.XmlConfiguratorConfigureAndWatch(configFile);
                    break;
                case "EXTERNAL":
                    // Log4net will be configured outside of Common.Logging
                    break;
                default:
                    _runtime.BasicConfiguratorConfigure();
                    break;
            }
        }

        /// <summary>
        /// Create a ILog instance by name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected override Common.Logging.ILog CreateLogger(string name)
        {
            return new Log4NetLogger(_runtime.GetLogger(name));
        }

        public interface ILog4NetRuntime
        {
            void BasicConfiguratorConfigure();
            ILog GetLogger(string name);
            void XmlConfiguratorConfigure();
            void XmlConfiguratorConfigure(string configFile);
            void XmlConfiguratorConfigureAndWatch(string configFile);
        }

        class Log4NetRuntime : ILog4NetRuntime
        {
            public void BasicConfiguratorConfigure()
            {
                BasicConfigurator.Configure();
            }

            public ILog GetLogger(string name)
            {
                return LogManager.GetLogger(name);
            }

            public void XmlConfiguratorConfigure()
            {
                XmlConfigurator.Configure();
            }

            public void XmlConfiguratorConfigure(string configFile)
            {
                XmlConfigurator.Configure(new FileInfo(configFile));
            }

            public void XmlConfiguratorConfigureAndWatch(string configFile)
            {
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
            }
        }
    }
}