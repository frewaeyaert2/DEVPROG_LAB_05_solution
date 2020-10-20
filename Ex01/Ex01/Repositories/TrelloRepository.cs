using Ex01.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ex01.Repositories
{
    public static class TrelloRepository
    {
        private const string _APIKEY = "71068bd52d0077beac582e94df10b9b3";
        private const string _USERTOKEN = "ba936007f113ae561ab9d41fa322cde52ba2c006a69cba615ec5a6bd2a5361c7";
        private const string _BASEURI = "https://api.trello.com/1";

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public static async Task<List<TrelloBoard>> GetTrelloBoardsAsync() {
            string url = $"https://api.trello.com/1/members/me/boards?key={_APIKEY}&token={_USERTOKEN}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    List<TrelloBoard> boards = JsonConvert.DeserializeObject<List<TrelloBoard>>(json);
                    return boards;
                }
                catch (Exception ex)
                {
                    //ALWAYS add a breakpoint here
                    throw ex;
                }
                
            }
        }

        public static async Task<List<TrelloList>> GetTrelloListsAsync(string boardId)
        {
            string url = $"{_BASEURI}/boards/{boardId}/lists?key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);

                    List<TrelloList> lists = JsonConvert.DeserializeObject<List<TrelloList>>(json);
                    return lists;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static async Task<List<TrelloCard>> GetTrelloCardsAsync(string listId)
        {
            string url = $"{_BASEURI}/lists/{listId}/cards?key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);

                    List<TrelloCard> cards = JsonConvert.DeserializeObject<List<TrelloCard>>(json);
                    return cards;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public static async Task<TrelloCard> GetCardByIdAsync(string cardId)
        {
            string url = $"{_BASEURI}/cards/{cardId}?key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);

                    if (json == null) return null;  //ID NOT FOUND

                    TrelloCard card = JsonConvert.DeserializeObject<TrelloCard>(json);
                    return card;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public static async Task<TrelloMember> GetMemberDataAsync()
        {
            string url = $"https://api.trello.com/1/members/me?key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);

                    if (json == null) return null;  //ID NOT FOUND

                    TrelloMember item = JsonConvert.DeserializeObject<TrelloMember>(json);
                    return item;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public static async Task UpdateCardAsync(TrelloCard card)
        {
            string url = $"{_BASEURI}/cards/{card.CardId}?key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(card);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMsg = $"Unsuccessful PUT to url: {url}, object: {json}";
                        throw new Exception(errorMsg);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public static async Task DeleteCardAsync(string cardId)
        {
            string url = $"{_BASEURI}/cards/{cardId}?key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    var response = await client.DeleteAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMsg = $"Unsuccessful DELETE from url: {url}, card id: {cardId}";
                        throw new Exception(errorMsg);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public static async Task AddCardAsync(string listId, TrelloCard card)
        {
            string url = $"{_BASEURI}/cards?idList={listId}&keepFromSource=all&key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(card);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMsg = $"Unsuccessful POST to url: {url}, object: {json}";
                        throw new Exception(errorMsg);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

    }
}
