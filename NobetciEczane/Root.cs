public class Result
{
    public string text { get; set; }
    public string name { get; set; }
    public string dist { get; set; }
    public string address { get; set; }
    public string phone { get; set; }
    public string loc { get; set; }
}

public class Root
{
    public bool success { get; set; }
    public List<Result> result { get; set; }
}
