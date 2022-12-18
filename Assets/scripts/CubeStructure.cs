using System;
using UnityEngine;

public class CubeStructure : MonoBehaviour
{
    public GameObject gc, bc, wc, yc, rc, oc, gowc, goyc, bowc, boyc, gwrc, gyrc, bwrc, byrc, gwc, gyc, grc, goc, bwc, byc, brc, boc, wrc, woc, yrc, yoc;
    Manager manager;

    class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        // Constructor
        public Vector(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        // Multiply a vector by matrix
        public static Vector[] VectorXMatrix(Vector[] vectors, int[] indexes, int[,] matrix)
        {
            float x, y, z;

            foreach (int i in indexes)
            {
                x = vectors[i].X; y = vectors[i].Y; z = vectors[i].Z;

                vectors[i].X = x * matrix[0, 0] + y * matrix[0, 1] + z * matrix[0, 2];
                vectors[i].Y = x * matrix[1, 0] + y * matrix[1, 1] + z * matrix[1, 2];
                vectors[i].Z = x * matrix[2, 0] + y * matrix[2, 1] + z * matrix[2, 2];
            }
            return vectors;
        }

        // True = point exist, False = point doesn't
        public static bool PointExist(Vector v, int axis, char plane)
        {
            return (plane == 'x' && v.X == axis) || (plane == 'y' && v.Y == axis) || (plane == 'z' && v.Z == axis);
        }

        // Print function
        public override string ToString()
        {
            return $"[{this.X},{this.Y},{this.Z}]";
        }
    }

    class Move
    {
        private GameObject[] Objects { get; set; }
        private Vector[] Vectors { get; set; }
        public char Rotation { get; set; }

        // Constructor
        public Move(Vector[] vectors, GameObject[] objs)
        {
            this.Vectors = vectors;
            this.Objects = objs;
            this.Rotation = ' ';
        }

        // Rotate the cube
        // LowerCase = ClockWise, UpperCase = Counter ClockWise
        public static Vector[] Spin(Move newMove)
        {
            int[,] pMatrix = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            int[] indexes = Org(newMove);
            char rot = newMove.Rotation;

            if (rot == 'r' || rot == 'l' || rot == 'v') // X axis clockwise
            {
                pMatrix[0, 0] = 1;
                pMatrix[2, 1] = 1;
                pMatrix[1, 2] = -1;
            }
            else if (rot == 'b' || rot == 'f' || rot == 'm') // Y axis clockwise
            {
                pMatrix[1, 0] = 1;
                pMatrix[0, 1] = -1;
                pMatrix[2, 2] = 1;
            }
            else if (rot == 'u' || rot == 'h' || rot == 'd') // Z axis clockwise
            {
                pMatrix[2, 0] = -1;
                pMatrix[1, 1] = 1;
                pMatrix[0, 2] = 1;

            }
            else if (rot == 'R' || rot == 'L' || rot == 'V') // X axis counter clockwise
            {
                pMatrix[0, 0] = 1;
                pMatrix[2, 1] = -1;
                pMatrix[1, 2] = 1;
            }
            else if (rot == 'B' || rot == 'F' || rot == 'M') // Y axis counter clockwise
            {
                pMatrix[1, 0] = -1;
                pMatrix[0, 1] = 1;
                pMatrix[2, 2] = 1;
            }
            else if (rot == 'U' || rot == 'H' || rot == 'D') // Z axis counter clockwise
            {
                pMatrix[2, 0] = 1;
                pMatrix[1, 1] = 1;
                pMatrix[0, 2] = -1;
            }

            Rotate90(newMove, rot, indexes);
            return Vector.VectorXMatrix(newMove.Vectors, indexes, pMatrix);
        }

        // Find the indexes of the vectors in the wanted rotation session
        private static int[] Org(Move newMove)
        {
            bool flagx = false, flagy = false, flagz = false;
            char rot = char.ToLower(newMove.Rotation);
            int index = 0, axis = 0;
            int[] indexes;

            // Check if rotation needs 8 or 9 vectors
            if (rot == 'v' || rot == 'h' || rot == 'm') indexes = new int[8];
            else indexes = new int[9];

            // Find the axis of the vectors we need to rotate
            if (rot == 'r' || rot == 'b' || rot == 'u') axis = 1;
            else if (rot == 'v' || rot == 'h' || rot == 'm') axis = 0;
            else axis = -1;

            // Check what axis the rotation is on
            if (rot == 'r' || rot == 'l' || rot == 'v') flagx = true;
            else if (rot == 'b' || rot =='f' || rot == 'm') flagz = true;
            else flagy = true;

            for (int i = 0; i < 26; i++)
            {
                // Take to the array only points that are on the wanted axis
                if ((Vector.PointExist(newMove.Vectors[i], axis, 'x') && flagx) || (Vector.PointExist(newMove.Vectors[i], axis, 'y') && flagy)
                    || Vector.PointExist(newMove.Vectors[i], axis, 'z') && flagz)
                {
                    indexes[index++] = i;
                }
            }
            return indexes;
        }

