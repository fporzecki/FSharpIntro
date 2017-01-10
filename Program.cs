using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translations;

namespace fsharpcsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string source = "people.xml";

            try
            {
                var peopleList = Translate(source, From.Xml, Types.ToPerson);
                foreach (var item in peopleList)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            catch (System.NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static List<Types.Person> Translate(string textSource,
                                    Func<string, Tuple<List<string>,
                                        List<string>>> extract,
                                    Func<string, string,
                                        Types.Person> translate)
        {
            var personTuple = extract(textSource);
            var personList = new List<Types.Person>();
            try
            {
                for (int i = 0; i < personTuple.Item1.Count; i++)
                {
                    personList.Add(translate(
                        personTuple.Item1[i],
                        personTuple.Item2[i]));
                } 
                return personList;
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
