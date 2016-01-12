﻿using SenpaiCreationKit.Properties;
using SenpaiCreationKit.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SenpaiCreationKit.Data
{
    public class SenpaiDatabase : DataContext
    {
        #region Fields

        public Table<Lesson> Lessons;
        public Table<Word> Words;
        public Table<Cloze> Clozes;
        public Table<Kanji> Kanjis;

        #endregion

        #region Constructor

        public SenpaiDatabase()
            //: base("Data Source=(LocalDB)\v11.0;AttachDbFilename=SenpaiDatabase.mdf;Integrated Security=True")
            : base(AppResources.dbConnection)
        {

        }

        #endregion

        #region Select Items

        public IQueryable<Lesson> GetLessons()
        {
            return from c in Lessons orderby c.Name select c;
        }

        public IQueryable<Lesson> GetLessons(Lesson.EType type)
        {
            return from c in Lessons where c.Type == (int)type orderby c.Name select c;
        }

        public IQueryable<Lesson> GetLessons(String substring)
        {
            return from c in Lessons where c.Name.Contains(substring) orderby c.Name select c;
        }

        public IQueryable<Lesson> GetLessons(Lesson.EType type, String substring)
        {
            return from c in Lessons where c.Type == (int)type && c.Name.Contains(substring) orderby c.Name select c;
        }

        public Lesson GetLesson(int id)
        {
            return (from c in Lessons where c.id == id select c).FirstOrDefault();
        }

        public Word GetWord(int id)
        {
            return (from c in Words where c.id == id select c).FirstOrDefault();
        }

        public Kanji GetKanji(int id)
        {
            return (from c in Kanjis where c.id == id select c).FirstOrDefault();
        }

        public Cloze GetCloze(int id)
        {
            return (from c in Clozes where c.id == id select c).FirstOrDefault();
        }

        #endregion

        #region Helper

        public void SubmitChanges(ConflictMode failureMode, bool trackChanges)
        {
            //i have to track changes this way, i need the id of each item that i want to track
            //but the id is set after submitChanges, so i have to save the inserts, etc. 
            //because the changeSet is gonna be reseted after submitChanges.
            //after submitchanges i track the changes, insert them and submit again
            //i also can't make an association between the ExportData and the other items, because
            //i want it one way, from exportData to item, but that seems not be possible for now.
            //i would had to make the association in both directions, but therefore i would had to 
            //add an exportData member to the other items and i don't want this -.-"
            //IList<object> inserts = null;
            //IList<object> updates = null;
            //IList<object> deletes = null;

            //if (trackChanges)
            //{
            //    ChangeSet changeSet = GetChangeSet();

            //    inserts = changeSet.Inserts;
            //    updates = changeSet.Updates;
            //    deletes = changeSet.Deletes;
            //}

            base.SubmitChanges(failureMode);

            //if (trackChanges)
            //{
            //    TrackChanges(inserts, updates, deletes);
            //    base.SubmitChanges(failureMode);
            //}
        }

        //private void TrackChanges(IList<object> inserts, IList<object> updates, IList<object> deletes)
        //{
        //    if (inserts.Count > 0)
        //    {
        //        TrackInserts(inserts);
        //    }

        //    if (updates.Count > 0)
        //    {
        //        TrackUpdates(updates);
        //    }

        //    if (deletes.Count > 0)
        //    {
        //        TrackDeletes(deletes);
        //    }
        //}

        //private void TrackInserts(IList<object> inserts)
        //{
        //    IEnumerable<Lesson> newLessons = inserts.OfType<Lesson>();
        //    IEnumerable<Word> newWords = inserts.OfType<Word>();
        //    IEnumerable<Kanji> newKanjis = inserts.OfType<Kanji>();
        //    IEnumerable<Cloze> newClozes = inserts.OfType<Cloze>();

        //    List<ExportData> exports = new List<ExportData>();

        //    //if the insert has a new lesson i just have to track the lesson, the items beneatf are inside this lesson
        //    if (newLessons.Count() > 0)
        //    {
        //        foreach (Lesson lesson in newLessons)
        //        {
        //            ExportData data = new ExportData(lesson.Id, ExportData.EItemType.newLesson);
        //            exports.Add(data);
        //        }
        //    }
        //    else //if i just add some new items to a given lesson i don't have to track the lesson but the newItems to this lesson/s
        //    {
        //        foreach (Word word in newWords)
        //        {
        //            ExportData data = new ExportData(word.Id, ExportData.EItemType.newWord);
        //            exports.Add(data);
        //        }

        //        foreach (Kanji kanji in newKanjis)
        //        {
        //            ExportData data = new ExportData(kanji.Id, ExportData.EItemType.newKanji);
        //            exports.Add(data);
        //        }

        //        foreach (Cloze cloze in newClozes)
        //        {
        //            ExportData data = new ExportData(cloze.Id, ExportData.EItemType.newCloze);
        //            exports.Add(data);
        //        }
        //    }

        //    exportData.InsertAllOnSubmit(exports);
        //}

        //private void TrackUpdates(IList<object> updates)
        //{
        //    IEnumerable<Lesson> updateLessons = updates.OfType<Lesson>();
        //    IEnumerable<Word> updateWords = updates.OfType<Word>();
        //    IEnumerable<Kanji> updateKanjis = updates.OfType<Kanji>();
        //    IEnumerable<Cloze> updateClozes = updates.OfType<Cloze>();

        //    List<ExportData> exports = new List<ExportData>();

        //    foreach (Lesson lesson in updateLessons)
        //    {
        //        ExportData data = new ExportData(lesson.Id, ExportData.EItemType.updatedLesson);
        //        exports.Add(data);
        //    }

        //    foreach (Word word in updateWords)
        //    {
        //        ExportData data = new ExportData(word.Id, ExportData.EItemType.updatedWord);
        //        exports.Add(data);
        //    }

        //    foreach (Kanji kanji in updateKanjis)
        //    {
        //        ExportData data = new ExportData(kanji.Id, ExportData.EItemType.updatedKanji);
        //        exports.Add(data);
        //    }

        //    foreach (Cloze cloze in updateClozes)
        //    {
        //        ExportData data = new ExportData(cloze.Id, ExportData.EItemType.updatedCloze);
        //        exports.Add(data);
        //    }

        //    exportData.InsertAllOnSubmit(exports);
        //}

        //private void TrackDeletes(IList<object> deletes)
        //{
        //    IEnumerable<Lesson> deletedLessons = deletes.OfType<Lesson>();
        //    IEnumerable<Word> deletedWords = deletes.OfType<Word>();
        //    IEnumerable<Kanji> deletedKanjis = deletes.OfType<Kanji>();
        //    IEnumerable<Cloze> deletedClozes = deletes.OfType<Cloze>();

        //    List<ExportData> exports = new List<ExportData>();

        //    if (deletedLessons.Count() > 0)
        //    {
        //        foreach (Lesson lesson in deletedLessons)
        //        {
        //            ExportData data = new ExportData(lesson.Id, ExportData.EItemType.deletedLesson);
        //            exports.Add(data);
        //        }
        //    }
        //    else
        //    {
        //        foreach (Word word in deletedWords)
        //        {
        //            ExportData data = new ExportData(word.Id, ExportData.EItemType.deletedWord);
        //            exports.Add(data);
        //        }

        //        foreach (Kanji kanji in deletedKanjis)
        //        {
        //            ExportData data = new ExportData(kanji.Id, ExportData.EItemType.deletedKanji);
        //            exports.Add(data);
        //        }

        //        foreach (Cloze cloze in deletedClozes)
        //        {
        //            ExportData data = new ExportData(cloze.Id, ExportData.EItemType.deletedCloze);
        //            exports.Add(data);
        //        }
        //    }

        //    exportData.InsertAllOnSubmit(exports);
        //}

        #endregion
    }
}