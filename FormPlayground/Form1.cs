using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using me.venj.Extensions;

namespace FormPlayground
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            DocumentManager dm = new DocumentManager();
            ProcessDocuments.Start(dm);

            for (int i = 0; i < 1000; i++)
            {
                var doc = new Document("Doc " + i.ToString(), "content");
                dm.AddDocument(doc);
                Console.WriteLine("Add document {0}", doc.Title);
                Thread.Sleep(new Random().Next(20));
            }*/

            var pdm = new PriorityDocumentManager();
            pdm.AddDocument(new Document("One", "Sample", 8));
            pdm.AddDocument(new Document("Two", "Sample", 3));
            pdm.AddDocument(new Document("Three", "Sample", 4));
            pdm.AddDocument(new Document("Four", "Sample", 8));
            pdm.AddDocument(new Document("Five", "Sample", 1));
            pdm.AddDocument(new Document("Six", "Sample", 9));
            pdm.AddDocument(new Document("Seven", "Sample", 1));
            pdm.AddDocument(new Document("Eight", "Sample", 1));
            pdm.DisplayAllNodes();

            List<int> l = new List<int>(new int[] { 1, 2, 3, 4, 5, 6 });

            IEnumerable<string> s = l.Map( (element) => (element * element).ToString() );

            foreach (var str in s)
            {
                Console.WriteLine("{0}", str);
            }

            var total = l.Reduce("g", (seed, f) => {
                seed = seed.ToString() + " ";
                return seed + f.ToString();
            });
            Console.WriteLine("{0}", total);
        }
    }
}
