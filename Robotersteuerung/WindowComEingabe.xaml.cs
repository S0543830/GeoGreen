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

namespace RoboterKinect
{
    /// <summary>
    /// Interaktionslogik für WindowComEingabe.xaml
    /// </summary>
    public partial class WindowComEingabe : Window
    {
        public WindowComEingabe()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBoxEnter(object sender, TouchEventArgs e)
        {
            if(textBox.Text!= "")
            { }
        }
    }
}
