using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YTS.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public int? Age { get; set; }
        public decimal? Money { get; set; }
    }
}