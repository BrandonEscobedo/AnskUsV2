using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.DependencyInjection
{
    public class AppicationAssemblyReference
    {
        internal static readonly Assembly assembly = typeof(AppicationAssemblyReference).Assembly;
    }
}
