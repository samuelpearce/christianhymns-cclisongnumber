using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristianHymnsCCLISongNumber
{
    class OutputCcliIdFile
    {
        private readonly String outputPath;
        private readonly StreamWriter file;

        public OutputCcliIdFile()
        {
            this.outputPath = @"ch-ccli.txt";
            file = new StreamWriter(this.outputPath);
        }

        public void Output(String chId, String christianHymnsId, bool isPublicDomain, String title, String authors, String metre)
        {
            file.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t",
                chId,
                christianHymnsId,
                isPublicDomain ? 1 : 0,
                title,
                authors,
                metre));
            file.Flush();
        }
    }
}
