#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/tree/Tree.js")]
        public IActionResult __editor_antlr4_tree_Tree_js()
        {
            if (SetResponseHeaders("521C6CB7F310FB547834CEBE8CBE2CA5") == false) { return StatusCode(304); }
            const string s = "G/IKIGSztl5fsWYoRZ6EnIi8vb0NkmXSFD97WvuFYrXYHicZT5l7s/cOUStx5ZtuQzySxBuJ2kk8xTm7GrqWsiZh9xS+/OUcKEmc+YcJaf4rKIHkd4BHFDhgEqqAHN6EGNDEWm8D5UuKE96TISsPTm1OBQAPy4x2kUjfUWeLAuHl9KZmT4UscGlKtnXqtkIrXOIbr1iAcNPlfxA4kG7tNRoaP/B/PyKJs40f1+z4Zhf8gnBk8fu78SiHQd4CTMGEJsnW5B9Jo+ALqGPOBHf0arVSQubhSUF4cIyVh4NUw1ChBbHBGUaPBYm9ItYToYROUR5rNoVvF3CB880Y9LJDCeOviUNelOD7pP1N/C9c0ANY5HJ0X3niymV//HAlaQocYB56Zm3rOX5T8zo2TAQcUvCoYVGDzDHqDL+MGMtLv5RRe5k5W18DyfogUizAxb+YB8o6HZC6Rnz5FHG2heJaLHTy2eQ6lPoo22l8MZ3JpUA5+WSvotGf4eQ84AQ5xSngOj9CR9YohDQfbI8A1ilElB7qcfEC/0mJaoVN5+L2ISkWdqs3GB10toijwNgsUmYuGJaBlKtM2SxgWz8QTcP548XhVv/GBQdbPeaVqSV6JIgWK9DBufjoCs5vtgXmKv1SdrwTC9WpVPJFzzWq7UQcqPFUnTbies2K7WwS1bOa9/agf+7Aqy/IySXQ4ZABYy202IlwGeQ1Ix+uUq2GCm9BHgB6fsMrtVH7bGyfOUBtNKAQaMnptJ/GNabeLIAcd+ZsQ/F/sijStLRpsTUmYw/q5B+cdw6g+DKMahErXQpkXNucbShkjSHwzJxtIAHJ10GNSsNSwTfsSjiwpJnqUcW6ZuKOUx6nMob94uvxZERKAWi7YLaEwzeN8ZlAH4hKeYEA+AdqpbqlGP+Yrr0sXD+8r/fpj/caP08j";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif