using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Model.Database
{
    public class Kanji
    {
        #region Fields
        
        public int id;
                
        public String kanji { get; set; }
        public String meaning { get; set; }
        public String onyomi { get; set; }
        public String kunyomi { get; set; }
        public String example { get; set; }
        public String strokeOrder;

        public float eFactor { get; set; }
        public int lastRound { get; set; }
        public int nextRound { get; set; }
        public int timestamp { get; set; }

        #endregion

        #region Properties


        #endregion

        #region Constructor

        public Kanji()
        {

        }

        public Kanji(Kanji other)
        {
            Fill(other);
        }

        public Kanji(String properties)
        {
            Fill(properties);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return kanji + " - " + meaning + " - " + onyomi + "、" + kunyomi;
        }
        
        /// <summary>
        /// <para>Creates a String to use for export as txt file.</para>
        /// <para>Export Pattern:</para>
        /// <para>id|kanji|meaning|onyomi|kunyomi|example|strokeOrder|efactor|lastRound|nextRound|timestamp</para>
        /// <para>Length = 11</para>
        /// </summary>
        public String ToExportString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(id);
            sb.Append("|");
            sb.Append(kanji);
            sb.Append("|");
            sb.Append(meaning);
            sb.Append("|");
            sb.Append(onyomi);
            sb.Append("|");
            sb.Append(kunyomi);
            sb.Append("|");
            sb.Append(example);
            sb.Append("|");
            sb.Append(strokeOrder);
            sb.Append("|");
            sb.Append(eFactor.ToString(CultureInfo.InvariantCulture));
            sb.Append("|");
            sb.Append(lastRound);
            sb.Append("|");
            sb.Append(nextRound);
            sb.Append("|");
            sb.Append(timestamp);
            
            return sb.ToString();
        }
        
        #endregion
        
        #region Util

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            for (int i = 0; i < parts.Length; ++i)
            {
                switch (i)
                {
                    case  0: id          = Convert.ToInt32 (parts[i]); break;
                    case  1: kanji       =                  parts[i] ; break;
                    case  2: meaning     =                  parts[i] ; break;
                    case  3: onyomi      =                  parts[i] ; break;
                    case  4: kunyomi     =                  parts[i] ; break;
                    case  5: example     =                  parts[i] ; break;
                    case  6: strokeOrder =                  parts[i] ; break;
                    case  7: eFactor     = Convert.ToSingle(parts[i]); break;
                    case  8: lastRound   = Convert.ToInt32 (parts[i]); break;
                    case  9: nextRound   = Convert.ToInt32 (parts[i]); break;
                    case 10: timestamp   = Convert.ToInt32 (parts[i]); break;
                }
            }
        }

        public void Fill(Kanji other)
        {
            kanji       = other.kanji;
            meaning     = other.meaning;
            onyomi      = other.onyomi;
            kunyomi     = other.kunyomi;
            example     = other.example;
            strokeOrder = other.strokeOrder;
            eFactor     = other.eFactor;
            lastRound   = other.lastRound;
            nextRound   = other.nextRound;
            timestamp   = other.timestamp;
        }

        #endregion
    }
}