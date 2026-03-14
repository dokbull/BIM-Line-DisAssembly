using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EContact
{
    public enum ContactType
    {
        INPUT = 0,
        OUTPUT = 1
    }

    public ContactType type = ContactType.INPUT;
    public int address = 0x0;
    public bool value = false;
    public string text = "";

    public string ioText
    {
        get;
        set;
    }

    public EContact(ContactType _type, int _address = 0x0)
    {
        type = _type;
        address = _address;
        ioText = address.ToString("X2");
    }
}
