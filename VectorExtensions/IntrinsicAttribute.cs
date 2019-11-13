using System;
using System.Collections.Generic;
using System.Text;

namespace VectorExtensions
{
    [AttributeUsage(AttributeTargets.Method)]
    public class IntrinsicAttribute : Attribute
    {
        public IntrinsicAttribute(string method) => Method = method;
        public string Method { get; }
    }
}
