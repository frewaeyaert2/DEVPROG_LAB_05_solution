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
    public partial class TrelloListsPage : ContentPage
    {
        public TrelloBoard BoardContent { get; set; }


        public TrelloListsPage(TrelloBoard board)
        {
            InitializeComponent();


            this.BoardContent = board;
            this.Title = BoardContent.Name;
            LoadLists();
        }

        private async Task LoadLists()
        {
            //this.BackgroundColor = Color.FromHex(BoardContent.Preferences.BackgroundColor);

            lvwTrelloLists.ItemsSource = await TrelloRepository.GetTrelloListsAsync(BoardContent.BoardId);
        }

        private void LvwTrelloLists_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TrelloList selectedList = lvwTrelloLists.SelectedItem as TrelloList;
            if (selectedList != null)
            {
                //navigate to trello cards page, pass selected list & board
                Navigation.PushAsync(new TrelloCardsPage(BoardContent, selectedList));

                //reset selected item
                lvwTrelloLists.SelectedItem = null;
            }
        }
    }
}