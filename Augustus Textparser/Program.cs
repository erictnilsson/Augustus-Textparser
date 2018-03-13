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
            //Console.WriteLine("Enter the filepath to the dataset");
            //var input = Console.ReadLine().Trim(new char[] { ' ', '\"' });

            ParseFile(@"C:\Users\Eric Nilsson\Downloads\till-eric (3).txt");
            Console.Read();
        }

        static void ParseFile(string filepath)
        {
            Dictionary<string, string[]> averages = new Dictionary<string, string[]>();

            var text = File.ReadAllLines(filepath);
            var row = new string[0];
            var tick = 1;

            for (int i = 0; i < text.Length; i++)
            {

                row = text[i].Split('\t');

                var ess = row[1];
                var cntry = row[2];
                var avggay = row[3];
                var avgwom = row[4];
                var fcntry = row[5];
                var mcntry = row[6];

                // if the dict doesn't contain the cntry-code as key
                if (!averages.ContainsKey(cntry))
                    // then add it 
                    averages.Add(cntry, new string[] { avggay, avgwom });
            }

            for (int i = 0; i < text.Length; i++)
            {
                var listRow = text[i].Split('\t').ToList();

                var ess = listRow[1];
                var cntry = listRow[2];
                var avggay = listRow[3];
                var avgwom = listRow[4];
                var fcntry = listRow[5];
                var mcntry = listRow[6];

                if (i == 0)
                {
                    listRow.Add("favggay");
                    listRow.Add("favgwom");
                    listRow.Add("mavggay");
                    listRow.Add("mavgwom");
                }
                else
                {
                    // father
                    if (fcntry.Equals("66") || fcntry.Equals("04") || fcntry.Equals("01") || fcntry.Equals("02") || fcntry.Equals("03") || fcntry.Equals("77") || fcntry.Equals("88"))
                    {
                        listRow.Add("");
                        listRow.Add("");
                    }
                    else
                    {
                        if (averages.ContainsKey(fcntry))
                        {
                            listRow.Add(averages[fcntry][0]);
                            listRow.Add(averages[fcntry][1]);
                        }
                        else
                        {
                            listRow.Add("");
                            listRow.Add("");
                        }

                    }

                    //mother
                    if (mcntry.Equals("66") || mcntry.Equals("04") || mcntry.Equals("01") || mcntry.Equals("02") || mcntry.Equals("03") || mcntry.Equals("77") || mcntry.Equals("88"))
                    {
                        listRow.Add("");
                        listRow.Add("");
                    }
                    else
                    {
                        if (averages.ContainsKey(mcntry))
                        {
                            listRow.Add(averages[mcntry][0]);
                            listRow.Add(averages[mcntry][1]);
                        }
                        else
                        {
                            listRow.Add("");
                            listRow.Add("");
                        }
                    }
                }
                text[i] = String.Join("\t", listRow.ToArray());
            }
            File.WriteAllLines(filepath.Substring(0, filepath.LastIndexOf('\\') + 1) + "parsedTextFile" + tick + ".txt", text);
        }
    }
}
