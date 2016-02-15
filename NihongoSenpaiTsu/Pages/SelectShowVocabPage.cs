using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NihongoSenpaiTsu.Model;
using NihongoSenpaiTsu.Common;

namespace NihongoSenpaiTsu.Pages
{
    public class SelectShowVocabPage : ISelectLessonPage
    {
        public void ChangePracticeDirection(EPracticeDirection direction)
        {
            throw new NotImplementedException();
        }

        public void Deinitialize()
        {
            throw new NotImplementedException();
        }

        public void Initialize(SelectLessonsPage page)
        {
            
        }

        public List<Lesson> LoadLessons()
        {
            return new List<Lesson>();
        }

        public void LoadLessons(List<Lesson> lessons)
        {
            
        }

        public void SaveSettings()
        {
            
        }
    }
}
