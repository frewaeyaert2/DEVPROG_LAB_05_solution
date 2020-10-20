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
    public partial class SingleCardPage : ContentPage
    {
        public TrelloBoard BoardContent { get; set; }
        public TrelloList ListContent { get; set; }

        public SingleCardPage(TrelloBoard board, TrelloList list)
        {
            InitializeComponent();

            this.BoardContent = board;
            this.ListContent = list;
            lblList.Text = ListContent.Name;
            lblBoard.Text = BoardContent.Name;

            FillMemberData();

        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            await TrelloRepository.AddCardAsync(ListContent.ListId, new TrelloCard() { IsClosed = false, Name = editName.Text });
            Navigation.PopAsync();
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void FillMemberData()
        {
            TrelloMember member = await TrelloRepository.GetMemberDataAsync();
            lblUsername.Text = member.Username;
            lblFullName.Text = member.FullName;
            imgAvatar.Source = string.Format("http://trello-avatars.s3.amazonaws.com/{0}/50.png", member.AvatarHash);
        }
    }
}