namespace UniLink.Client.Site.Pages
{
    public partial class IndexPage
    {
        public string Title { get; set; }

        protected override void OnInitialized()
        {
            Title = "Deu certo carai";
        }
    }
}