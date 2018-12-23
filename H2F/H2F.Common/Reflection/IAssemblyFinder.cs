using System;
using System.Collections.Generic;
using System.Text;
//
using System.Reflection;
namespace H2F.Standard .Common.Reflection
{
    public interface IAssemblyFinder
    {
        List<Assembly> GetAllAssemblies();
    }
}
