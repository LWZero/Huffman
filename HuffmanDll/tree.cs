using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    //Arbre de Noeud
    class Tree
    {
        public List<Node> NodeList { get; set; }

        public Tree()
        {
            NodeList = new List<Node>();
        }

        //Construit la liste initiale de noeuds dans l'arbre
        public void ConstructList(Dictionary<byte, int> dico)
        {
            foreach (byte c in dico.Keys)
            {
                NodeList.Add(new Node(c, dico[c]));
            }
            Sort();

        }

        //Trie de la liste des noeuds
        public void Sort()
        {
            NodeList.Sort((node1, node2) => node1.Weight.CompareTo(node2.Weight));
        }

        //Affiche l'intégralité de l'arbre
        public void Print()
        {
            foreach (Node n in NodeList)
            {
                Console.Write(n.ToString());
            }
        }

        //Etape de l'algorithme de Huffman : Fusion de 2 noeuds
        public void MergeNode()
        {
            //Tant qu'on à plus de deux élements
            while (NodeList.Count > 1)
            {
                //Création d'un nouveau noeud
                Node root = new Node(0, 0);
                root.Left_son = NodeList[0];
                root.Right_son = NodeList[1];
                root.Weight = root.NodeWeight();
                //On retire les deux élements et on ajoute le nouveau
                NodeList.RemoveRange(0, 2);
                NodeList.Add(root);
                

                if (NodeList.Count > 1)
                {
                    Sort();
                }

            }
        }

        //Encode chaque symbole avec son tableau de bool
        public void Encode(Dictionary<bool[], byte> tabhuff)
        {
            NodeList[0].Encode(tabhuff, new bool[0]);
        }

        public void EncodeSolo(Dictionary<bool[], byte> tabhuff)
        {
            NodeList[0].Encode(tabhuff, new bool[1]);
        }

        public void Decode(List<bool> buffer, byte[] res,int nbEle)
        {
            for (int pos = 0; pos < nbEle; pos++)
                NodeList[0].Decode(buffer, res, pos);
        }
    }
}
