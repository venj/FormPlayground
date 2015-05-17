using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using me.venj.Extensions;

namespace FormPlayground
{
    public class Document
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public byte Priority { get; private set; }

        public Document(string title, string content, byte priority) 
        {
            this.Title = title;
            this.Content = content;
            this.Priority = priority;
        }

        public Document(string title, string content) : this(title, content, priority : 1) { }
    }

    public class DocumentManager
    {
        private readonly Queue<Document> documentQueue = new Queue<Document>();
        
        public void AddDocument(Document doc)
        {
            lock (this)
            {
                documentQueue.Enqueue(doc);
            }
        }

        public Document GetDucoment()
        {
            Document doc = null;
            lock (this)
            {
                doc = documentQueue.Dequeue();
            }
            return doc;
        }

        public bool IsDocumentAvailable
        {
            get
            {
                return documentQueue.Count > 0;
            }
        }
    }

    public class ProcessDocuments
    {
        public static void Start(DocumentManager dm)
        {
            Task.Factory.StartNew(new ProcessDocuments(dm).Run);
        }

        private DocumentManager documentManager;

        protected ProcessDocuments(DocumentManager dm)
        {
            if (dm == null)
            {
                throw new ArgumentNullException("dm");
            }
            documentManager = dm;
        }

        protected void Run()
        {
            while (true)
            {
                if (documentManager.IsDocumentAvailable)
                {
                    Document doc = documentManager.GetDucoment();
                    Console.WriteLine("Processing document {0}", doc.Title);
                }
                Thread.Sleep(new Random().Next(20));
            }
        }
    }

    public class PriorityDocumentManager
    {
        private readonly LinkedList<Document> documentList;
        private readonly List<LinkedListNode<Document>> priorityNodes;

        public PriorityDocumentManager()
        {
            documentList = new LinkedList<Document>();
            priorityNodes = new List<LinkedListNode<Document>>(10);

            10.Times( () =>  priorityNodes.Add(new LinkedListNode<Document>(null)) );
        }

        public void AddDocument(Document d)
        {
            if (d == null)
            {
                throw new ArgumentNullException("d");
            }
            AddDocumentToPriorityNode(d, d.Priority);
        }

        private void AddDocumentToPriorityNode(Document doc, int priority)
        {
            if (priority > 9 || priority < 0)
            {
                throw new ArgumentException("Priority must be between 0 and 9");
            }

            if (priorityNodes[priority].Value == null)
            {
                --priority;
                if (priority >= 0)
                {
                    AddDocumentToPriorityNode(doc, priority);
                }
                else
                {
                    documentList.AddLast(doc);
                    priorityNodes[doc.Priority] = documentList.Last;
                }
                return;
            }
            else
            {
                LinkedListNode<Document> prioNode = priorityNodes[priority];
                if (priority == doc.Priority)
                {
                    documentList.AddAfter(prioNode, doc);
                    priorityNodes[doc.Priority] = prioNode.Next;
                }
                else
                {
                    LinkedListNode<Document> firstPrioNode = prioNode;
                    while (firstPrioNode.Previous != null && firstPrioNode.Previous.Value.Priority == prioNode.Value.Priority)
                    {
                        firstPrioNode = prioNode.Previous;
                        prioNode = firstPrioNode;
                    }
                    documentList.AddBefore(firstPrioNode, doc);
                    priorityNodes[doc.Priority] = firstPrioNode.Previous;
                }
            }
        }

        public void DisplayAllNodes()
        {
            documentList.Each((document) => {
                Console.WriteLine("Priority: {0}, title: {1}", document.Priority, document.Title);
            });
        }
    }
}

