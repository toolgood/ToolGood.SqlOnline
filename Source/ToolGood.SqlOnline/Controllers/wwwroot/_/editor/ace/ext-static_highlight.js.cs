#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-static_highlight.js")]
        public IActionResult __editor_ace_ext_static_highlight_js()
        {
            if (SetResponseHeaders("D344C654AFDBA0D1726C9557B19A2E39") == false) { return StatusCode(304); }
            const string s = "G9YOAByFcZvz9ZNS2sqqwE1Mm+qaVqJvLWVmmX6k+oz1NkXyqYQPxS/da1WLVSi7o8cSt6z3Pt/qqlwqjvFgLJ+xneAB5Y/Src3m//b7BQ0SkVilJs69c998FZtBRJIks0ZkxTqt74a8NkZz2QYm4H2tfaGPf2QMNRxzVXxNVXqMW19xhwVk1e8AlelzgLCtDsRQXfptDsgas6upoqUNK2PFx7PDrtqPDzZQNQHXIqufI4Kh8Ac8zbHQNA19cnqoKpCIP3gchdVKesL8+ux6X0D0/DvA0OSqfQ0bEYgk2c3y00p3Z2ND9iZkwcIy8tR7EQ2k6+O2kPYUV2Xa/vKHdf14A5M3uazK73f+mJ4KV9x0fGfzeOlmM6sfTVkn3vI6efnOD+FhX/wGM5X69BCrd41yAfAu1qW2dC/WszrFt726iZxWdKn7/5RSnHqHXurjKmlMTJWWNN0Ww/X/0TqtGgTJpB+FB4cTLiJeXAHHKXGjJTRKa6/q6T69FA9JoOTdZLzNXWKmNSUKqbiWD+N7KT+6UmjH0BZzYfk66z2JD3QkZbbQegHbWhuv9axVxQImyCEsFhLC5Ui5vm789Ve51dd/FmalZPSYXNLO6lxdDo/9FB8oyOfi4jRc6bDREGj8DioRJL8+xtGSy7CfEY5cjm0smhF04DAECa4oaqpC4mV8jGOZCmqM9DRs12QKcYAl35L8i1IeCEZfY8nat3C423qmhSNki5WiSWo5kD4JrgwmC4s5/qB2IgkbgC8rq5HrZIf3PvEH3tjHYREG3DjKL9lipThOVIqSXVJaNk5BPFMANcDN3ZSWkDDqaEQ4xzl8OSQ+cByJ4iv+yXEuVGlR8aCkjgczSmHa+93LaqtBRE5QInJZxa4NEOhLqOKp2khrdTDvkzFse0Gv1ym+FOhd8ytJ4lG2GK22szkrd2GyGQvz1iG9Gx9M/I7wyS7L5vPUrSFCjjmVjWqV4B0y/cclLV8CV4xoJrAguyxH79s2sPm4cuHYdC0UDGQ3X9NzBjyrtQauDqQIMubXuEMztj4fPnQpOoiBQ2KgbDkQScNNBYiyoWG++litBVurvCGJLlANHH68wCHwwVzLG26BQkYc271zPnwmDYYCDojSAbH0cZ04IaNA1o6iM2LEQh6YQOoq70lMjDl6Y/U/LEitICLA0g+jJDMYFUGTpIHQ/l6NNP86K3WvQ2TGdhSw5/+uKuQaQ9IIH0Z0GfIkx/pWM4znh1upYXGNGs2Qq182KcOTquujVjM94YOs7MZeUJfjL39hgm2pkZah9rgxrFug+bsq+euGRcaXVtH7A9b6oXIevB/oLXMuGhmrDUCASPzhEiiOlCiF6sLdRD0BR6C96XMk5pozr17vEIx1oOaRFQMzIgn1AIgVmwM07nTeUw4B+lZU2J9Gz7qA8w2H0kJOPyhG+lUel+WSozzDvA900hj8Ha9aJfmLaVz9zw3vcfKeohnMotQuHRl0S0oIpH/+MIeVvDZdY+3K3DMhGDWmC8wklB2RIrOauYhp5eY1WqLSephwVvB/UmScMSJgwhGVGZ1uwP0LvixrdLcMjV7LmrhDRWO61zSqfT5O1KytMWlRY2T/noTsQKAzRsqA+PBFydRkeUzdjtcrZpbDOH2jh2It6A+9+kWCUw21BcoycoWZiBq9TG9q6FgazPoart9yzgt201USMc6mNcz+bHy3vWE0Geoe3g/PznFEbVqsCC520R4KgWZm2BAQyz9ygfCzczUU6BICoLZzLYCahGsNtv+pAgJ5uWZM1uq0OesUAl8SNPtIwbzFAZeFKl14vnaP6zkrgV8ZsDvPoXak7MVRa9RWZmR55FZQmxk6YgJbrXPEPi7490g/OxiP2mHt6PaFwLjGea5jqA/ssgFNubse8ZMe+q++1py1SEffLnOzUWrkunlmNLjQTUndi4ekxVki0MTPrJtSUCPkSDELR+bf5z0ZeEI1pGMt9AY=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif