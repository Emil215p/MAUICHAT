﻿using Newtonsoft.Json;
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
        public ObservableCollection<Message> Messages { get; set; } = [];
        
        public Dictionary<int, Message> MessagesReflection { get; set; } = [];

        public ObservableCollection<Message> PendingMessages { get; set; } = [];


        // should move some message logic here

        public MessageViewModel()
        {
            GetMessages();

            System.Timers.Timer timer = new();
            timer.Elapsed += OnTimedMessage;
            timer.Interval = 10000;
            timer.Enabled = true;

            Messages.CollectionChanged += Messages_CollectionChanged;

        }

        private void OnTimedMessage(object? sender, ElapsedEventArgs e)
        {
            GetMessages();
        }

        private void Messages_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                // this is a hack
                PendingMessages.Clear();
            }
        }

        public async void GetMessages()
        {
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync("http://emko01.skp-dp.sde.dk/CSharpAPI_Test/index.php"); // get the data, declare API url
            string content = await response.Content.ReadAsStringAsync(); // Get the content of the response
            var values = JsonConvert.DeserializeObject<Message[]>(content) ?? throw new Exception("No Messages");

            
            foreach (var item in values)
            {
                if (MessagesReflection.TryAdd(item.Id, item))
                {
                    Messages.Add(item);
                }
            }
        }

        public async Task SetMessage(string user, string editorcontent, string? imageload)
        {
            var client = new HttpClient();
            var data = new { username = user, message = editorcontent, image = imageload ?? string.Empty }; // Data to send
            var json = JsonConvert.SerializeObject(data); // Make the data Jason
            var content = new StringContent(json, Encoding.UTF8, "application/json"); // More Jason stuff
            await client.PostAsync("http://emko01.skp-dp.sde.dk/CSharpAPI_Test/index.php", content); // Post the data, declare API url

            PendingMessages.Add(
                new Message
                {
                    Content = editorcontent,
                    Image = imageload,
                    Username = user

                });

            // add to temp list, which show the use that their message has been sent
            // then when message is received in GetMessage remove it from this list
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
