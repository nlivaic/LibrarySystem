using System.Collections.Generic;

namespace LibrarySystem.Application.Services.Sorting
{
    public class PropertyMappingValue
    {
        public string SourcePropertyName { get; set; }
        public IEnumerable<string> TargetPropertyNames { get; set; }
        public bool Revert { get; set; }

        public PropertyMappingValue(string sourcePropertyName, IEnumerable<string> targetPropertyNames, bool revert)
        {
            SourcePropertyName = sourcePropertyName;
            TargetPropertyNames = targetPropertyNames;
            Revert = revert;
        }
    }
}
