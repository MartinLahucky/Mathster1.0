using System;
using System.Collections.Generic;
using System.Linq;
using Mathster.Resources.Exercises;
using Mathster.Resources.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseCounting : ContentPage
    {
        private byte id;
        private long beginTime;
        private Exercise[] queue;
        private bool under = false;
        private bool underChanged = false;

        public ExerciseCounting(byte id, Exercise[] queue)
        {
            InitializeComponent();
            MenuButton.IconImageSource = "menu_icon.png";
            UnderButton.IconImageSource = "under_icon.png";
            NextButton.Text = ">";
            SubmitButton.Text = Localization.Submit;
            ResultLabelInput.Text = String.Empty;
            ResultInput.Text = String.Empty;
            beginTime = DateTime.UtcNow.Ticks;
            ExerciseLabel.Text = queue[id].Assignment;

            this.queue = queue;
            this.id = id;

            
            switch (queue[id].ExerciseType)
            {
                case 1:
                    SetTitle(Localization.Addition);
                    break;

                case 2:
                    SetTitle(Localization.Subtraction);
                    break;

                case 3:
                    SetTitle(Localization.Multiplication);
                    break;

                case 4:
                    SetTitle(Localization.Division);
                    break;

                case 5:
                    SetTitle(Localization.Equation);
                    UnderButton.IconImageSource = "";
                    UnderButton.IsEnabled = false;
                    UnderButton.Priority = 0;
                    MenuButton.Priority = 1;
                    break;
            }

            if (id < queue.Length - 1)
            {
                SubmitButton.IsVisible = false;
            }
            else
            {
                NextButton.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResultInput.Focus();
        }

        private void SetTitle(string operation)
        {
            Title = $"{operation} | {id + 1}/{queue.Count()}";
        }

        private async void MenuButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
        }

        private void UnderButton_OnClicked(object sender, EventArgs e)
        {
            switch (under)
            {
                case false:
                    SecondLayer.Margin = new Thickness(60, -40, 60, 0);
                    ExerciseLabel.Text = queue[id].AssignmentUnder;
                    ResultInput.FlowDirection = FlowDirection.RightToLeft;
                    under = true;
                    break;
                case true:
                    SecondLayer.Margin = new Thickness(60, 30, 60, 0);
                    ExerciseLabel.Text = queue[id].Assignment;
                    ResultInput.FlowDirection = FlowDirection.LeftToRight;
                    under = false;
                    break;
            }

            ResultInput.Focus();
        }

        private void ResultInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // I have no idea what I'm doing 
            // Creating list with number and navigation in the number 
            List<string> num = new List<string>();
            string numText = ResultInput.Text;
            // Check, if number not empty
            if (ResultInput.Text == "")
            {
                underChanged = false;
                ResultLabelInput.Text = String.Empty;
            }
            else
            {
                try
                {
                    switch (under)
                    {
                        case true:
                            // Loading correct number
                            for (int i = 0; i < numText.Length; i++)
                            {
                                num.Add(numText[i].ToString());
                            }

                            // If the number is negative, writes minus 1st  
                            if (num[0] == "-")
                            {
                                num.Remove("-");
                                num.Add("-");
                            }

                            // Writing first character 
                            ResultLabelInput.Text = num[num.Count - 1];
                            // Check if not only 1 symbol 
                            if (num.Count != 1)
                            {
                                for (int i = num.Count - 2; i >= 0; i--)
                                {
                                    ResultLabelInput.Text += num[i];
                                }
                            }

                            underChanged = true;
                            break;

                        case false:
                            // Not reversing number when under mode changed
                            if (underChanged)
                            {
                                // Adding last character e.NewTextValue[e.NewTextValue.Length -1]
                                numText = ResultLabelInput.Text + e.NewTextValue[e.NewTextValue.Length - 1];
                                ResultInput.Text = numText;
                                underChanged = false;
                            }

                            // Loading correct number format
                            for (int i = 0; i < numText.Length; i++)
                            {
                                num.Add(numText[i].ToString());
                            }

                            // Again IDK what magic is this  
                            ResultLabelInput.Text = num[0];
                            if (num.Count != 1)
                            {
                                for (int i = 1; i < numText.Length; i++)
                                {
                                    ResultLabelInput.Text += num[i];
                                }
                            }

                            break;
                    }
                }
                catch
                {
                    ResultLabelInput.Text = String.Empty;
                    ResultInput.Text = String.Empty;
                }
            }
        }

        private async void NextButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                queue[id].UserInput = float.Parse(ResultLabelInput.Text);
                queue[id].CountLenght = DateTime.UtcNow.Ticks - beginTime;
                Button button = (Button) sender;

                if (button.Text == ">")
                {
                    id++;
                    await Navigation.PushAsync(new ExerciseCounting(id, queue));
                    var existingPages = Navigation.NavigationStack.ToList();
                    foreach (var page in existingPages)
                    {
                        Navigation.RemovePage(page);
                    }
                }
                else
                {
                    await Navigation.PushAsync(new Summary(queue, false));
                    var existingPages = Navigation.NavigationStack.ToList();
                    foreach (var page in existingPages)
                    {
                        Navigation.RemovePage(page);
                    }
                }
            }
            catch // (Exception exception)
            {
                // await DisplayAlert(AppResource.Upozorneni, exception.Message, AppResource.Ok);
                await DisplayAlert(Localization.Alert, Localization.AlertInputNumber, Localization.Ok);
            }
        }
    }
}
