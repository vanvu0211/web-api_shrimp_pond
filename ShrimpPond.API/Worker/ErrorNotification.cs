namespace ShrimpPond.API.Worker
{
    public class ErrorNotification
    {
        public string name { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;

        public ErrorNotification(string name, string status)
        {
            this.name = name;
            this.status = status;
        }
    }
}
