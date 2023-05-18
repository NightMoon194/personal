using System;

public class TableNameAttribute : Attribute
{
    public string Name { get; private set; }
    public TableNameAttribute(string name)
    {
        Name = name;
    }
}
