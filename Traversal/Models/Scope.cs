using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Traversal.Models
{
    public class Scope
    {
        public List<Regex> Variables = new List<Regex>();

        public void AddVariable(string variable)
        {
            variable = variable.ToLower();
            Variables.Add(FindFilesPatternToRegex.Convert(variable));
        }

        public bool CheckSync(string key)
        {
            key = key.ToLower();
            return Variables.Any(x => x.Match(key).Success);
        }

        public bool this[string key]
        {
            get => CheckSync(key);
        }
    }
}