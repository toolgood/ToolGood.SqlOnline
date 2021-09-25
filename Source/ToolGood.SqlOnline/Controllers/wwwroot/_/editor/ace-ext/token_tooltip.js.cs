#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace-ext/token_tooltip.js")]
        public IActionResult __editor_ace_ext_token_tooltip_js()
        {
            if (SetResponseHeaders("4E07C33339BE56160866AE7B5D4FB262") == false) { return StatusCode(304); }
            const string s = "Gx8MIGQ11V7fRCgMsqYZ+25bYMfjdXNmKKk66ECmM+WjtabRKjQSpROpkZG9N9kXRO8R8ZvD9h4RS98gZAnJWidUXiN1Zc22PKIMgfTMzs2XgRmJg9sGZ5KN/ruaRi/9+kMV/waUDM9u6MOktxSX5vzU2skAmlQuH/o8G8wZErcZtdVByhUfg4QcFBy0VpKRFgVQU01ZwCvoAbAQYjkfYCuMnsonsI3hP/duMNmI3XDk+Wp1O1TDIYGyxFtDYsHZIZHrEKCx/K4hGyXBMufZs91grmXNiKP1tXai+vjLY1zMVt2TQwrQmq7bNoio8cZl5RrtpsUhjx9LU1oq3aaQ5d/hA99R3es9DJSQvBE2EJVly3n0AKT8lmjYT9ZeTW7G3WqUMAIXNpZI4lUuexM1K8kwKhj84OcKjIt88ZPNn7Y513jVhOdFAcYpLI2OSUiGFCZZtj7ND/6GMyznA4tFqPWNI+3kDIHk3nSQ6tI51xs1zdu8CTYWsDXq5ta+3vsYlgaI1AlUkD6UBWBwXXcV+PG3NuOLQNqHM+MuiBVfpqV0byJUdWz63G1nzdZty0IvJm5akfoDOrqPoUO6k3Pq9u2Sen1z29PT/vl7V/VX9XXTVs1IUf7saLppkFhddQiqnLl6VfCgjtye7pjuIJJKFAOOvbRvDQaMigGjSovmjAUjDmk1GXMGlbN7tVvnyAPB7/iUhOzIiw1FHU8ixlcq75MaR6Sp4jkmr3S/MzqBFvHPJPo5hyRy7t8Y4xpmv+bV6bb6WVdv/5SzWKaVY+gSkWmTs/Jr+8PO23RQzlHqUeAQM3VTruruKU6Oy8nKvYo5O90IdxfxxTECIfvreNdJaLV4Zdl+UhSQSnHZlTA/Tqx7Cmlj/9bJZJN2WMwxRtTs8ZiIxIDXo6QsjE0ilz4hHGwqiQIemvtgwFZIuMcYhzPQNnaBMEylwG3NydTV1iIfFgGoexpFj0Bdj37zHJtzF1EvR+nP2KCR36vJCBAubYgvKhxGTywoDl9VYtENkL/9z/8Wab58CObIWTiYw9GHLWsb1nnuIMq5hP7uGbNg5T8SsR/CS3JmD3hI1DGGSfyOruwiJg/65ss5A8CUjMHp3wuoqWtsP8UruRnRolBOROj2BtWLtZJSTqpoGngFpxnvRK5pg7rzUItFbipY4Z0UhHMbJ1s+Kpu6NEzvVYB4oShAg4RRlQhWVw6+Aewq0qy85Ncvc7byWQVk29aWfTjo77JYhTS3el6jT5zJidY7EyloeYAqUEzyBRYRgt00lQHsC1ZeIqS+lRnO5ultPt0J+X+oCRIiM9TjyLsxCKYAvfHn5FEPY+hOqKSynI3TnIi2OuTb3TyODMs93fUi9fy+AD0+yDBmzPz+shrawhS5ehN0dlJIEZ5dWOG5D9b2mOA14+6JsFsSDd4paLvjscVgJyDByiQuA5KAJdYTHGnTIHWxSU+n2FIJiREB";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif