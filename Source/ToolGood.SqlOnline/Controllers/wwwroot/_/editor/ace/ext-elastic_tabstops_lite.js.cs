#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-elastic_tabstops_lite.js")]
        public IActionResult __editor_ace_ext_elastic_tabstops_lite_js()
        {
            if (SetResponseHeaders("F4F59E2EB4AFD9F455B8DB428E564EF2") == false) { return StatusCode(304); }
            const string s = "G7sOAIzDuOF7IJbWTqYhorSaqa9vLBQGlVP6OtD62nn8nCLwNLLm4EoFRbY3fJ9mK4rtFI3eCuLt6SEKVALOudlz7v+1pq/oiholCVkl62Ty9m22dHcl2GSmlxQItpsSkyVH8OVXll8j19y+KOMBtbaXY3qBe07Cg5te4AF/K+CvwPmsz/5y/0A7L+8UHaPj1+pwY9S8QwSJjhyQSXpGidrchPb3QEH1z9k3XLXvwlmdpT2MA2RaEbWNvcgsMZVZgiTirq1mvILTRZ8T6/48KPaz8wVxkFmydHah7+cyVE6vVVpZMj0AoKKIZG3jpQd9mUVi6TK9dy+ne5+0i0uvqiREVqB03P8Fh3dpx+AZyEleVYjVHogReBDPRCaPoyAGM5NNJwHBacyA0QVEYNU1AUwp5H7wqCta28a/ew2WMnTSgfBWxbQdorAk1eQKSgRP3iPPT7O3d1Uchxk8mQaV2V7p8DaQrf/Ur9MD1J4PlTyU2tUt81e9J7oWJzDHOAxeZzvbJi/SBk9EUJwEIvv08r+l/4NAZEFt88r1vGOZbFGOFjsjcori9dMFg6xbgv0/fg3MYI+cF5CQrYLptqcUZL6FR+m2/+zQiizrYfeKo9CiLWY/gqLW6t7IHfvrV/pPnavqrSsdIvrN1LbZkoAC46xyct2ek9oGQfL7GKzKWcvloyIdzEfR96+j1CjlwYy7OHs+lTypFoLXYFA3CQn3m2e3Ck7dcWYodZLJOqOoUgLflFikzEEtHL+La89GNNZ0VNhoEQqYFtbJnCtxdVB6sadaJnwsCnI9ICRyqt+NssEnWobOfD72tptnD5Ad9QOn1i4O1zjnCYncrjUT+7pXUhZZ+1aM7kxkHVgmz33WpF/2WPnilIgYZ4XgO3/rsr6T/dKXJPY4SdzLg/17tGy7cn24Q2EWKoR7qd6YETIHxfyXb8+VQRhdigVNyRWOtk62Feh31eRhk46yZP+vUkA6qsSmcgFLrIjSX/Bt+uZxGRGWqLJgxmE1Ovg2XHpoYSSB0vwo+FtyDlW8RM3x9SnNtM6m2S37oDlsRszXsPo64kuEqhq5SnFem9oek9A1SKGlwJD5NThgpAS9jhlmGqUw5Elh++C56vc2rR5DuUE2EklnosCg1K3ooKcxI5RbNtO9gYnJ/azlcNLba9jAsY6/MYNIZ6n8WJaj1yxSfLTR1NHKLlsmoLFqvEUCVpoZMCI278q7FNLFkAdYvoug3hWCUlWeeFo3PHjO5oVfDhaNMOGQe/yR1lJsGW6JkLQDI8gGtEiHuoybIMBiyuIjvG9yuMvirLSJsHOS7W7pAYOlIs3B99p24seCtOqznO6HggFXGrjFon8KDWlR/cLyv7qVU3MnPRczrPu5/HiB4RYFtSSyrGHp77bshNrOyy+4CaxVeSN6CFldRiGlZhnqc0JXWmCoUny8stXAcCrKtqaTsdWUBJ4U9QrQVxNw44JLUEXUancO8yqeAxUH60gsH1j9wuCZlJy7NP+jHsMUhst1WJsCoEdlWFT7NsbX3cjX3zgz7TKgD6x1E4o9yfiRsTg58HS2iRDgbUOq1DUwOn8PmCJv7W8r8uqfxTNfWDGxJeXa7feV9UiBkkDsMYbW2lW1CmgbehhI7slbl3o3nu0b4jlheG6vdBjTSeVem3pB4LFfXqIqpfLqfsXRqldpy/vgl9Qd3FTnH8uRhdgqkE77jalZQoTHtpJza6x5zul1hKEmxAig1oLvbTsFKLzIs+sUviRpO4FHMOblKlGHgzYjbQpOBMbjiIk+0Dsdcc5Tcs1kq1v6SNqsFPYIz/seAtoeLK2oLhOuPQ==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif