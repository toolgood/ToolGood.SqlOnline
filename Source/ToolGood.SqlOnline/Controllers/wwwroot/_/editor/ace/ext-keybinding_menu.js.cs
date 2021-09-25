#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-keybinding_menu.js")]
        public IActionResult __editor_ace_ext_keybinding_menu_js()
        {
            if (SetResponseHeaders("3C223B21083F2E47DEC6688B75B43841") == false) { return StatusCode(304); }
            const string s = "G5cOAByHsZvlxmZCLV45QpLZ/9e0TlfmZ9eBJ9Z0eGyvR9MsBLDGMtUIlpvKIyBNGoT5sCkzlPpdEWX51Rx+ZcquPL7CyN7b3VzCJYRQGV2rvga0NB6d7ny9mWKUZPqnEl0kPxbDIs2c0Bmn8oaMYUOPe/Jx8Kpzj2X4XqOdY3FDYAQ97gJBpwpcRQNYxk9Kk3hZU8G1xYtwTM80lfSaTMQYXNc7ZuKfYCJgFqfy2HLs2/GpWZHsYYF86Lix3fWf1v/6fBF1a4gnzSOwrGHIWV4e0WIw/YpOJTEOwhVFH8t8d7kkPGq0B7JarNoOYQQhjw5kLmly1Hbhc+NVL5Iwnmsq72LCr7WlqwK+v2MfaaCdT1sCCB9pstAJUqQS7y5sHDT6hOFnbpgsS05fFi6X5gQeL6+IcKWpYlvYRRrtnI3HKScZSy4LfN+cwKMRRMxxjKG4LIXGeV8B2SV7aKxUs5W5i85gsZpQZixlVBO15t5Pi81blrWvcro4M3c9Q0v/pa4BWWnqFNU5Dn7hk+f03/rA/+do3tzdvhjbZBFOfZdPaJQXdOz0sAOcOIY7a3IBQoXihbyQPsX1F7t9Fu3RCQTJkMBPNJEElQxvN+ZTTidab6KTRAVHHDFJgmbbZ3L9b9RtWZlkyDLdPouzf5chU/dyUKx0XNbvokEJ1RLwpVkOoSEgP+LTt7g8Fs7Wn9Cp+rP/72RNg2eXTkPX+77ntVMJMXWR03exFZAfCrCsrqimhcbspTk2pI1L5yxBO8K1ZV0wSSr14XQwDVT0C6cAxzBPgZMPVW++7DAU8ulgH980J6oQFb0YVVYqYmVZ1ZFM5jQoEeyfFZZV1O/IFn5NKQw1loXV0c7IiInvXkY4pM4bqBMAVduBxIYTKbrzk8zcA5biJuAtUh2YFK7RJq21CL/BvHZy2SgSbq00hmrMIQbijX0/4VlJ7wVsXL0GcFNQoDdj/vbAAIhqt0AuHFyoDogkt/rrVZEt2G+dNV1ZZ6CLPsrssJDqOawC/hOayBEkQVz3YIXK3RHmvskutJfDV1U3naSPs37fDUxOdBukD4NKDkfSM3As5BP/CI4x7Gx6PgHumPBaldGkOI2k7qESPxtQcvjYBuf7S40H/18iXuedS/vHXKTu94+PP3/7T7WI9ioxTkbnnL1QbjeE/gseO96756a7pAV1gQVmqBbGJcbh97wB53aLSmRqVKd+UImSCX4OH64O+R481d+gGdMONO7Q7JIcO/Qm3S4sRB2V+7q4GhtMPz2KC/Jf611EVOyxaKTRyMh/sLkRwIl/BaaFG9G4dVxJooqa/fuIbq+3BVzBrhheWHfG6GIV7d0kayY1g0aKfghmcSJtG7ACloZzNJPdmwAb0C1Bbrq3JphGUTW2dSa4BNZMI7AELtYIaNRVJpmtoYfA/QaaZ9ijNp8Qi+6jUuR7GwscHdJCukdLFjXbFlrZ2SNj344OUqNf0qAhAy+ZhmfZlRc027fnc+1EKL8QIs4L2zsMe5n5n5MApArXZEL8PRbuZfuEMbP/XX4l6D8dSvaQrW0o80tU4ZIWJBmV8HMnsAV9gIdrDEAjqGGoMDvBNeaRXnG16RjTRTo57sPuqMTi9JBuXr6P9dHofdczKGwATAyS/DUbH0zN4P3Qlc7bcnA0WMUpgx9ZPhrlo0zdgvegqSGt2zmOSXL99lCNuOG+4ps3wtFv3IHIXQRZ2IM7hYARcBdMa6WIM/bN6RtJTwduqoF4CpttAA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif