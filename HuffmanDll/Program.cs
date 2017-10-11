using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{

 

    class Program
    {
        static void Main(string[] args)
        {
            /* TEST DE CONVERTION
            List<bool> t = new List<bool>();
            t.Insert(0,true);
            t.Insert(0,true);
            t.Insert(0,true);
            t.Insert(0,true);

            t.Insert(0,true);
            t.Insert(0,true);
            t.Insert(0,true);
            t.Insert(0,true);d

            t.Insert(0,false);
            t.Insert(0,false);
            t.Insert(0,false);
            t.Insert(0,false);

            t.Insert(0,false);
            t.Insert(0,false);
            t.Insert(0,false);
            t.Insert(0,true);

            t.Insert(0,true);

            //Envoie 1 1000 0000 1111 1111
            // 19 bits, donc lgt = 3
            // on pad la fin
            //Dans la table  1100 0000 0111 1111 1000 0000
            //               

            ConvertBooltoByte(t);

            Test du converter de byte
            */
            //s=Encode("phrase de test");
            String s = "aaaddbb  abc";
            s = ("phrase de test");
            Data data = new Data();
            byte[] tmp = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
                tmp[i] = (byte) s[i];
          
             data.ReadFreq(  tmp ); //Lecture d'une chaine entrée + Gestion chaine vide

            var test = data.SendList();
            for(int i = 0;i<test.Count;i++)
                Console.Write(test[i].ToString());
          
            //Encode(s,data);
            // 1111 1110 1101 0101 0000 1101 1000 0000
            Decode(Encode(s, data), data);
           //IT WORKS
        }

        static byte[] Encode(String s,Data data)
        {
            Tree tree = new Tree();
            tree.ConstructList(data.DicoFreq);
            tree.MergeNode();

            tree.Encode(data.TabHuff);
            foreach (bool[] b in data.TabHuff.Keys)
            {
                var sb = new StringBuilder("new bool[] { ");
                foreach (var c in b)
                {
                    sb.Append(c + ", ");
                }
                sb.Append("}");
                Console.WriteLine(sb.ToString() + " " + data.TabHuff[b]);
            }
            List<bool> bo = new List<bool>();

            data.CreateTabHuffInv();

            foreach (byte c in s)
            {
                bool[] tmp;
                tmp = data.TabHuffInv[c];
                foreach(bool tmpB in tmp)
                {
                    bo.Add(tmpB);
                }
            }
            return ConvertBooltoByte(bo);
        }

        static void Decode( byte[] s,Data data)
        {
            List<bool> tmp = ConvertBytetoBool(s);
            List<bool> buffer = new List<bool>();
            StringBuilder res = new StringBuilder("");
            while (tmp.Count != 0)
            {


                buffer.Add(tmp[0]);
                tmp.RemoveAt(0);
          
                foreach(bool[] keys in data.TabHuff.Keys)
                {
                    if (EqualTabBool(buffer, keys))
                    {
                        res.Append((char ) data.TabHuff[keys]);
                        buffer.RemoveAll(entry => true);
                    }
                }
        
            
            }
            Console.WriteLine(res.ToString());
        }
        public static List<bool> ConvertBytetoBool(byte[] s)
        {

            List<bool> res = new List<bool>();

            foreach (byte b in s)
            {
                byte stemp;
                
                for(int i=7;i>-1;i--)
                {
                    stemp = b;
                    byte tmp = 1;

                    tmp <<= i;
                    stemp &= tmp;
                    if (stemp > 0)
                        res.Add(true);
                    else
                        res.Add(false);
                        
                }
            }
            foreach (bool i in res)
            {
                Console.WriteLine(i);
            }
            return res;
        }

 
        public static byte[] ConvertBooltoByte( List<bool> s)
        {
            
            int byteLength;
            //Creation du tableau de byte
            if (s.Count % 8 == 0)
                byteLength = s.Count / 8;
            else
                byteLength = s.Count / 8 + 1;
            byte[] res = new byte[byteLength];

            //Init du tableau de byte
            for (int i = 0; i < byteLength; i++)
                res[i] = 0;
            
            //Parcours de la liste
            int j = 0;
            int pos = 0;
            for (int i=0;i<s.Count;i++)
            {
                
                if (s[i])
                {
                    byte tmp = 1;
         
                    tmp <<= 7-j;
                    res[pos] |= tmp;
                }
                j++; 
                if(j>=8)
                {
                    j = 0;
                    pos++;
                }
            }
            foreach(byte b in res)
                Console.WriteLine(b.ToString());
            return res;
        }

        //Cherche une égalité case par case de tableau de boolean
        public static bool EqualTabBool(List<bool> t1, bool[] t2)
        {
            if (t1.Count != t2.Length)
                return false;

            bool res = true;
            for (int i = 0; i < t1.Count; i++)
                if (t1[i] != t2[i])
                    res = false;
            return res;
        }
    }

}
