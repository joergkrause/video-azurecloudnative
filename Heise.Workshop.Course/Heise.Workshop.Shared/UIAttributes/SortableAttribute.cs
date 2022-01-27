using System;
using System.Collections.Generic;
using System.Text;

namespace Heise.Workshop.Shared.UIAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SortableAttribute : Attribute
    {
    }
}
