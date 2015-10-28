using System;

using Xamarin.Forms;

namespace SKTest
{
    public class HomeScreenView : ContentPage
    {
        public HomeScreenView ()
        {
            Content = new StackLayout { 
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}


