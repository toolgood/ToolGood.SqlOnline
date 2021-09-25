#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/mysql.html")]
        public IActionResult __editor_mysql_html()
        {
            if (SetResponseHeaders("6C834B87F7847C86BE3FB214AABB7394") == false) { return StatusCode(304); }
            const string s = "G70EAIzDuPHauDRfsehIz2n9gMWl1gbuAb+BrlDvsKT49P9/mxVCg5ACodx/H+ZTiUua1EoeJe9gUp2tXw7Wk5OvOo2mMLnap4Ezc4jufG31jq006qQhmxXmz2J/3Z7bxEyc83y1GYMjsaCQu3/5Br2BaTbUlH+hKZJXLTcdzBLnlwdeoJ+DKcBYZYT7TlaYflak/lEqVAOYmQlGRRMSH4OpzAHrdoIGACRpqFvU0ASJTyZkQBqaHU5sREAHG6MZxPSlkK8qAeTRu5Pxy9Qjr8lEa34UMDYbqP6LqD51X7WG+VyadsPVXuWCsVH9VhaHeUfvcg3enyJXJTNuOyxllhfAZOPKdYt0FVv1km8M22LTdsG0oUljuBRmXEbsy0kVq1T50g2BEE50u3+FBzS9eb10cWpV2L4WiAMsbArxu/Z/wcbWwelWLX6XeST8pX4rWb9kjh14krr+oli9bsqUcc19obpBi+JK3tzukAE86KyDU5kNdHt+ZKiqGunXKmyV12Iz86AK+nEL6NEU4YVfJ0uZtc0+OwTKRQ1FyTLxkZwBPmlF6ERT8+UYqIuUovziouG0jAA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/html");
        }
    }
}
#endif