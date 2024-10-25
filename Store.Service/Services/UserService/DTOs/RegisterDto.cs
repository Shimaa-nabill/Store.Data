using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.UserService.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}



//TEST
//"id": "c6540a2a-55dd-4446-a0be-d73ac0476750",
//  "displayName": "shimaa",
//  "email": "shimaa@example.com",
//  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InNoaW1hYUBleGFtcGxlLmNvbSIsImdpdmVuX25hbWUiOiJzaGltYWEiLCJVc2VySWQiOiJjNjU0MGEyYS01NWRkLTQ0NDYtYTBiZS1kNzNhYzA0NzY3NTAiLCJVc2VyTmFtZSI6InNoaW1hYSIsIm5iZiI6MTcyOTc5NDA4NywiZXhwIjoxNzI5ODgwNDg3LCJpYXQiOjE3Mjk3OTQwODcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxNDcifQ.mv4iA9fz8aZsoOYRYq8K_fDXUUcXSgnxPM5rOJrCOWc"
