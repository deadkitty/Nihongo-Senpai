using SenpaiCreationKit.Data;
using System.Windows.Controls;

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

            sourceWord  = word;
            sourceKanji = null;
            sourceCloze = null;

            mainTextblock.Text = word.ToDetailString();
            descTextblock.Text = word.ToDescriptionString();
        }

        public DetailItem(Kanji kanji)
        {
            InitializeComponent();

            sourceWord  = null;
            sourceKanji = kanji;
            sourceCloze = null;

            mainTextblock.Text = kanji.ToDetailString();
            descTextblock.Text = kanji.ToExampleString();
        }

        public DetailItem(Cloze cloze)
        {
            InitializeComponent();

            sourceKanji = null;
            sourceWord  = null;
            sourceCloze = cloze;

            mainTextblock.Text = cloze.ToString();
            descTextblock.Text = cloze.Inserts;// + "、" + cloze.Hints;
        }

        public void Update()
        {
            if (sourceWord != null)
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
            mainTextblock.Text = word.ToDetailString();
            descTextblock.Text = word.ToDescriptionString();
        }

        public void Update(Kanji kanji)
        {
            mainTextblock.Text = kanji.ToDetailString();
            descTextblock.Text = kanji.ToExampleString();
        }

        public void Update(Cloze cloze)
        {
            mainTextblock.Text = cloze.ToString();
            descTextblock.Text = cloze.Inserts;// + "、" + cloze.Hints;
        }
    }
}
