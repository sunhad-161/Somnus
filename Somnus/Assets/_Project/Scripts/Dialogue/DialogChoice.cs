using System;

[Serializable]
public class DialogChoice
{
    public string PlayerText;
    public ChoiceType Type;
    public Dialog NextDialog;
}
