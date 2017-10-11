using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class Data
    {
        //Reminder, mais on envoie le dico des frequences avec la séquence sans le tableau de huffman > il faudrai les séparer ( en theorie)
        //Besoin de deux dico de huffman, parce qu'on doit retrouver les tab de bool selon les char comme key
        public Dictionary<byte, int> DicoFreq { get; set; }
        public Dictionary<bool[], byte> TabHuff { get; set; }
        public Dictionary<byte,bool[]> TabHuffInv { get; set; }
        public Tree TreeHuff { get; set; }

        public Data()
        {
            DicoFreq = new Dictionary<byte, int>();
            TabHuff = new Dictionary<bool[], byte>();
            TabHuffInv = new Dictionary<byte, bool[]>();
            TreeHuff = new Tree();
        }

   

        //Lecture d'une String et transformation en tableau de fréquence
        public void ReadFreq(byte[] s)
        {
            foreach (byte c in s)
            {
                if (DicoFreq.ContainsKey(c))
                {
                    DicoFreq[c]++;
                }
                else
                {
                    DicoFreq.Add(c, 1);
                }
            }
        }

        public void CreateTabHuffInv()
        {
            foreach(bool[] s in TabHuff.Keys)
            {
                TabHuffInv.Add(TabHuff[s], s);
            }
        }

        public List<KeyValuePair<byte,int>> SendList()
        {
            List<KeyValuePair<byte, int>> buffer = new List<KeyValuePair<byte, int>>();
            foreach(byte key in DicoFreq.Keys)
            {
                buffer.Add(new KeyValuePair<byte, int>(key, DicoFreq[key]));
            }
       
           return buffer;
        }

        public void CreateTabHuff()
        {
           
            TreeHuff.ConstructList(DicoFreq);
            if (DicoFreq.Count > 1)
            {
                TreeHuff.MergeNode();

                TreeHuff.Encode(TabHuff);
            }
            else
                TreeHuff.EncodeSolo(TabHuff);
            CreateTabHuffInv();

        }

        public void CreateDicoFreq(List<KeyValuePair<byte,int>> entry)
        {
            for(int i=0;i<entry.Count;i++)
            {
                DicoFreq.Add(entry[i].Key, entry[i].Value);
            }
        }

        public int nBSymbol()
        {
            int nb = 0;
            foreach(byte key in DicoFreq.Keys)
            {
                nb += DicoFreq[key];
            }
            return nb;
        }
    }


}
