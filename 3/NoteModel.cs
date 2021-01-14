using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace _3
{
    public class NoteModel : INote
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; }

        public NoteModel() { }
        public NoteModel(int id, string title, string text)
        {
            Id = id;
            Title = title;
            Text = text;
            CreatedOn = DateTime.UtcNow;
        }
    }
}
