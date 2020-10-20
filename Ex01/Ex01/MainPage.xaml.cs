using Ex01.Models;
using Ex01.Repositories;
using Ex01.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ex01
{
    public partial class MainPage : ContentPage
    {
        private static Random _rnd = new Random();


        public MainPage()
        {
            InitializeComponent();

            //TestTrelloRepository();

            LoadBoardsAsync();
        }

        private async Task TestTrelloRepository()
        {
            List<TrelloBoard> boardsList = await TrelloRepository.GetTrelloBoardsAsync();

            //studenten mogen dit op de lange manier doen, maar feel free om de goede
            //  studenten als hint te geven / uit te dagen om ook LINQ te gebruiken:
            TrelloBoard selectedBoard = boardsList.Where(b => b.IsFavorite).ToList<TrelloBoard>().First();


            //2. TRELLOLISTS
            List<TrelloList> trelloLists = await TrelloRepository.GetTrelloListsAsync(selectedBoard.BoardId);

            TrelloList selectedList = trelloLists[_rnd.Next(trelloLists.Count)];

            //3. TRELLOCARDS
            List<TrelloCard> cards = await TrelloRepository.GetTrelloCardsAsync(selectedList.ListId);

            TrelloCard selectedCard = cards[_rnd.Next(cards.Count)];

            //4. SINGLE CARD
            TrelloCard cardDetail = await TrelloRepository.GetCardByIdAsync(selectedCard.CardId);


            //5. ADD/UPDATE/DELETE (studenten mogen dit testen op hun eigen manier)

            string testName = "Look at my fancy card";

            //ADD
            TrelloCard newCard = new TrelloCard { Name = testName, IsClosed = false };
            await TrelloRepository.AddCardAsync(selectedList.ListId, newCard);
            TrelloCard searchCard = (await TrelloRepository.GetTrelloCardsAsync(selectedList.ListId)).Where(c => c.Name.Equals(testName)).FirstOrDefault();
            bool cardAdded = (searchCard != null);

            //DELETE
            if (cardAdded)
            {
                await TrelloRepository.DeleteCardAsync(searchCard.CardId);
            }

            //UPDATE
            string temp = cardDetail.Name;
            cardDetail.Name = testName;
            await TrelloRepository.UpdateCardAsync(cardDetail);
            bool cardUpdated = (await TrelloRepository.GetCardByIdAsync(cardDetail.CardId)).Name.Equals(testName);

            cardDetail.Name = temp; //reset
            await TrelloRepository.UpdateCardAsync(cardDetail);

        }

        private async Task LoadBoardsAsync()
        {
            lvwBoards.ItemsSource = await TrelloRepository.GetTrelloBoardsAsync();
        }

        private void lvwBoards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NavigateToTrelloLists();
        }

        private void NavigateToTrelloLists()
        {
            TrelloBoard selectedBoard = lvwBoards.SelectedItem as TrelloBoard;
            if (selectedBoard != null)
            {
                //navigate to trello lists page, pass the selected board
                Navigation.PushAsync(new TrelloListsPage(selectedBoard));

                //clear selection 
                lvwBoards.SelectedItem = null;
            }
        }
    }
}
