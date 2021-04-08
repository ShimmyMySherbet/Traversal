using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SDG.Unturned;

namespace Traversal.Models
{
    public class Scope
    {
        public List<KeyValuePair<Regex, string>> Variables = new List<KeyValuePair<Regex, string>>();

        public void AddVariable(string variable)
        {
            variable = variable.ToLower();
            Variables.Add(new KeyValuePair<Regex, string>(FindFilesPatternToRegex.Convert(variable), variable));

            UnturnedLog.info("Mount var: [{var}]", variable);
        }

        public bool CheckSync(string key)
        {
            key = key.ToLower();

            bool v = false;

            foreach(var vr in Variables)
            {
                bool res = vr.Key.IsMatch(key);

                UnturnedLog.info("Scope [{i}] == [{v}] {r}", key, vr.Value, res);
                if (res)
                {
                    v = true;
                    break;
                }
            }


            //bool v = Variables.Any(x => x.Match(key).Success);
            UnturnedLog.info("Check Scope [{key}]: {v}", key, v);
            return v;
        }

        public bool this[string key]
        {
            get => CheckSync(key);
        }
    }
}