        // Unity function - rotate the cube 90 degrees
        private static void Rotate90(Move newMove, char mv, int[] indexes)
        {
            int xd = 00, yd = 0, zd = 0, neg = 1;

            // Shift or normal move
            if (mv == char.ToUpper(mv)) neg = -1;
            mv = char.ToLower(mv);

            // Find direction
            if (mv == 'r' || mv == 'v' || mv == 'l') yd = 90 * neg;
            else if (mv == 'b' || mv == 'm' || mv == 'f') zd = 90 * neg;
            else xd = 90 * neg;

            // Rotate degrees
            foreach (int i in indexes)
                newMove.Objects[i].transform.Rotate(-yd, xd, zd, Space.World);
        }
    }

    class Manager
    {
        private bool rm, lm, bm, um, dm, fm, vm, hm, mm, shiftm, flag = false;
        private System.Random rnd = new System.Random();
        private GameObject[] _VectorsO { get; set; }
        private Vector[] _Vectors { get; set; }
        private Move _Move { get; set; }
        private char mv, rotation;

        private char[] MOVES = { 'r', 'l', 'b', 'v', 'm', 'f', 'u', 'h', 'd', 'R', 'L', 'B', 'V', 'M', 'F', 'U', 'H', 'D' };
        private const int SCRAMBLE_AMOUNT = 0;
        private const int ROTATIONS_AMOUNT = 16;

        // Constructor
        public Manager(GameObject[] objs, Vector[] vectors, Move move)
        {
            this._Vectors = vectors;
            this._VectorsO = objs;
            this._Move = move;
            FixPosition(); 
        }

        // Unity function - Manage program per frame
        public void Manage()
        {
            CheckAction();
            rotation = CheckRotation();

            if (rotation != ' ')
            {
                _Move.Rotation = rotation;
                _Vectors = Move.Spin(_Move);
                FixPosition();
            }
        }

        // Unity function - update vectors position
        private void FixPosition()
        {
            for (int i = 0; i < 26; i++)
                _VectorsO[i].transform.position = new Vector3(_Vectors[i].X, _Vectors[i].Y, _Vectors[i].Z);
        }
        
        // Unity function - check for rotation
        private char CheckRotation()
        {
            rm = Input.GetKeyDown(KeyCode.E); lm = Input.GetKeyDown(KeyCode.Q); bm = Input.GetKeyDown(KeyCode.D);
            fm = Input.GetKeyDown(KeyCode.A); um = Input.GetKeyDown(KeyCode.C); dm = Input.GetKeyDown(KeyCode.Z);
            vm = Input.GetKeyDown(KeyCode.W); hm = Input.GetKeyDown(KeyCode.X); mm = Input.GetKeyDown(KeyCode.S);
            shiftm = Input.GetKeyDown(KeyCode.LeftShift);

            if (shiftm) flag = true;
            if (rm || lm || bm || fm || um || dm || vm || hm || mm)
            {
                if (rm) mv = 'r';
                else if (lm) mv = 'l';
                else if (bm) mv = 'b';
                else if (fm) mv = 'f';
                else if (um) mv = 'u';
                else if (dm) mv = 'd';
                else if (vm) mv = 'v';
                else if (hm) mv = 'h';
                else if (mm) mv = 'm';
                if (flag)
                {
                    mv = char.ToUpper(mv);
                    flag = false;
                }
                return mv;
            }
            return ' ';
        }

        // Unity function - actions possible: scramble or reset
        private void CheckAction()
        {
            bool scramble = Input.GetKeyDown(KeyCode.M);
            if (scramble) 
            {
                Debug.Log("pressed");
                
                for (int i = 0; i < SCRAMBLE_AMOUNT; i++)
                {
                    _Move.Rotation = MOVES[rnd.Next(0, ROTATIONS_AMOUNT )];
                    Debug.Log(_Move.Rotation);
                    _Vectors = Move.Spin(_Move);
                }
                scramble = false;

            }
            
        }
    }

    void Start()
    {
        Vector g = new Vector(0, 0, -1), b = new Vector(0, 0, 1), w = new Vector(0, -1, 0), y = new Vector(0, 1, 0), r = new Vector(-1, 0, 0), o = new Vector(1, 0, 0);
        Vector gow = new Vector(1, -1, -1), goy = new Vector(1, 1, -1), bow = new Vector(1, -1, 1), boy = new Vector(1, 1, 1), gwr = new Vector(-1, -1, -1);
        Vector gyr = new Vector(-1, 1, -1), bwr = new Vector(-1, -1, 1), byr = new Vector(-1, 1, 1);
        Vector gw = new Vector(0, -1, -1), gy = new Vector(0, 1, -1), gr = new Vector(-1, 0, -1), go = new Vector(1, 0, -1), bw = new Vector(0, -1, 1), by = new Vector(0, 1, 1);
        Vector br = new Vector(-1, 0, 1), bo = new Vector(1, 0, 1), wr = new Vector(-1, -1, 0), wo = new Vector(1, -1, 0), yr = new Vector(-1, 1, 0), yo = new Vector(1, 1, 0);

        GameObject[] vectorsO = { gc, bc, wc, yc, rc, oc, gowc, goyc, bowc, boyc, gwrc, gyrc, bwrc, byrc, gwc, gyc, grc, goc, bwc, byc, brc, boc, wrc, woc, yrc, yoc };
        Vector[] vectors = { g, b, w, y, r, o, gow, goy, bow, boy, gwr, gyr, bwr, byr, gw, gy, gr, go, bw, by, br, bo, wr, wo, yr, yo };

        manager = new Manager(vectorsO, vectors, new Move(vectors, vectorsO));
    }

    void FixedUpdate()
    {
        manager.Manage();
    }
}