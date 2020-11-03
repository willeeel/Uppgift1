using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class Customer
    {
        [JsonProperty(propertyName: "id")]
        public int Id { get; set; }

        [JsonProperty(propertyName: "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(propertyName: "lastName")]
        public string LastName { get; set; }

        [JsonProperty(propertyName: "email")]
        public string Email { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";
    }

}