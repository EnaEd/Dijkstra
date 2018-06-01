using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("the number of tests");
            int s = 0;
            s = Convert.ToInt32(Console.ReadLine());
            if (s <= 10)
            {
                Graph g = new Graph();
                g.InitGraph();
                //output matrix adjacences
                //for (int i = 0; i < g.AdjMatrix.GetLength(0); i++)
                //{
                //    for (int j = 0; j < g.AdjMatrix.GetLength(1); j++)
                //    {
                //        Console.Write(g.AdjMatrix[i, j]);
                //    }
                //    Console.WriteLine();
                //}
                Console.WriteLine("enter number of paths to find ");
                int numbOfPathFind = Convert.ToInt32(Console.ReadLine());
                if (numbOfPathFind <= 100)
                {
                    for (int k = 0; k < numbOfPathFind; k++)
                    {
                        string cityFrom, cityTo;
                        Console.WriteLine("destination cities");
                        string[] arrCity = Console.ReadLine().Split();
                        cityFrom = arrCity[0];//one more
                        cityTo = arrCity[1];  //hard binding

                        g.Path(cityFrom, cityTo);
                    }
                    Console.WriteLine("******end test***********");
                }
                else
                {
                    Console.WriteLine("incorrect data");
                    return;
                }
               
            }
            else
                Console.WriteLine("value must be between 1 10");
        }

        // class vertex 
        class Vertex
        {
            public string NameVer { get; set; }//property access
            public bool IsVisit { get; set; }//Flag

            public Vertex(string name)
            {//constructor
                NameVer = name;
                IsVisit = false;
            }
        }

        //class Graph
        class Graph
        {
            public int MaxVert = 10000;//max vertexes
            public List<Vertex> VertexList = new List<Vertex>();//vertex list
            public int[,] AdjMatrix { get; set; }//matrix adjancecy
            public DistPar[] sPath;// data array of min path
            int min = 0;
            int minIndex = 0;
            int numbOfCit = 0;

            public void InitGraph()
            {//graph initizialtion

                //query list for input
                Console.WriteLine("enter number of cities");
                try
                {
                    numbOfCit = Convert.ToInt32(Console.ReadLine());
                    if (numbOfCit > MaxVert)
                    {
                        Console.WriteLine("wrong quantity");
                        return;
                    }
                    AdjMatrix = new int[numbOfCit, numbOfCit];//new array autoinit 0
                    for (int k = 0; k < numbOfCit; k++)//we add data based on number of cities 
                    {
                        Console.WriteLine("enter name city");
                        string name = Console.ReadLine();
                        Vertex v = new Vertex(name);//create new obj Vertex
                        VertexList.Add(v);          //and push in list vertex

                        Console.WriteLine("enter neighbors city");
                        int neighb = Convert.ToInt32(Console.ReadLine());//count neightbors
                        for (int i = 0; i < neighb; i++)
                        {
                            Console.WriteLine("enter index neighbors and transport cost");
                            string strtpm = Console.ReadLine();//get in data
                            string[] arrTmp = strtpm.Split();//divide the string into the characters we need
                            AdjMatrix[VertexList.Count - 1, Convert.ToInt32(arrTmp[0]) - 1] = Convert.ToInt32(arrTmp[1]);//hard binding,but...why not
                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            public void Path(string _cityFrom, string _cityTo)
            {

                sPath = new DistPar[VertexList.Count];
                for (int i = 0; i < VertexList.Count; i++)
                {
                    sPath[i] = new DistPar { Distance = 10000 };//start transport cost
                }

                for (int i = 0; i < VertexList.Count; i++)
                {
                    if (VertexList[i].NameVer.Equals(_cityFrom))
                    {
                        sPath[i].Distance = 0;//path start point
                        Console.WriteLine("hi " + VertexList[i].NameVer+" "+i);//testing
                        goto M;
                    }
                    //Console.WriteLine("The city was not found");

                }


               M: do//start algorithm
                {
                    minIndex = 10000;//start value 
                    min = 10000;     //start value
                    for (int j = 0; j < VertexList.Count; j++)
                    {
                        if (sPath[j].Distance < min && VertexList[j].IsVisit == false)//if vertex no visit and cost <start value
                        {
                            min = sPath[j].Distance;//change places
                            minIndex = j;
                        }
                        if (minIndex != 10000)
                        {
                            for (int k = 0; k < VertexList.Count; k++)//sample min cost 
                            {
                                if (AdjMatrix[minIndex, k] > 0)
                                {
                                    int tmp = min + AdjMatrix[minIndex, k];
                                    if (tmp < sPath[k].Distance)
                                    {
                                        sPath[k].Distance = tmp;//add min cost to array
                                    }
                                }
                            }
                            VertexList[minIndex].IsVisit = true;//change flag
                        }
                    }

                } while (minIndex < 10000);

                for (int i = 0; i < VertexList.Count; i++)
                {
                    if (VertexList[i].NameVer.Equals(_cityTo))
                    {
                        Console.WriteLine("hi+" + sPath[i].Distance);
                    }
                }
                //refresh data
                for (int i = 0; i < VertexList.Count; i++) {
                    VertexList[i].IsVisit = false;
                }
            }
        }
        //class DistPar
        class DistPar
        {
            public int Distance { get; set; }//cost transport
        }
    }
}
