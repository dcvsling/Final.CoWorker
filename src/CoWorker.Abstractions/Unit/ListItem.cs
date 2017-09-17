using System;

namespace CoWorker.Abstractions.Values
{

    public struct ListItem
    {
        string Text { get; set; }
        string Value { get; set; }
        bool IsSelected { get; set; }
    }
}
