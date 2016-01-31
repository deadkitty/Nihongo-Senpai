using SenpaiCreationKit.Controls;
using SenpaiCreationKit.Data;
using SenpaiCreationKit.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace SenpaiCreationKit.Pages
{
    /// <summary>
    /// Interaktionslogik für EditInsertLessonPage.xaml
    /// </summary>
    public partial class EditInsertLessonPage : Page
    {
        #region Fields
        
        private Lesson lesson = null;

        private List<Cloze> newClozes      = new List<Cloze>();
        private List<Cloze> clozesToDelete = new List<Cloze>();

        private DetailItem selectedItem = null;

        #endregion

        #region Constructor

        public EditInsertLessonPage(Lesson lesson)
        {
            InitializeComponent();

            InputLanguageManager.Current.CurrentInputLanguage = CultureInfo.GetCultureInfo("ja-JP");

            InsertItem.ChangeSelectedColor();

            this.lesson = lesson;
        }

        #endregion

        #region Initialize
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lessonNameTextbox.Text = lesson.Name;

            foreach (Cloze cloze in lesson.Clozes)
            {
                clozesListbox.Items.Add(new DetailItem(cloze));
            }
        }

        #endregion

        #region Button Click

        private void chooseInsertParts_Click(object sender, RoutedEventArgs e)
        {
            foreach (InsertItem item in selectInsertsListbox.SelectedItems)
            {
                item.IsInsertText = true;
            }

            InsertItem.ChangeSelectedColor();

            selectInsertsListbox.SelectedItems.Clear();
        }

        private void removeInsertParts_Click(object sender, RoutedEventArgs e)
        {
            foreach (InsertItem item in selectInsertsListbox.SelectedItems)
            {
                item.IsInsertText = false;
            }
        }

        private void createCloze_Click(object sender, RoutedEventArgs e)
        {
            bool result;

            if (clozesListbox.SelectedItem == null)
            {
                result = TryCreateCloze();
            }
            else
            {
                result = TryUpdateCloze();
            }

            if (!result)
            {
                MessageBox.Show(AppResources.SomeInsertPartsAreMissing);
            }
            else
            {
                selectInsertsListbox.Items.Clear();

                sentenceTextbox.Clear();

                //hintsContainer.Children.Clear();

                sentenceTextbox.Focus();

                selectedItem = null;
                clozesListbox.SelectedItems.Clear();
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(AppResources.Really, AppResources.DeleteWords, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                while (clozesListbox.SelectedItems.Count > 0)
                {
                    DetailItem item = clozesListbox.SelectedItems[0] as DetailItem;

                    clozesListbox.Items.Remove(item);

                    //if the cloze was no new cloze than add it to the delete list to make sure to delete realy remove it from the database later
                    if (!newClozes.Remove(item.sourceCloze))
                    {
                        clozesToDelete.Add(item.sourceCloze);
                        lesson.Words.Remove(item.sourceWord);
                    }
                }
            }
        }

        private void chooseAll_Click(object sender, RoutedEventArgs e)
        {
            clozesListbox.SelectAll();
        }

        private void editLesson_Click(object sender, RoutedEventArgs e)
        {
            if (lessonNameTextbox.Text == "")
            {
                MessageBox.Show(AppResources.LessonNameEmpty);
            }
            else
            {
                lesson.Name = lessonNameTextbox.Text;
                lesson.Size += newClozes.Count - clozesToDelete.Count;

                DataManager.UpdateInsertLesson(lesson, newClozes, clozesToDelete);
            
                NavigationService.GoBack();
            }
        }

        private void cancelEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(AppResources.CancelCreation, AppResources.Really, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                NavigationService.GoBack();
            }
        }

        #endregion

        #region Others

        private void sentenceListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectInsertsListbox.Items.Clear();

            sentenceTextbox.Clear();
            //hintsContainer.Children.Clear();

            if (clozesListbox.SelectedItems.Count > 0)
            {
                selectedItem = clozesListbox.SelectedItem as DetailItem;

                RestoreClozeView(selectedItem.sourceCloze);
            }
            else
            {
                selectedItem = null;
            }
        }

        private void sentenceTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sentenceTextbox.Text == "")
                {
                    MessageBox.Show(AppResources.SentenceTextboxCantBeEmpty);
                }
                else
                {
                    selectInsertsListbox.Items.Clear();
                    foreach (char insert in sentenceTextbox.Text)
                    {
                        selectInsertsListbox.Items.Add(new InsertItem(insert));
                    }

                    //hintsTextbox.Focus();
                }
            }
        }

        #endregion

        #region Helper

        private bool TryCreateCloze()
        {
            Cloze cloze = new Cloze();

            CreateCloze(cloze);

            if (cloze.Inserts == "")
            {
                return false;
            }

            newClozes.Add(cloze);
            clozesListbox.Items.Add(new DetailItem(cloze));

            return true;
        }

        private void CreateCloze(Cloze cloze)
        {
            StringBuilder textBuilder = new StringBuilder();
            StringBuilder insertsBuilder = new StringBuilder();

            for (int i = 0; i < selectInsertsListbox.Items.Count - 1; ++i)
            {
                InsertItem item1 = selectInsertsListbox.Items[i] as InsertItem;
                InsertItem item2 = selectInsertsListbox.Items[i + 1] as InsertItem;

                if (item1.InsertIndex == 0 && item2.InsertIndex == 0)
                {
                    textBuilder.Append(item1.Character);
                }

                if (item1.InsertIndex == 0 && item2.InsertIndex > 0)
                {
                    textBuilder.Append(item1.Character);
                    textBuilder.Append('_');
                    insertsBuilder.Append('_');
                }

                if (item1.InsertIndex > 0 && item2.InsertIndex > 0)
                {
                    insertsBuilder.Append(item1.Character);

                    if (item1.InsertIndex != item2.InsertIndex)
                    {
                        textBuilder.Append('_');
                        insertsBuilder.Append('_');
                    }
                }

                if (item1.InsertIndex > 0 && item2.InsertIndex == 0)
                {
                    insertsBuilder.Append(item1.Character);
                }
            }

            InsertItem item = selectInsertsListbox.Items[selectInsertsListbox.Items.Count - 1] as InsertItem;

            if (item.InsertIndex > 0)
            {
                insertsBuilder.Append(item.Character);
            }
            else
            {
                textBuilder.Append(item.Character);
            }

            cloze.Text = textBuilder.ToString();
            cloze.Inserts = insertsBuilder.ToString().TrimStart('_');
            //cloze.Hints = hintsTextbox.Text.Replace("、", "_");
        }

        private bool TryUpdateCloze()
        {
            Cloze cloze = selectedItem.sourceCloze;

            CreateCloze(cloze);

            if (cloze.Inserts == "")
            {
                return false;
            }

            (clozesListbox.SelectedItem as DetailItem).Update();

            return true;
        }

        private void RestoreClozeView(Cloze cloze)
        {
            StringBuilder textBuilder = new StringBuilder();
            StringBuilder insertsBuilder = new StringBuilder();

            String[] textParts = cloze.Text.Split('_');
            String[] insertParts = cloze.Inserts.Split('_');

            for (int i = 0; i < insertParts.Length; ++i)
            {
                textBuilder.Append(textParts[i]);
                textBuilder.Append(insertParts[i]);
            }

            textBuilder.Append(textParts.Last());

            sentenceTextbox.Text = textBuilder.ToString();
            //hintsTextbox.Text = cloze.Hints.Replace('_', '、');

            int insertsIndex = 0;

            foreach (char sign in cloze.Text)
            {
                InsertItem item;

                if (sign == '_')
                {
                    foreach (char insert in insertParts[insertsIndex])
                    {
                        item = new InsertItem(insert);
                        item.IsInsertText = true;

                        selectInsertsListbox.Items.Add(item);
                    }

                    InsertItem.ChangeSelectedColor();
                    ++insertsIndex;
                }
                else
                {
                    item = new InsertItem(sign);

                    selectInsertsListbox.Items.Add(item);
                }
            }
        }

        #endregion
    }
}
