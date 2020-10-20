using Ex01.Models;
using Ex01.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ex01.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrelloCardsPage : ContentPage
    {
        public TrelloList ListContent { get; set; }
        public TrelloBoard BoardContent { get; set; }

        public TrelloCardsPage(TrelloBoard board, TrelloList list)
        {
            InitializeComponent();
            this.ListContent = list;
            this.BoardContent = board;
            lblListName.Text = BoardContent.Name;

            // handle tap on label
            TapGestureRecognizer gesture = new TapGestureRecognizer();
            gesture.Tapped += Gesture_Tapped;
            lblAddCard.GestureRecognizers.Add(gesture);
        }

        protected override void OnAppearing()
        {
            LoadCardsAsync();

            base.OnAppearing();
        }

        private void Gesture_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SingleCardPage(BoardContent, ListContent));
        }

        private async Task LoadCardsAsync()
        {
            lvwCards.ItemsSource = await TrelloRepository.GetTrelloCardsAsync(ListContent.ListId);
        }

        private void btnGoBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void BtnCloseCard_Clicked(object sender, EventArgs e)
        {
            //get listview item in which this button was clicked + convert to trello card
            TrelloCard card = (sender as Button).BindingContext as TrelloCard;

            //UPDATE the status of the card to closed
            card.IsClosed = true;
            //SEND the update TO the API
            await TrelloRepository.UpdateCardAsync(card);

            //AFTER (see: await) updating the card, reload the list of cards
            LoadCardsAsync();
        }
    }
}