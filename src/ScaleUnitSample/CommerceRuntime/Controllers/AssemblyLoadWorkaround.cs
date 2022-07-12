using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Contoso.CommerceRuntime.Controllers
{
    public static class AssemblyLoadWorkaround
    {
        private static bool initialized = false;
        public static void Init()
        {
            if (!initialized)
            {
                initialized = true;
                AppDomain.CurrentDomain.AssemblyResolve +=
                    (sender, args) =>
                    {
                        var assemblyNameNoVersion = Regex.Match(args.Name, "[a-zA-Z0-9.]+");
                        if(assemblyNameNoVersion.Success && assemblyNameNoVersion.Value.Length > 0)
                        {
                            return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(asm => asm.FullName.StartsWith(assemblyNameNoVersion.Value));
                        }
                        return null;
                    };
            }
        }
    }
}
