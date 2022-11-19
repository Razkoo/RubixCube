using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neat_af
{
    class Cube
    {
        public char [][] cubeS { get; set; }
        public char [][] Scube { get; set; }

        public Cube(char [][] solved)
        {
            this.Scube = solved;
            setstart();
        }
        private void setstart()
        {
            this.cubeS = this.Scube;   
        }
        private char[][] SideRot(char[][] set, int whatside, bool shift)
        {
            int[] arr = new int[] { 0, 2, 4, 6, 1, 3, 5, 7 };
            if (shift)
            {
                for (int i = 0; i < 2; i++)
                {
                    char saveval = set[whatside][i * 4 + 0];
                    set[whatside][arr[i * 4 + 0]] = set[whatside][arr[i * 4 + 1]];
                    set[whatside][arr[i * 4 + 1]] = set[whatside][arr[i * 4 + 2]];
                    set[whatside][arr[i * 4 + 2]] = set[whatside][arr[i * 4 + 3]];
                    set[whatside][arr[i * 4 + 3]] = saveval;

                }

            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    char saveval = set[whatside][i * 4 + 3];
                    set[whatside][arr[i * 4 + 3]] = set[whatside][arr[i * 4 + 2]];
                    set[whatside][arr[i * 4 + 2]] = set[whatside][arr[i * 4 + 1]];
                    set[whatside][arr[i * 4 + 1]] = set[whatside][arr[i * 4 + 0]];
                    set[whatside][arr[i * 4 + 0]] = saveval;

                }

            }

            return set;
        }
        private char[][] Switch(char[][] set, int[] arr,bool shift, bool side, int whatside)
        {
            if (shift)
            {
                for (int i = 1; i < 4; i++)
                {
                    char saveval = set[arr[12]][i + 12];
                    set[arr[12]][arr[i + 12]] = set[arr[8]][arr[i + 8]];
                    set[arr[8]][arr[i + 8]] = set[arr[4]][arr[i + 4]];
                    set[arr[4]][arr[i + 4]] = set[arr[0]][arr[i + 0]];
                    set[arr[0]][arr[i + 0]] = saveval;

                }
            }
            else
            {
                for (int i = 1; i < 4; i++)
                {
                    char saveval = set[arr[0]][arr[i + 0]];
                    set[arr[0]][arr[i + 0]] = set[arr[4]][arr[i + 4]];
                    set[arr[4]][arr[i + 4]] = set[arr[8]][arr[i + 8]];
                    set[arr[8]][arr[i + 8]] = set[arr[12]][arr[i + 12]];
                    set[arr[12]][arr[i + 12]] = saveval;

                }
            }

            if (side)
            {
                set = SideRot(set, whatside, shift);
            }
            return set;
        }
        public override string ToString()
        {
            string[] arr = new string[] { string.Join("", cubeS[0]), string.Join("", cubeS[1]), string.Join("", cubeS[2]), string.Join("", cubeS[3]), string.Join("", cubeS[4]), string.Join("", cubeS[5]) };
            string arrhalfway = string.Join("", arr);
            char[] almostoutput = new char[108];
            for (int i = 0; i < arrhalfway.Length; i++)
            {
                almostoutput[2 * i] = arrhalfway[i];
                almostoutput[2 * i + 1] = ',';
            }
            string output;
            output = string.Join("" , almostoutput);
            return output;
        }
    

        public Cube Rotation(char c,bool shift, Cube cube)
        {
            int[] arr = new int[16];
            //turn x ={ side x (0) ,square1 , squar2 ,square3 ,side y (4) ,square1 , squar2 ,square3 ,side z (8) ,square1 , squar2 ,square3 ,side q(12) ,square1  , squar2 ,square3 }
            //r = {0,2,3,4,1,2,3,4,2,2,3,4,3,2,3,4}
            //l = {0,0,7,6,1,0,7,6,2,0,7,6,3,0,7,6}
            //v = {0,5,8,1,1,5,8,1,2,5,8,1,3,5,8,1}
            //u = {1,0,1,2,4,0,1,2,3,4,5,6,5,0,1,2}
            //h = {1,3,8,7,4,3,8,7,3,7,8,3,5,3,8,7}
            //d = {1,4,5,6,4,4,5,6,3,0,1,2,5,4,5,6}
            //b = {0,2,1,0,5,0,7,6,2,6,5,4,4,4,3,2}
            //m = {0,3,8,7,5,1,8,5,2,7,8,3,4,1,8,5}
            //f = {0,4,5,6,5,2,3,4,2,6,5,4,4,6,7,0}
            bool side = false;
            int whatside = 0;
            switch (c)
            {
                case 'r':
                    arr = new int[] { 0, 2, 3, 4, 1, 2, 3, 4, 2, 2, 3, 4, 3, 2, 3, 4 };
                    side = true;
                    whatside = 4;
                    break;
                case 'l':
                    arr = new int[] { 0, 0, 7, 6, 1, 0, 7, 6, 2, 0, 7, 6, 3, 0, 7, 6 };
                    side = true;
                    whatside = 5; 
                    break;
                case 'v':
                    arr = new int[] { 0, 5, 8, 1, 1, 5, 8, 1, 2, 5, 8, 1, 3, 5, 8, 1 };
                    break;
                case 'u':
                    arr = new int[] { 1, 0, 1, 2, 4, 0, 1, 2, 3, 4, 5, 6, 5, 0, 1, 2 };
                    side = true;
                    whatside = 0;
                    break;
                case 'h':
                    arr = new int[] { 1, 3, 8, 7, 4, 3, 8, 7, 3, 7, 8, 3, 5, 3, 8, 7 };
                    break;
                case 'd':
                    arr = new int[] { 1, 4, 5, 6, 4, 4, 5, 6, 3, 0, 1, 2, 5, 4, 5, 6 };
                    side = true;
                    whatside = 2;
                    break;
                case 'b':
                    arr = new int[] { 0, 2, 1, 0, 5, 0, 7, 6, 2, 6, 5, 4, 4, 4, 3, 2 };
                    side = true;
                    whatside = 3;
                    break;
                case 'm':
                    arr = new int[] { 0, 3, 8, 7, 5, 1, 8, 5, 2, 7, 8, 3, 4, 1, 8, 5 };
                    break;
                case 'f':
                    arr = new int[] { 0, 4, 5, 6, 5, 2, 3, 4, 2, 6, 5, 4, 4, 6, 7, 0 };
                    side = true;
                    whatside = 1;
                    break;
                case 'p':
                    cube.setstart();
                    return cube;
                    break;
            }
            cube.cubeS = cube.Switch(cube.cubeS, arr, shift, side, whatside);

            return cube;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Cube state;
            char[][] solvedcube = new char[6][];
            string s;
            char c;
            solvedcube[0] = new char[] { 'y', 'y', 'y', 'y', 'y', 'y', 'y', 'y', 'y' };
            solvedcube[1] = new char[] { 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b' };
            solvedcube[2] = new char[] { 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w' };
            solvedcube[3] = new char[] { 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g' };
            solvedcube[4] = new char[] { 'r', 'r', 'r', 'r', 'r', 'r', 'r', 'r', 'r' };
            solvedcube[5] = new char[] { 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o' };
            state = new Cube(solvedcube);
            
            Console.WriteLine(state);
            while (true)
            {
                Console.WriteLine("input rotation");
                s = Console.ReadLine();
                c = s[0];
                state = state.Rotation(c, false, state);

                Console.WriteLine(state );
            }
        }
    }
}
