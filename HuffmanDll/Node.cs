using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    class Node
    {
        public byte Symbol { get; set; }
        public int Weight { get; set; }
        public Node Right_son { get; set; }
        public Node Left_son { get; set; }

        
        public Node(byte c, int weight)
        {
            this.Symbol = c;
            this.Weight = weight;
            Right_son = null;
            Left_son = null;
        }

        //Affiche la node, puis ses enfants
        public override String ToString()
        {
            String s = Symbol + " " + Weight + "\n";
            if (Left_son != null)
            {
                s += "      " + Left_son.ToString();
            }
            if (Right_son != null)
            {
                s += "      " + Right_son.ToString();
            }
            return s;
        }

  
        //Update du poids du noeud 
        public int NodeWeight()
        {
            int i = Weight;
            if (Right_son != null)
            {
                i += Right_son.Weight;
            }
            if (Left_son != null)
            {
                i += Left_son.Weight;
            }
            return i;
        }

        //Trouve l'encodage correspondant au noeud et à ses enfants
        public void Encode(Dictionary<bool[], byte> tabhuff, bool[] t)
        {
            if (Symbol != 0)
            {
                tabhuff.Add(t, Symbol);
            }
            else
            {
                if (Left_son != null)
                {
                    bool[] tmp_left = new bool[t.Length + 1];
                    t.CopyTo(tmp_left, 0);
                    tmp_left[t.Length] = false;
                    Left_son.Encode(tabhuff, tmp_left);
                }
                if (Right_son != null)
                {
                    bool[] tmp_right = new bool[t.Length + 1];
                    t.CopyTo(tmp_right, 0);
                    tmp_right[t.Length] = true;
                    Right_son.Encode(tabhuff, tmp_right);
                }

            }
        }
            public void Decode(List<bool> buffer, byte[] res, int pos)
            {
                if (Symbol == 0)
                {
                    if (buffer[0] == false)
                    {
                        buffer.RemoveAt(0);
                        Left_son.Decode(buffer, res, pos);
                    }
                    else
                    {
                        buffer.RemoveAt(0);
                        Right_son.Decode(buffer, res, pos);
                    }
                }
                else
                {
                    res[pos] = Symbol;
                }
            }
        

       
    }
}
