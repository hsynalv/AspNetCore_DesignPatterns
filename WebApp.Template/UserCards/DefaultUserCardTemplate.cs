namespace WebApp.Template.UserCards
{
    public class DefaultUserCardTemplate : UserCardTemplate
    {
        protected override string SetFooter()
        {
            return string.Empty;
        }

        protected override string SetPicture()
        {
            return $"<img class='card-img-top' src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQzld4j0UVTqlGqgaR_Kxv4IXjpGt7yCL5fMJA4QwnKPDEglh9r3lpMXRc0SwRnk_5lrMQ&usqp=CAU'>";
        }
    }
}
