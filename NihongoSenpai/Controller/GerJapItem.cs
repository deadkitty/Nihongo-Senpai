using NihongoSenpai.Data;
using NihongoSenpai.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Controller
{
    public class GerJapItem : IVocabItem
    {
        private Word source;

        private int timestamp;
        private int repetition;
        private int nextInterval;
        private float eFactor;

        private String shownText;
        private String hiddenText1;
        private String hiddenText2;
        private String descriptionText;

        public GerJapItem(Word source)
        {
            this.source = source;
            this.timestamp = source.timeStampTransl;
            this.repetition = source.repetitionTranslation;
            this.nextInterval = source.nextIntervalTranslation;
            this.eFactor = source.eFactorTranslation;
        }

        #region ToString

        public override string ToString()
        {
            return "GerJapItem = " + source.ToString();
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

        public int Repetition
        {
            get { return repetition; }
            set { repetition = value; }
        }

        public int NextInterval
        {
            get { return nextInterval; }
            set { nextInterval = value; }
        }

        public float EFactor
        {
            get { return eFactor; }
            set { eFactor = value; }
        }
        
        public void InitText()
        {
            shownText = source.translation;

            if(source.kanji == "")
            {
                hiddenText1 = source.kana;
                hiddenText2 = "";
            }
            else
            {
                hiddenText1 = source.kanji + "、";
                hiddenText2 = source.kana;
            }
            
            switch (source.ShowDescription)
            {
                case Word.EShowFlags.dontShow    : descriptionText = Word.GetTypeString(source.Type); break;
                case Word.EShowFlags.justGerman  : descriptionText = source.ToDescriptionString(); break;
                case Word.EShowFlags.justJapanese: descriptionText = Word.GetTypeString(source.Type); break;
                case Word.EShowFlags.showBoth    : descriptionText = source.ToDescriptionString(); break;
            }
        }

        public void WriteBack()
        {
            source.timeStampTransl = timestamp;
            source.repetitionTranslation = repetition;
            source.nextIntervalTranslation = nextInterval;
            source.eFactorTranslation = eFactor;
        }

        #endregion
    }
}
