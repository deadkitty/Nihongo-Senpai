using SenpaiMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SenpaiMarketplace.Models
{
    public class SenpaiDatabase : DbContext
    {
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Word>   Words   { get; set; }
        public DbSet<Kanji>  Kanjis  { get; set; }
        public DbSet<Cloze>  Clozes  { get; set; }

        public SenpaiDatabase()
            : base("name=SenpaiDatabase")
        {

        }
    }
}