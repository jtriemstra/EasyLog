using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EasyLogListener
{
    public class AppConfigWrapper
    {
        public static String ParameterByEnvironment(int intEnvironmentCode)
        {
            return ConfigurationManager.AppSettings["parameters_" + intEnvironmentCode];
        }

        public static String WorkingDirectory
        {
            get { return ConfigurationManager.AppSettings["working_directory"]; }
        }

        public static String ShellLogin
        {
            get { return ConfigurationManager.AppSettings["shell_login"]; }
        }

        public static String ShellBuild
        {
            get { return ConfigurationManager.AppSettings["shell_build"]; }
        }

        public static String ShellDownload
        {
            get { return ConfigurationManager.AppSettings["shell_download"]; }
        }
    }
}
