using ApiDomain.Contracts;

namespace ApiApplication
{
    public class Github : IGithub
    {
        public string ShowMeTheCode()
        {
            return "https://github.com/sfspacov/softplan";
        }
    }
}
