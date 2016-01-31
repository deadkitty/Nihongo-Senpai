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

namespace SenpaiCreationKit.Controls
{
    /// <summary>
    /// Interaktionslogik für InsertItem.xaml
    /// </summary>
    public partial class InsertItem : UserControl
    {
        private static Color color;
        private static int insertCount = 0;
        private static Random rand = new Random();

        private bool isInsertText = false;
        private int insertIndex = 0;

        public char Character { get; set; }

        public bool IsInsertText
        {
            get { return isInsertText; }
            set
            {
                if(value)
                {
                    Background = GetSelectedColor();
                    insertIndex = insertCount;
                }
                else
                {
                    Background = null;
                    insertIndex = 0;
                }
                isInsertText = value;
            }
        }

        public int InsertIndex
        {
            get { return insertIndex; }
        }

        public InsertItem()
        {
            InitializeComponent();
        }

        public InsertItem(char sign)
        {
            InitializeComponent();

            Character = sign;
            charTextbox.Text += sign;
        }

        private static SolidColorBrush GetSelectedColor()
        {
            return new SolidColorBrush(color);
        }

        public static void ChangeSelectedColor()
        {
            color = Color.FromRgb((byte)rand.Next( 50, 255),
                                  (byte)rand.Next(150, 255),
                                  (byte)rand.Next(100, 255));
            ++insertCount;
        }
    }
}
