public class Strings {

    public enum Language{
        English,
        Italian
    }

    public Language SelectedLanguage { get; set; }

    public Strings(Language _selectedLanguage)
    {
        SelectedLanguage = _selectedLanguage;
    }

    /*Language Independent string getters*/
    //TODO: add all static strings for the sessions in Italian or english
    public string Session1Text1Example
    {
        get
        {
            return SelectString("session example", "esempio di sessione");
        }
    }

    private string SelectString(string _english, string _italian)
    {
        if (SelectedLanguage == Language.English)
            return _english;
        else if (SelectedLanguage == Language.Italian)
            return _italian;
        else
            return "Unknown language...";
    }
}
