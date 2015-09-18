using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EnvVarEditor.ViewModel;

namespace EnvVarEditor.View {
    public partial class MainWindow : Window {
        private MainViewModel _mainViewModel;

        public MainWindow() {
            InitializeComponent();

            this.DataContext = _mainViewModel = new MainViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            _mainViewModel.SaveChanges();
        }
    }
}
