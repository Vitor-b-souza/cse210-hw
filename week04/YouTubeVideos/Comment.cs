public class Comment
{
    private string commenter;
    private string text;

    public Comment(string commenter, string text)
    {
        this.commenter = commenter;
        this.text = text;
    }

    public string GetCommenter()
    {
        return commenter;
    }

    public string GetText()
    {
        return text;
    }
}
