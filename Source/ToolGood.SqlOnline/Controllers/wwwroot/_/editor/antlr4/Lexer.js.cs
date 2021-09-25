#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/Lexer.js")]
        public IActionResult __editor_antlr4_Lexer_js()
        {
            if (SetResponseHeaders("814B000BB280F38F5E1DB4407B302217") == false) { return StatusCode(304); }
            const string s = "G4USAGTf5uvpCu6yHsKBticUdext/ypD5AmRbb93v8+VgG2NqbF1a0xdlfkzN8kHTgHAEWfygM/ZbIHVqn5VII/a18ZgtspPxARO1i4Mw0mNK4ROJeXG7WwB9aj3M7kDvFKUlZ1gSy8jsApjwfQfMKDh6XpeU/66PIXr9/SuerYD1jUR109kMaqRm/O1nt7M/ciw7TPRHIa7P3LWj/0Xv//yAbc01UoH2esw+Pm6CSsetv9znh6/GBRy9+dojtWchP7Tt/777y5//U4+xJoaDQtZb17Jx16zblWtA85Kcy6GtPU6VlhIfwcoweb6bh3jcQgBGiPuwyn/ruFbESICAlwjsl70t+7Sy6TPyGTeW466JdL9z/JxthfzB1G2U7f3POYp0qSKfdZZfCA1chJaAFYzqokZs7FSQ054INIIlo9wf/U/Gnf0XP4aIw8DWksvQp4tbrzAxgfY/3FD7fPHFPvm56iYkV1w+xO5Pf5Yvz7KIjOoCuNzDcS2LO7Nly/T6aUWeeMApOr1SNeakJbWJiOpR8ZQh8ynjLS1jIymHul5BRlFgWwcDfZ6hNqKBFSsn2UMiD/PaFhISvyyzPN54NuRmejD5mFUz+qsPR+f3AGYfpACZRYFwWInLv5lJ2dltkgEZ7mhugOAP6bvyvtBBHDpUmyvLJNjSrTEZBnR98YRoGxYAHsMMJTMBPt+KOqaVN+R4EwD9xe2NrSXTFY5NG7ZXRAcgLPbk9YdysX01+bSrMB6k84u5eQe/o5kW7iRhDoh1KJ1qbMdnA0V9Pq9eRMlLeplgbRgRiNUq5b8n/TsdhyBmT3df/ng8uWowsEAoF7Ga0TRHGXlC21xI5wZtwN11Mhfs823gKgRMIStqZLSFRcboF6ezYFau3UBKOpTBPKGDNWIyP3yoxDKjZUnyr7sz5ZRA+9047+hLgEBHnZy/Eqfz6XOsIMaXL4s1GGIObMJnQKMwCVzAhQTLd4UFYqwxXyw0GYtE4UzdIGZByDhy3WYYRg84YQFR376WiRbhi4v9vh/EgfsRwCc+VhpOoxMHHSgUmcJb1K/lINubS0GrcDHROOjeLbJPlVc1oO5X6u4sBW1pwLQJ/XY3HjAdo3pRUox1c171wrVhs/Kp66SLnIpSlirk8E2J/ocNIqE8vIayds/hcqur/ZsYn85eCKQWHYZcf1dbDZtMvVZIrjAcTZFUHDTEttEBaw0A6bve0l0n9MzCjqW1FuEIbqH/VBUPxndG0rqV+9HNJSKJVYt+iuBUhGX34gkcqahxe+xJ5qd7K2js5VsKeUOsaVwBH+SIxhxdHOTOlqTd0aQKYhpL/KxszUpjzt3k1qYyk3lo9e70S1IDagqUoiu4bHbe3tKVlim+lT6ReGv3+3BuKfdmI7yEFjvjxNIz/ZUhQiqbkUxS6+7JOELJyRjokeVOJdFo5vmuREFcOWQknsJBZ91ePtvn5oroYMjPhGo3m3a0wZdCH8dC69VqZmTPcJfAJW0nKj/PuZhiQAJ0oKzN+GKDoC3SLKRpIzudB7It/VuN05518nkla/+XQb0Ius8Rwh5HX0wW2PLuUrliKa/oXS7qku3PQ5e6ZGbYfuv0zfCpMTdHL5pOPntm66WfNWXbMEqq63FqzWKUR7kJQtuo4pNpq79b5mi38Pe0BbUtw/LTavT956xZ34iGKYLAJXOEcQZClNW";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif