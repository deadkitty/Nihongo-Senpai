using SenpaiCreationKit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SenpaiCreationKit.Controls
{
    /// <summary>
    /// Interaktionslogik für SelectLessonControl.xaml
    /// </summary>
    public partial class SelectLessonsControl : UserControl
    {
        public IPageUpdater pageUpdater;

        public List<Lesson> selectedLessons = new List<Lesson>();

        public SelectLessonsControl()
        {
            InitializeComponent();
        }

        public void FillPostLessons(List<Lesson> lessons, List<Lesson> uploadedLessons)
        {
            selectedLessons.Clear();
            lessonsListbox.Items.Clear();

            foreach(Lesson lesson in lessons)
            {
                bool addLesson = true;

                foreach(Lesson uploaded in uploadedLessons)
                {
                    if (lesson.id == uploaded.id)
                    {
                        addLesson = false;
                        break;
                    }
                }

                if(addLesson)
                {
                    lessonsListbox.Items.Add(lesson);
                }
            }
        }

        public void FillPutLessons(List<Lesson> lessons, List<Lesson> uploadedLessons)
        {
            selectedLessons.Clear();
            lessonsListbox.Items.Clear();

            foreach (Lesson lesson in lessons)
            {
                bool addLesson = false;

                foreach (Lesson uploaded in uploadedLessons)
                {
                    if (lesson.id == uploaded.id)
                    {
                        addLesson = true;
                        break;
                    }
                }

                if (addLesson)
                {
                    lessonsListbox.Items.Add(lesson);
                }
            }
        }

        public void FillDeleteLessons(List<Lesson> uploadedLessons)
        {
            selectedLessons.Clear();
            lessonsListbox.Items.Clear();

            foreach (Lesson lesson in uploadedLessons)
            {
                lessonsListbox.Items.Add(lesson);
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;

            selectedLessons.Clear();
        }

        private void select_Click(object sender, RoutedEventArgs e)
        {
            if (lessonsListbox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Lektionen auswählen!!!");
            }
            else
            {
                Visibility = Visibility.Collapsed;
                
                foreach(Lesson lesson in lessonsListbox.SelectedItems)
                {
                    selectedLessons.Add(lesson);
                }

                pageUpdater.UpdatePage();
            }
        }
    }
}
