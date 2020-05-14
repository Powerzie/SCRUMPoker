using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SPWPF.CustomControls.NumChoiser
{
    /// <summary>
    /// Interaction logic for NumChoiser.xaml
    /// </summary>
    public partial class NumChoiser : UserControl
    {
        public string _numberToSelect{get;set;}
         public  string NumberToSelect { get { return _numberToSelect; } set { _numberToSelect = value; NumText.Content = _numberToSelect; } }
        public NumChoiser(int num)
        {
            InitializeComponent();
            NumberToSelect = num.ToString();
        }
        public NumChoiser()
        {
            InitializeComponent();
        }
    }
}
