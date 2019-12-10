using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YTS.Data.Models
{
    public class User
    {
        public int Id;
        public string UserName;
        public string Sex;
        public string Phone;
        public int? Age;
        public decimal? Money;
    }
}