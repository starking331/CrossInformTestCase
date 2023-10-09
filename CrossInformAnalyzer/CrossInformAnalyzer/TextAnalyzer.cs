using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInformAnalyzer
{
    public class TextAnalyzer
    {
        private string text;

        public TextAnalyzer()
        {
            text = "";
        }

        public string GetText()
        {
            return text;
        }

        public void ReadText(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Файла по этому пути не существует", nameof(filePath));
            }
            text = File.ReadAllText(filePath);
        }

        public IEnumerable<KeyValuePair<string, int>> GetPopularSubstrings(int substringLength, int substringsCount)
        {
            if (substringLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(substringLength), "Длина подстроки для поиска должна быть больше 1");
            }

            if (substringsCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(substringsCount), "Количество самых часто встречающихся подстрок должно быть больше 1");
            }

            var substrings = new ConcurrentDictionary<string, int>();

            Parallel.For(0, text.Length - substringLength + 1, firstIndex =>
            {
                var lastIndex = firstIndex + substringLength - 1;

                for (var i = firstIndex; i <= lastIndex; i++)
                {
                    if (!char.IsLetter(text[i]))
                        return;
                }

                var substring = text.Substring(firstIndex, substringLength);

                substrings.AddOrUpdate(substring, 1, (key, tripletCount) => tripletCount + 1);
            });

            var result = substrings
                .OrderByDescending(substring => substring.Value)
                .Take(substringsCount);

            return result;
        }



        public IEnumerable<KeyValuePair<string, int>> GetPopularSubstrings(int substringLength, int substringsCount, string filePath)
        {
            ReadText(filePath);
            return GetPopularSubstrings(substringLength, substringsCount);
        }
    }
}
