﻿using NihongoSenpai.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Model
{
    public interface IVocabItem
    {
        Word SourceWord { get; set; }
        String ShownText { get; }
        String HiddenText1 { get; }
        String HiddenText2 { get; }
        String DescriptionText { get; }

        int TimeStamp { get; set; }
        int LastRound { get; set; }
        int NextRound { get; set; }
        float EFactor { get; set; }

        void InitText();
        void WriteBack();
    }
}
