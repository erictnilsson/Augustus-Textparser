using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augustus_Textparser
{
    class Program
    {
        /*
         * 1. parse the file holding the averages for each country, store it as a dict<ctry, val>
         * 2. parse through the actual dataset
         * 3. add two new columns for each dataset; 'favg', 'mavg'
         * 4. for each value in the 'fctry'-column, fetch the avg from the dict and add it to the 'favg'-column
         * 5. for each value in the 'mctry'-column, fetch the avg from the dict and add it to the 'mavg'-column
         * 6. create new files with the complete dataset and save to the same directory 
         */
        static void Main(string[] args)
        {
            ParseFile("C: \\Users\\Eric Nilsson\\Desktop\\bajs.txt");
        }

        static void ParseFile(string filepath)
        {
            Dictionary<string, string[]> averages = new Dictionary<string, string[]>();

            var text = File.ReadAllLines(filepath);
            var row = new string[0];

            for (int i = 0; i < text.Length; i++)
            {

                row = text[i].Split('\t');
                if (!averages.ContainsKey(row[5]))
                {
                    averages.Add(row[5], new string[] { row[500], row[501] });
                }
            }

            for (int i = 0; i < text.Length; i++)
            {
                row = text[i].Split('\t');
                if (i == 0)
                {
                    row[502] = "favggay";
                    row[503] = "mavggay";
                    row[504] = "favgwom";
                    row[505] = "mavgwom";
                }
                else
                {
                    // father
                    if (row[158].Equals("66"))
                    {
                        row[502] = averages[row[5]][0];
                        row[504] = averages[row[5]][1];
                    }
                    else if (row[158].Equals("04"))
                    {
                        row[502] = "";
                        row[503] = "";
                    }
                    else
                    {
                        if (averages.ContainsKey(row[158]))
                        {
                            row[502] = averages[row[158]][0];
                            row[504] = averages[row[158]][1];
                        }
                        else
                        {
                            row[502] = "#ERROR";
                            row[504] = "#ERROR";
                        }

                    }

                    //mother
                    if (row[160].Equals("66"))
                    {
                        row[503] = averages[row[5]][0];
                        row[505] = averages[row[5]][1];
                    }
                    else if (row[160].Equals("04"))
                    {
                        row[503] = "";
                        row[505] = "";
                    }
                    else
                    {
                        if (averages.ContainsKey(row[160]))
                        {
                            row[503] = averages[row[160]][0];
                            row[505] = averages[row[160]][1];
                        }
                        else
                        {
                            row[503] = "#ERROR";
                            row[505] = "#ERROR";
                        }
                    }
                }
                text[i] = String.Join("\t", row);
            }
            File.WriteAllLines(filepath, text);

        }

        static Dictionary<string, string> GetCountryAverage(string filepath)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                // full text as a string[]
                var text = File.ReadAllLines(filepath);

                foreach (var row in text)
                {
                    var foo = row.Split('\t');
                    dict.Add(foo[0], foo[1]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dict;
        }
    }
}
