using NihongoSenpaiTsu.Common;
using NihongoSenpaiTsu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpaiTsu.Pages
{
    public interface ISelectLessonPage
    {
        void Initialize(SelectLessonsPage page);
        void Deinitialize();

        List<Lesson> LoadLessons();
        void LoadLessons(List<Lesson> lessons);

        void ChangePracticeDirection(EPracticeDirection direction);
        void SaveSettings();
    }
}
