using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using NihongoSenpai.Controller;
using NihongoSenpai.Data;
using NihongoSenpai.Utilities;
using NihongoSenpai.Controls;
using NihongoSenpai.Data.Database;
using NihongoSenpai.Resources;

namespace NihongoSenpai.Pages
{
    public partial class PracticeConjugationPage : PhoneApplicationPage, IPageUpdater
    {
        #region Fields
	    
        PracticeConjugationItem[] conjugationItems;
 
	    #endregion

        #region Constructor

        public PracticeConjugationPage()
        {
            InitializeComponent();

            ConjugationController.pageUpdater = this;

            conjugationItems = new PracticeConjugationItem[ConjugationData.maxActiveConjugationWordsCount];

            for (int i = 0; i < conjugationItems.Length; ++i)
            {
                conjugationItems[i] = new PracticeConjugationItem();
                conjugationItems[i].Initialize(i, this);

                itemsStackPanel.Children.Insert(i, conjugationItems[i]);
            }

            ConjugationController.GetNextWords();

            UpdateView();
        }
        
        #endregion

        #region Events

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            List<String> answers = new List<String>();

            for (int i = 0; i < ConjugationData.ActiveWords.Count; ++i)
            {
                answers.Add(conjugationItems[i].targetWordTextbox.Text);
            }

            bool[] correctAnswered = ConjugationController.CheckWords(answers.ToArray());

            for (int i = 0; i < correctAnswered.Length; ++i)
            {
                if (!correctAnswered[i])
                {
                    conjugationItems[i].targetWordTextbox.Foreground = new SolidColorBrush(Colors.Red);
                    conjugationItems[i].targetWordTextbox.Text += "（" + ConjugationData.TargetWords[i] + "）";
                }
                else
                {
                    conjugationItems[i].targetWordTextbox.Foreground = new SolidColorBrush(Colors.Green);
                }
            }

            okButton.Visibility = System.Windows.Visibility.Collapsed;
            nextButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            ConjugationController.GetNextWords();
            
            okButton.Visibility = System.Windows.Visibility.Visible;
            nextButton.Visibility = System.Windows.Visibility.Collapsed;

            UpdateView();
        }

        public void UpdateView()
        {
            for (int i = 0; i < ConjugationData.maxActiveConjugationWordsCount; ++i)
            {
                if(i < ConjugationData.ActiveWords.Count)
                {
                    conjugationItems[i].Visibility = System.Windows.Visibility.Visible;
                    conjugationItems[i].UpdateItem(ConjugationData.ActiveWords[i].ToJString(), TargetFormToString(ConjugationData.TargetForms[i]));
                }
                else
                {
                    conjugationItems[i].Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
        
        public void ChangeFocus(int itemID)
        {
            //in the last textbox focus the page to hide the keyboard
            if(itemID + 1 == ConjugationData.ActiveWords.Count)
            {
                this.Focus();
            }
            else //focus the next textbox
            {
                conjugationItems[itemID + 1].Focus();
            }
        }
        
        public static String TargetFormToString(ConjugationData.ETargetForm targetForm)
        {
            switch (targetForm)
            {
                case ConjugationData.ETargetForm.ruForm: return "る - Form";
                case ConjugationData.ETargetForm.naiForm: return "ない - Form";
                case ConjugationData.ETargetForm.naiPastForm: return "ない - Form Past";
                case ConjugationData.ETargetForm.masuForm: return "ます - Form";
                case ConjugationData.ETargetForm.masuNegativeForm: return "ます - Form Negative";
                case ConjugationData.ETargetForm.masuPastForm: return "ます - Form Past";
                case ConjugationData.ETargetForm.masuPastNegativeForm: return "ます - Form Past Negative";
                case ConjugationData.ETargetForm.teForm: return "て - Form";
                case ConjugationData.ETargetForm.taForm: return "た - Form";
                case ConjugationData.ETargetForm.imperativeForm: return "Imperative Form";
                case ConjugationData.ETargetForm.prohibitiveForm: return "Prohibitive Form";
                case ConjugationData.ETargetForm.volitionalForm: return "Volitional Form";
                case ConjugationData.ETargetForm.conditionalForm: return "Conditional Form";
                case ConjugationData.ETargetForm.tai: return "たい - Form";
                case ConjugationData.ETargetForm.sugi: return "すぎ - Form";
                case ConjugationData.ETargetForm.yasui: return "やすい - Form";
                case ConjugationData.ETargetForm.nikui: return "にくい - Form";
                case ConjugationData.ETargetForm.potentialVerb: return "Potential Verb";
                case ConjugationData.ETargetForm.passiveVerb: return "Passive Verb";
                case ConjugationData.ETargetForm.causativeVerb: return "Causasitive Verb";
            }

            return "";
        }

        #endregion

        #region IPageUpdater

        public void UpdatePage()
        {
            throw new NotImplementedException();
        }

        public void RoundFinished()
        {
            if(MessageBox.Show(AppResources.Again, AppResources.RoundFinished, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                ConjugationController.LoadLessons();
                ConjugationController.GetNextWords();
            }
            else
            {
                NavigationService.Navigate(new Uri("/Pages/SelectConjugationPage.xaml", UriKind.Relative));

                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
            }
        }

        public void EndPractice()
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}