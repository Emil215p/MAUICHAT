using Newtonsoft.Json;

namespace MAUICHAT
{
    public class Messages
    {
        [JsonProperty("id")]
       public int Id { get; set; }
        [JsonProperty("username")]
       public string? Username { get; set; }
        [JsonProperty("message")]
       public string? Message { get; set; }
        [JsonProperty("image")]
       public string? Image { get; set; }
    }
    public class ApiHandler 
    {
        public async List<Messages> OnGetMessage()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://emko01.skp-dp.sde.dk/CSharpAPI_Test/index.php");
            string content = await response.content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Messages>>(content);
        }
    }
}