using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.Shared;
using Howest.MagicCards.Shared.DTO.Deck;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Collections;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Howest.MagicCards.Web.Pages
{
    public partial class DeckBuilder
    {
        private IEnumerable<CardDetailReadDTO>? Cards = null;
        private IEnumerable<SetDTO>? Sets = null;
        public Dictionary<CardDetailReadDTO, int> SelectCards = new Dictionary<CardDetailReadDTO, int>();
        private CardDetailReadDTO currentselectedcard = null;
        private List<ArtistReadDTO> artists = new List<ArtistReadDTO>();
        private readonly JsonSerializerOptions _jsonOptions;
        private HttpClient _httpClient;
        private HttpClient _httpClientDecks;

        #region Services
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ProtectedLocalStorage storage { get; set; }
        #endregion

        #region Filterbindings
        private string CardNameFilter { get; set; }
        private string CardTextFilter { get; set; }
        private string CardSetFilter { get; set; }
        private string CardOrderFilter { get; set; }
        private string CardRarityFilter { get; set; }
        private int CardArtistFilter { get; set; }
        private int CurrentPage { get; set; } = 1;
        private bool navigatingPages { get; set; } = false;
        private int MaxPages { get; set; }
        #endregion

        #region DeckDataBindings
        private string deckoptions = "load";
        private string UserMessage = "";
        private int DeckNumber = 0;
        private string DeckPassword;
        private string SaveDeckPassword = "";
        #endregion

        public DeckBuilder()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("mtgWebAPI");
            _httpClientDecks = HttpClientFactory.CreateClient("mtgMinimalAPI");
            await LoadInitialCards();
            await LoadSets();
            await LoadArtists();
        }

        private async Task LoadInitialCards()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"cards?" +
                                                                    $"Sort=asc");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                PagedResponse<IEnumerable<CardDetailReadDTO>> result = JsonSerializer.Deserialize<PagedResponse<IEnumerable<CardDetailReadDTO>>>(apiResponse, _jsonOptions);
                Console.WriteLine(result.TotalPages);
                MaxPages = result.TotalPages;
                Cards = result.Data;
            }
            else
            {
                Cards = new List<CardDetailReadDTO>().AsEnumerable();
            }
        }
        private async Task LoadSets()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"sets");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<SetDTO> result = JsonSerializer.Deserialize<IEnumerable<SetDTO>>(apiResponse, _jsonOptions);
                Sets = result;
            }
            else
            {
                Sets = new List<SetDTO>().AsEnumerable();
            }
        }

        private async Task LoadArtists()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"artists");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<ArtistReadDTO> result = JsonSerializer.Deserialize<List<ArtistReadDTO>>(apiResponse, _jsonOptions);
                result = result.OrderBy(a => a.Name).Where(a => !a.Name.Contains("?")).ToList();
                artists = result;
            }
        }

        private async Task LoadDeckLocal()
        {
            ProtectedBrowserStorageResult<List<int>> storageResult = await storage.GetAsync<List<int>>("currentDeck");
            SelectCards = new Dictionary<CardDetailReadDTO, int>();
            if (storageResult.Success)
            {
                if(storageResult.Value != null)
                {
                    foreach(var cardid in storageResult.Value)
                    {
                        HttpResponseMessage response = await _httpClient.GetAsync($"cards/{cardid}");
                        string apiReponse = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            AddCardToDeck(JsonSerializer.Deserialize<CardDetailReadDTO>(apiReponse, _jsonOptions));
                        }
                    }
                }
            }
        }

        private async void AddCardToDeck(CardDetailReadDTO card)
        {
            int currentAmountOfDiffCards = 0;
            foreach(int val in SelectCards.Values)
            {
                currentAmountOfDiffCards += val;
            }
            if(currentAmountOfDiffCards != 60)
            {
                int amountofcards;
                SelectCards.TryGetValue(card, out amountofcards);
                SelectCards[card] = amountofcards + 1;
            }
            await updateLocalStorage();
        }

        private async void removeCardFromDeck(CardDetailReadDTO card)
        {
            int amountofcards;
            SelectCards.TryGetValue(card, out amountofcards);
            if(amountofcards != 1)
            {
                SelectCards[card] = amountofcards - 1;
            }
            else { SelectCards.Remove(card); }
            await updateLocalStorage();
        }

        private async Task updateLocalStorage()
        {
            List<long> t1 = new List<long>();
            foreach(CardDetailReadDTO card in SelectCards.Keys)
            {
                for(int i = 0; i < SelectCards[card]; i++)
                {
                    t1.Add((long)card.Id);
                }
            }
            await storage.SetAsync("currentDeck", t1);
        }

        private async Task ShowPreviousPage()
        {
            if(CurrentPage != 1)
            {
                CurrentPage -= 1;
            }
            navigatingPages = true;
            await LoadCards();
        }

        private async Task ShowNextPage()
        {
            if (CurrentPage != MaxPages)
            {
                CurrentPage += 1;
            }
            navigatingPages = true;
            await LoadCards();
        }

        private async Task LoadCards()
        {
            Cards = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"cards?" +
                                                                    $"Set={CardSetFilter}&" +
                                                                    $"Name={CardNameFilter}&" +
                                                                    $"Text={CardTextFilter}&" +
                                                                    $"Sort={CardOrderFilter}&" +
                                                                    $"Rarity={CardRarityFilter}&" +
                                                                    $"Artist={CardArtistFilter}&" +
                                                                    $"PageNumber={CurrentPage}");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                if (!navigatingPages)
                {
                    CurrentPage = 1;
                }
                PagedResponse<IEnumerable<CardDetailReadDTO>> result = JsonSerializer.Deserialize<PagedResponse<IEnumerable<CardDetailReadDTO>>>(apiResponse, _jsonOptions);
                navigatingPages = false;
                Cards = result.Data;
            }
        }

        private void openPopup(CardDetailReadDTO card)
        {
            currentselectedcard = card;
        }
        private void closePopup()
        {
            currentselectedcard = null;
        }

        private async Task LoadDeckFromApi()
        {
            UserMessage = "";
            HttpResponseMessage response = await _httpClient.GetAsync($"decks/{DeckNumber}?Password={DeckPassword}");
            string apiRespone = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                DeckDTO result = JsonSerializer.Deserialize<DeckDTO>(apiRespone, _jsonOptions);
                foreach(CardDetailReadDTO card in result.Cards)
                {
                    AddCardToDeck(card);
                }
            }
            else
            {
                SelectCards = new Dictionary<CardDetailReadDTO, int>();
                UserMessage = "Could not retrieve deck.";
            }
            DeckNumber = 0;
            DeckPassword = "";
        }

        private async Task SaveDeckToApi()
        {
            int totalCards = 0;
            foreach(int count in SelectCards.Values)
            {
                totalCards += count;
            }
            if(totalCards == 60)
            {
                List<int> CardIds = new List<int>();
                foreach(CardDetailReadDTO card in SelectCards.Keys)
                {
                    for(int i = 0; i < SelectCards[card]; i++)
                    {
                        CardIds.Add((int)card.Id);
                    }
                }
                String jsonBody = JsonSerializer.Serialize(new InputDeck() { Cards = CardIds, Password = SaveDeckPassword });
                StringContent body = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClientDecks.PostAsync($"decks", body);
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Deck newDeck = JsonSerializer.Deserialize<Deck>(apiResponse, _jsonOptions);
                    if(newDeck.Password != "")
                    {
                        UserMessage = $"Deck saved! Your id is {newDeck.Id}, and your password is {newDeck.Password}. Please remember these to access your deck!";
                    }
                    else
                    {
                        UserMessage = $"Deck saved! Your id is {newDeck.Id}. Please remember this number to access your deck!";
                    }
                }
            }
            else
            {
                UserMessage = $"You have {totalCards} card(s) selected, you need 60 for a deck!";
            }
        }

        private async Task DeleteDeckFromApi()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, _httpClientDecks.BaseAddress + $"decks/{DeckNumber}");
            if (DeckPassword != "" && DeckPassword != null)
            {
                request.Headers.Add("Authorization", DeckPassword);
            }
            HttpResponseMessage response = await _httpClientDecks.SendAsync(request);
            string apiResponse = await response.Content?.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                UserMessage = "Deck has been removed!";
            }
            else
            {
                UserMessage = "Deck could not be removed";
            }
        }

        private void ResetUserMessage()
        {
            UserMessage = "";
        }
        
        private void ClearDeck()
        {
            SelectCards = new Dictionary<CardDetailReadDTO, int>();
            updateLocalStorage();
        }
    }
}