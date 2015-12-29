using NihongoSenpai.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Data
{
    public interface IVocabItem
    {
        Word SourceWord { get; set; }
        String ShownText { get; }
        String HiddenText1 { get; }
        String HiddenText2 { get; }
        String DescriptionText { get; }

        int TimeStamp { get; set; }
        int Repetition { get; set; }
        int NextInterval { get; set; }
        float EFactor { get; set; }

        void InitText();
        void WriteBack();
    }
}
