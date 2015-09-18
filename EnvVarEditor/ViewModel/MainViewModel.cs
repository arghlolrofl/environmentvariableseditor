using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using EnvVarEditor.Model;

namespace EnvVarEditor.ViewModel {
    public class MainViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<EnvVar> envVars;
        public List<EnvVar> EnvVars {
            get { return envVars; }
            set {
                if (envVars == value) return;

                envVars = value;
                RaisePropertyChanged("EnvVars");
            }
        }

        private EnvVar selectedVar;
        public EnvVar SelectedVar {
            get { return selectedVar; }
            set {
                if (selectedVar == value) return;

                selectedVar = value;
                RaisePropertyChanged("SelectedVar");
            }
        }

        public MainViewModel() {
            resetVars();
        }

        protected void RaisePropertyChanged(string pName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(pName));
        }

        private void resetVars() {
            EnvVars = new List<EnvVar>();

            var dict = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User)
                                  .Cast<DictionaryEntry>()
                                  .ToDictionary(
                                        item => item.Key.ToString(), item => item.Value.ToString()
                                  );

            var sortedItems = from pair in dict
                              orderby pair.Key
                              select pair;

            foreach(var item in sortedItems)
                EnvVars.Add(new EnvVar(item.Key, item.Value));
        }

        internal void SaveChanges() {
            string key = SelectedVar.Name;
            string value = SelectedVar.Value;

            if (value.Contains(Environment.NewLine))
                value = value.Replace(Environment.NewLine, ";");

            try {
                Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.User);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Update successful!");
            
            resetVars();
            SelectedVar = EnvVars.SingleOrDefault(v => v.Name == key);
        }
    }
}
