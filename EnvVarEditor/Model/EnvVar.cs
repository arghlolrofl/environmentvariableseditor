using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvVarEditor.Model {
    public class EnvVar {
        public string Name { get; set; }
        public string Value { get; set; }

        public EnvVar(string k, string v) {
            Name = k;

            if (v.Contains(";")) {
                Value = v.Replace(";", Environment.NewLine);
            } else {
                Value = v;
            }
        }

        public override string ToString() {
            return Name;
        }
    }
}
