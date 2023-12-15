using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MAUICHAT
{
    public class MessageViewModel
    {
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        
        public Dictionary<int, Message> MessagesReflection { get; set; } = new Dictionary<int, Message>();



        // should move some message logic here

        public MessageViewModel()
        {
            GetMessages();

            System.Timers.Timer timer = new();
            timer.Elapsed += new ElapsedEventHandler(OnTimedMessage);
            timer.Interval = 10000;
            timer.Enabled = true;

        }

        public async void GetMessages()
        {
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync("http://emko01.skp-dp.sde.dk/CSharpAPI_Test/index.php"); // get the data, declare API url
            string content = await response.Content.ReadAsStringAsync(); // Get the content of the response
            var values = JsonConvert.DeserializeObject<Message[]>(content);

            if (values == null)
            {
                throw new Exception("No messages exist");
            }


            
            foreach (var item in values)
            {
                if (!MessagesReflection.ContainsKey(item.Id))
                {
                    MessagesReflection.Add(item.Id, item);
                    Messages.Add(item);
                }
            }
        }

        private async void OnTimedMessage(object source, ElapsedEventArgs e)
        {
            GetMessages();
        }
    }

    public class Message
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string? Username { get; set; }
        [JsonProperty("message")]
        public string? Content { get; set; }
        [JsonProperty("image")]
        public string? Image { get; set; }
    }
}
