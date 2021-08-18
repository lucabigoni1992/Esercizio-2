using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Esercizio_2.Controllers
{
    public class TextElaborator
    {
        public int CountAsync(string text)
        {
            return Regex.Matches(text, @"[A-Za-z0-9]+").Count;//come parole intendo sequenze alfanumeriche
        }
    }
}