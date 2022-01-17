using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public string type;
    public string value;

    public Message(string type, string value)
    {
        this.type = type;
        this.value = value;
    }
}
