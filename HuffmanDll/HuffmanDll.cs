using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huffman;

namespace HuffmanDll
{
    public class HuffmanDll : MarshalByRefObject, IPlugin
    {
        public string PluginName { get; set; }
        
        public HuffmanDll()
        {
            PluginName = "Halfman";
        }
        public bool Compress(ref HuffmanData data)
        {
            Data tmp = new Data();
            if (data.uncompressedData.Count() > 0)
            {
                tmp.ReadFreq(data.uncompressedData);
                tmp.CreateTabHuff();

                data.compressedData = Encode(data.uncompressedData, tmp);
                data.frequency = tmp.SendList();
                data.sizeOfUncompressedData = data.compressedData.Count();
                return true;
            }
            return false;
            
        }

        public bool Decompress(ref HuffmanData data)
        {
            if (data.compressedData.Count() > 0)
            {
                Data tmp = new Data();
                tmp.CreateDicoFreq(data.frequency);
                tmp.CreateTabHuff();
               
                data.uncompressedData = Decode(data.compressedData, tmp);
                return true;
            }
            return false; 
        }


        static byte[] Encode(byte[] entry, Data data)
        {

            List<bool> bo = new List<bool>();
            bool[] buffer;
            foreach (byte c in entry)
            {
              
                buffer = data.TabHuffInv[c];
                foreach (bool tmpB in buffer)
                {
                    bo.Add(tmpB);
                }
            }
            return ConvertBooltoByte(bo);
        }


        /*static byte[] Decode(byte[] entry, Data data)
        {
            List<bool> tmp = ConvertBytetoBool(entry);
            List<bool> buffer = new List<bool>();
            byte[] res = new byte[data.nBSymbol()];
            
            int pos = 0;
            while (pos<res.Length)
            {
                buffer.Add(tmp[0]);
                tmp.RemoveAt(0);

                foreach (bool[] keys in data.TabHuff.Keys)
                {
                    if (EqualTabBool(buffer, keys))
                    {
                        res[pos] = (data.TabHuff[keys]);
                        pos++;
                        buffer.RemoveAll(entryS => true);
                    }
                }
                
            }
            return res;
        }*/

        static byte[] Decode(byte[] entry, Data data)
        {
            List<bool> tmp = ConvertBytetoBool(entry);
            byte[] res = new byte[data.nBSymbol()];
            data.TreeHuff.Decode(tmp,res, data.nBSymbol());
            return res;
        }

        public static List<bool> ConvertBytetoBool(byte[] s)
        {

            List<bool> res = new List<bool>();

            foreach (byte b in s)
            {
                byte stemp;

                for (int i = 7; i > -1; i--)
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


        public static byte[] ConvertBooltoByte(List<bool> s)
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
            for (int i = 0; i < s.Count; i++)
            {

                if (s[i])
                {
                    byte tmp = 1;

                    tmp <<= 7 - j;
                    res[pos] |= tmp;
                }
                j++;
                if (j >= 8)
                {
                    j = 0;
                    pos++;
                }
            }
        
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
                { return false; }
                   
            return res;
        }
    }
}

