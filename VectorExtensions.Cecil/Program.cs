using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

namespace VectorExtensions.Cecil
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = "VectorExtensions.dll";
            const string startingDLLLocation = "C:";
            var module = ModuleDefinition.ReadModule(startingDLLLocation + "\\" + fileName);
            var types = module.Types.Where(t => t.Name.StartsWith("Vector"));
            var operatorMethods = types.SelectMany(t => t.Methods).Where(m => m.Name.StartsWith("op_"));
            foreach (var method in operatorMethods)
            {
                method.IsSpecialName = true;
            }
            var binPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            module.Write(binPath + "\\" + fileName);
        }
    }
}
