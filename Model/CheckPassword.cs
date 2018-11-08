using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteBook;

namespace NoteBook
{
    public class CheckPassword
    {
        private int strength = 0;

        public int CheckPass(string pass)
        {
            if (ContainsDigit(pass)) ++strength;
            if (ContainsLowerLetter(pass)) ++strength;
            if (ContainsPunctuation(pass)) ++strength;
            if (ContainsSeparator(pass)) ++strength;
            if (ContainsUpperLetter(pass)) ++strength;
            if (pass.Length >= 10) strength += 2;

            return strength;
        }

        private static bool ContainsLowerLetter(string pass)
        {
            foreach (char c in pass)
            {
                if ((Char.IsLetter(c)) && (Char.IsLower(c)))
                    return true;
            }
            return false;
        }

        private static bool ContainsUpperLetter(string pass)
        {
            foreach (char c in pass)
            {
                if ((Char.IsLetter(c)) && (Char.IsUpper(c)))
                    return true;
            }
            return false;
        }

        private static bool ContainsDigit(string pass)
        {
            foreach (char c in pass)
            {
                if (Char.IsDigit(c))
                    return true;
            }
            return false;
        }

        private static bool ContainsPunctuation(string pass)
        {
            foreach (char c in pass)
            {
                if (Char.IsPunctuation(c))
                    return true;
            }
            return false;
        }

        private static bool ContainsSeparator(string pass)
        {
            foreach (char c in pass)
            {
                if (Char.IsSeparator(c))
                    return true;
            }
            return false;
        }
    }
}

