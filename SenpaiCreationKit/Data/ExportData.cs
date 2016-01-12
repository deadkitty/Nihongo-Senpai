using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 0649

namespace SenpaiCreationKit.Data
{
    [Table]
    public class ExportData
    {
        #region EItemType
        
        public enum EItemType
        {
            newLesson,
            newWord,
            newKanji,
            newCloze,
            updatedLesson,
            updatedWord,
            updatedKanji,
            updatedCloze,
            deletedLesson,
            deletedWord,
            deletedKanji,
            deletedCloze,
            count,
            undefined = -1,
        }

        #endregion

        #region Fields

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        internal int id;

        [Column]
        internal int itemID;
        
        [Column]
        internal int itemType;
        
        #endregion

        #region Properties
        
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        public EItemType Type
        {
            get { return (EItemType)itemType; }
            set { itemType = (int)value; }
        }

        #endregion

        #region Constructor

        public ExportData()
        {

        }

        public ExportData(int itemID, EItemType itemType)
        {
            this.itemID = itemID;
            this.Type = itemType;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            String item = "";
            //switch(this.Type)
            //{
            //    case EItemType.newLesson: item = DataManager.Database.GetLesson(itemID).ToString(); break;
            //    case EItemType.newWord: item = DataManager.Database.GetWord(itemID).ToDetailString(); break;
            //    case EItemType.newKanji: item = DataManager.Database.GetKanji(itemID).ToDetailString(); break;
            //    case EItemType.updatedLesson: item = DataManager.Database.GetLesson(itemID).ToString(); break;
            //    case EItemType.updatedWord: item = DataManager.Database.GetWord(itemID).ToDetailString(); break;
            //    case EItemType.updatedKanji: item = DataManager.Database.GetKanji(itemID).ToDetailString(); break;
            //    case EItemType.deletedLesson: item = itemID.ToString(); break;
            //    case EItemType.deletedWord: item = itemID.ToString(); break;
            //    case EItemType.deletedKanji: item = itemID.ToString(); break;
            //    case EItemType.deletedCloze: item = itemID.ToString(); break;
            //}

            return Type + " - " + item;
        }

        #endregion
    }
}
