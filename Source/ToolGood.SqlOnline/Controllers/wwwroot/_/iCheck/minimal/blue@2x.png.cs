#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/iCheck/minimal/blue@2x.png")]
        public IActionResult __iCheck_minimal_blue_2x_png()
        {
            if (SetResponseHeaders("0035EC50CF54CE054DB8C956716D268D") == false) { return StatusCode(304); }
            const string s = "G4EFAGYAbtqapEb3NTeiZja5hlPt7NAMhW/dkR6IL3Y86O13+i192JYFQZYlgJXecK+k5JkyS6GsrSC0VFxUAACA1tQEbAsAED1MkqAgKxix+QV7WlJTU7CBPcLzcNEr8Pkmf/epDrZiakEZlSkg2u+r1MvntLUlAyINmYKP49/p7hgMPRefMq369BpW6d/61Z5E4+07wltBJa1AVbJ+vnb8sXGewHyrLWa3C5HEn0KvyZlYmugxXb59nst+/cg9oinGfSGynYld3nOArUMFJWr+nGVgPu5bi1hdE/fgGyo5jzqWAPRhhK+vyFzT7fk2ZLl7Zngmsa+UKOQfS6F1MFH1CfnQB/JuEAVmddutZebYJCX1DktAlg2n6X08LL9Ln+IIQlsycT36/qo4KRRCy/SERNv/x4me72hh6k4HWO7yOzTVdGbmw6qvQi9izw6NXfgNkZaShbFAooCcNGt5pANGGrbOhcXOYUsQ4vvt8DbSzwSCF1g/kLrJwgCD6shRU2CeBf+cddYggv7ZjrHWUtfiw9Q9O7TcQ8B7vcg+N/30OjNq+k80Hs6o+pliqALZC85OopRsY2W4tCNMOM/nyA6N9W8NA8cm4nOecejTXsFCqYh8ZrK5aGBf3tmwlUFT98ZJFc99i+9G/xecptuv0AQ/ao21JKtUz6bS+qlepXv5HcqORNaz/Wrz+9UeZ3Ht3t1fExpQ2TGB0b2s9RL4qEZ/GKEzVlOYRLNqMAha+lc+5sxvhMZOYKjBHYsyKf5EljeTRvpklmwjBbOlN17g2vN7M6P/VeGSf2DyKYOen21QSbEDE7pNnm/7ppHvrzDov0v64IVGv2H/ovbpIp8HKqH0ixjdy13gqXjIRaVD+PFfnp8KBdknOstzsyoIxQdYqlLixhq/pFLiXDi2Qs9DQCjc0wDoUUO7HMPtMfVPEIpl+0gv+80Y9d2YGvtvJB19haHuaP7VO6hGf/h36r04Q6D6vOtZyV8GyMM23yeG7HxX8xjCLpAxqnzgPYYxOK18ezxxpJpWEj+tXfgbfo0hiK4mXKhYexd4UYuuRFQ0dx8mkn7YKtNh3zRYZsvPkA28nMYQdmnBHZbfdSKEBmZmJXqzm6TyM1jTcBXNyCX9CzHlpkK/+vLwQXDj3EjkG7ert4UgnwAfZXqslQbJ29N5DGFzifTj30ImsYHA9PS5GIW2onfFPxNHplcZRlaroUyfVGht65fOC+GBmdkbHai5SKxuM03weaBUffCv+OPssf9JB7V08K6hrl7tnfr84h5YmUax5sL5gatjaFwm4HQpwzwTLu2l2rv1eQ6BYL3wN2nKa6Tu58sYwi2Byjz6LkTwMAKg94nN7R+/7rblU9d2fb7L4g+d4LFF4681gkupvZLxKReCDSG0OP9or/Q6M1xhAxR9mvcMhJ2Tur6aRp07F707UdEqnRk6onlmBWOUWiYZ9Uzx0/wMlni3FjqLHK/uGpuZ8wJUe7n1gcDb7YQgybfJI1fxUw8NSEUdvcROYHQ9BISOqj7RQ1fJZVpih4E81UrYEYY6aOpe4R1w9A2yo7lZC8HLfkJjJZS7+5ol6R8P+8EvrdIr3zV/fZRqKaQ0DfJss9HO91Re9SnKjHJEJpwQFZxQ74F4Ylx7kBoDY4IRgvFQoi1fPV95S52ERbi4/IbiXmA3F03nEkb3UvQDY9S1WtWqVsVHIO6U8v4KxBeEbJZZsXkqnFuJoBVliySi3bn/AZq6F9OoX4YMn8A09cT5cO6NZgSHql4rNc7kG71EfoGm7j0+z8+8A9pEsIZYo8RB08DwNamhT+TLPiAnHJNFmJDxo+BRQsoPFMlTZpoNrdIXFhI6/W/qxsg4SirUwtfpAEAeH0FhrbMHAGN6zwr8xxCWBQ==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif