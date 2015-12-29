using SenpaiCreationKit.Data;
using SenpaiCreationKit.Pages;
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
    /// Interaktionslogik für DetailWordItem.xaml
    /// </summary>
    public partial class DetailItem : UserControl
    {
        public Word sourceWord;
        public Kanji sourceKanji;
        public Cloze sourceCloze;

        public DetailItem()
        {
            InitializeComponent();
        }

        public DetailItem(Word word)
        {
            InitializeComponent();

            this.sourceWord = word;
            this.sourceKanji = null;
            this.sourceCloze = null;

            wordTextblock.Text = word.ToDetailString();
            descriptionTextblock.Text = word.ToDescriptionString();
        }

        public DetailItem(Kanji kanji)
        {
            InitializeComponent();

            this.sourceKanji = kanji;
            this.sourceWord = null;
            this.sourceCloze = null;

            wordTextblock.Text = kanji.ToDetailString();
            descriptionTextblock.Text = kanji.ToExampleString();
        }

        public DetailItem(Cloze cloze)
        {
            InitializeComponent();

            this.sourceKanji = null;
            this.sourceWord = null;
            this.sourceCloze = cloze;
            
            wordTextblock.Text = cloze.ToString();
            descriptionTextblock.Text = cloze.inserts + "、" + cloze.hints;
        }
        public void Update()
        {
            if(sourceWord != null)
            {
                Update(sourceWord);
            }
            else if (sourceKanji != null)
            {
                Update(sourceKanji);
            }
            else
            {
                Update(sourceCloze);
            }
        }

        public void Update(Word word)
        {
            wordTextblock.Text = word.ToDetailString();
            descriptionTextblock.Text = word.ToDescriptionString();
        }

        public void Update(Kanji kanji)
        {
            wordTextblock.Text = kanji.ToDetailString();
            descriptionTextblock.Text = kanji.ToExampleString();
        }
        
        public void Update(Cloze cloze)
        {
            wordTextblock.Text = cloze.ToString();
            descriptionTextblock.Text = cloze.inserts + "、" + cloze.hints;
        }
    }
}
