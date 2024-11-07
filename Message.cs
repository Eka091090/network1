using System.Text.Json;
using Microsoft.VisualBasic;

namespace lesson1;

internal class Message
{
    public string? Name {get; set;}

    public string? Text {get; set;}

    public DateTime Time {get; set;}

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Message? FromJson(string message)
    {
        return JsonSerializer.Deserialize<Message>(message);
    }

    public Message(string nickname, string text) 
    {
        this.Name = nickname;
        this.Text = text;
        this.Time = DateTime.Now;
    }

    public Message() 
    {

    }

    public override string ToString()
    {
        return $"Received message from {Name} ({Time.ToShortDateString()}): \n {Text}";
    }
}