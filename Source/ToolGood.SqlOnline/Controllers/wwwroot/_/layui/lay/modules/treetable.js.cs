#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/treetable.js")]
        public IActionResult __layui_lay_modules_treetable_js()
        {
            if (SetResponseHeaders("D8A335829E74E4474A7E3B978736C12C") == false) { return StatusCode(304); }
            const string s = "G1oRAJyFsbMjFBdfSakzVgzBCElmW4SN/Fzq4qauM3Oa8Tc+lUvpLEvW0jgcLPHmZOy001uamq+vzQBrLTjalM71WCmv1n6CUq27uDRavhr+ufZ55/KvhNKicjUm7wWm2eQgn7MpICe3e0AkC4pR1VVWu3pZz+Gz5nCReMjmO5aA1K6+r/H39WlhuKTVE0BoA05QAgXmOxA6AFCqmGglYnbMVYaEYI4qLB5LOgblDPgOqaiGKA4IR7MzsvZskNlm+M43NjOKmliZdUSIOaZacAjLLIFg88Lln07DzXtE3Q0DIrsYyQ1v889q19NCrBjE14t4a8Xo12yvV1Fl5I18ycoDysyRYoQiK3JU/uLjdFoCRC35E/5O1ZHz3jPN3t4et9EwZzgE3UzP48r7i+m/yqwuZsf+a/84ZZT+yfZvvt4PlSn4KcOvBsVl3CHQ1AdOffn9d6hZ6nwS4+vDsLwOqItK7J7uKzCESrpBmzy7xdk1ABCF6RXElwJU0fFjQrB6zHkBInewpcX+X5/lIiJGHMkdhOrsAYsq1oRfn8ix5cnYEyLKlwCV46rq3LxGqEUlc4WWogXRIXjAqBW91QF/01HxjcanF3yQbdxtexQi/sh0Sicyd92HxOBpWmD9M08xbm/prOosbergutE80Dk23W/fjMxCeY3tjucVuGxBJDN+d7YDZgV1Ll0Wdf2pSokz2NuN7UoFs3Sb6hrHZ1pibMf3ZlqKfLifXtakWjddPSggBqepEqNAarDFUNZ9kUW8sCX4NWR2T6S1Ut/vbC2C9PHp2q6hC4pmJGDuFPU25LkHqlq8VvC1fZvStKh3m4CVjHNMqXnHKnL+fEkSDhRWGbSvVMGEC54r3GOh+9vQUSJ93erwn74zff2wbgM9ehD3nClIJFidkUhwEkjC7S5Uqn31wwCu/8hH21WGphBCQGbwPK2eY9zmyI11THPfD0Nq6z63CR+tAdJUZhOHgs5asUEzbdAOqNrHDe5+rx7y4vf2fN3X82IWRLPRHK0MzzCxSaoELLHYPwKJhKcL/S46sQnzRQ4JUo8PBVksHQymCnXYeni/YcdjipMwvV+uM1BqsC/0DzmboMQcF22XxFfjqUSg5hWhqH/T4GmX+rAbH4UAZezOpWfL636jX/lJ6HQE0N8cFEsPFlYandAoGVADjuSNIQNvEvAMYLvb5s2owg9DLh68Dcg+HWxB81BMsAlHo4FDABljKBi7e8ZgvWc0btyTdsHue1SSHnUHVr98ptd3ugOAoHGTBXmjNi4ucN6XvmlDZvTvDQOv5fRXC1FzXH9SJTsFDVeRbtidS///fxru4UzIObkgl+SKXJOb/bf3OkVg2KzvijV8Pp/OoUOeKyOdcs4mfQ0FZzqHrPwkzGTx2z8JD11GAOMOJF/7gLmWLj3Bc/WWAHsFpVALrYJGhJMScXChg0sc2LkPqyC9pf///cFRDcl+gy6GAQbq1bsADVz6w8N0bDEKvj3HlUV9yZK5hcIHLczOG3c40PyvRNRwt56/rJVrzvuHyIeQHigurLOctUDVZnmrdxkDEOKG06gi/bU57nIlDC5TsmcFoc2rmZFa6az6NRY5OIYdnRZzb0k0l4J6iWr+GgRpQRsDxoaIAFqrhsVdarNrg0JArirTcTQzGqdsW8z/AJG/YBoFKhMiSMMaGvG2lFhlRckN//lQLAzPUqz0q4s5A0MO+l3GSJDFRrS8WOKeprah3BmdBmuyX86FNDTnyDQUWlotLBM29/G5l3+uLoxdxFNT8TfYEcER4tCtzftTbkJsMUy6oPZKegks0U8ZUSQzkoj9s0sSeJLpk6rJU6gTq4EmXuNCVWTbpkW6oUEW+8yMrbqN/Z1hIvcfHTM0lZcnYvOcngFu0OSM4SgiYdkFyxT5C/ehoCU882WtmPilZ/WeTcrYROJjHlpv5eLCPeM+Vaay5SXxrnTJOOr//yS984mU/yxwt0aLcC9ESGS0ttVEZbF7thRNzdVBpW1z9oBT0y6E/QJG2VKa6dlsV4c3nfrAyM1g+tJUhT0FXNAU+dCgRZ0rsCoqYKIU";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif