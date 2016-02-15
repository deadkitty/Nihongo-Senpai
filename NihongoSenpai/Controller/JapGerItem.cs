using NihongoSenpai.Model;
using NihongoSenpai.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Controller
{
    public class JapGerItem : IVocabItem
    {
        private Word source;

        private int timestamp;
        private int lastRound;
        private int nextRound;
        private float eFactor;

        private String shownText;
        private String hiddenText1;
        private String hiddenText2;
        private String descriptionText;

        public JapGerItem(Word source)
        {
            this.source = source;
            this.timestamp = source.timeStampJapanese;
            this.lastRound = source.lastRoundJapanese;
            this.nextRound = source.nextRoundJapanese;
            this.eFactor = source.eFactorJapanese;
        }
        
        #region ToString

        public override string ToString()
        {
            return "JapGerItem = " +  source.ToString();
        }

        #endregion

        #region IVocabItem

        public Word SourceWord
        {
            get { return source; }
            set { source = value; }
        }

        public string ShownText
        {
            get { return shownText; }
        }

        public string HiddenText1
        {
            get { return hiddenText1; }
        }
        
        public string HiddenText2
        {
            get { return hiddenText2; }
        }

        public string DescriptionText
        {
            get { return descriptionText; }
        }

        public int TimeStamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public int LastRound
        {
            get { return lastRound; }
            set { lastRound = value; }
        }

        public int NextRound
        {
            get { return nextRound; }
            set { nextRound = value; }
        }

        public float EFactor
        {
            get { return eFactor; }
            set { eFactor = value; }
        }
        
        public void InitText()
        {
            if(source.Type == Word.EType.grammar)
            {
                shownText   = source.kana;
                hiddenText1 = source.translation;
                hiddenText2 = source.kanji;
            }
            else
            {
                if (source.kanji == "")
                {
                    shownText   = source.kana;
                    hiddenText1 = "";
                    hiddenText2 = source.translation;
                }
                else
                {
                    shownText   = source.kanji;
                    hiddenText1 = source.kana + "、";
                    hiddenText2 = source.translation;
                }
            }

            switch (source.ShowDescription)
            {
                case Word.EShowFlags.dontShow    : descriptionText = Word.GetTypeString(source.Type); break;
                case Word.EShowFlags.justGerman  : descriptionText = Word.GetTypeString(source.Type); break;
                case Word.EShowFlags.justJapanese: descriptionText = source.ToDescriptionString(); break;
                case Word.EShowFlags.showBoth    : descriptionText = source.ToDescriptionString(); break;
            }
        }

        public void WriteBack()
        {
            source.timeStampJapanese = timestamp;
            source.lastRoundJapanese = lastRound;
            source.nextRoundJapanese = nextRound;
            source.eFactorJapanese   = eFactor;
        }

        #endregion
    }
}
