using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WinAPI_1
{
    class Program
    {

        const int MAX_PIXEL_DIFF = 11;
        
        const int MIN_MOUSE_DEFAULT_MOVE_TICK = 80;
        const int MAX_MOUSE_DEFAULT_MVOE_TICK = 140;

        const int MIN_MOUSE_FIX_MOVE_TICK = 5;
        const int MAX_MOUSE_FIX_MOVE_TICK = 10;

        static void Main(string[] args)
        {
            while(true)
            {
                Console.Write("Enter cmd: ");
                string cmd = Console.ReadLine();
                switch(cmd)
                {
                    case "run":
                        MoveCursor(new Point(799, 912));
                        break;

                    case "exit": 
                        return;
                }
            }
        }

        private static void MoveCursor(Point end, bool isFix = false)  
        {
            // random time to move from 0.8 to 1.4 sec + fix up to 0.2 sec

            Random r = new Random();
            int ticks = isFix ? r.Next(MIN_MOUSE_FIX_MOVE_TICK, MAX_MOUSE_FIX_MOVE_TICK) : r.Next(MIN_MOUSE_DEFAULT_MOVE_TICK, MAX_MOUSE_DEFAULT_MVOE_TICK);

            int[] pointsDistance = GetDistanceBetweenPoints(Cursor.Position, end);
            int offsetX = pointsDistance[0] / ticks;
            int offsetY = pointsDistance[1] / ticks;

            for (int t = 0; t < ticks; t++)
            {
                WinAPI.SetCursorPos(Cursor.Position.X + offsetX, Cursor.Position.Y + offsetY);
                Thread.Sleep( 10 );
            }


            int[] endPointsDistance = GetDistanceBetweenPoints(Cursor.Position, end);
            if (endPointsDistance[0] > MAX_PIXEL_DIFF || endPointsDistance[1] > MAX_PIXEL_DIFF)
                MoveCursor(end, true);

            if (isFix) return;

            WinAPI.SetCursorPos(end.X, end.Y);
        }

        private static int[] GetDistanceBetweenPoints(Point start, Point end)
        {
            // return int[0] - distance between X coords
            // return int[1] - distance between Y coords

            int distanceX = start.X > end.X ?
                -(start.X - end.X) :
                end.X - start.X;

            int distanceY = start.Y > end.Y ?
                -(start.Y - end.Y) :
                end.Y - start.Y;

            return new int[2] { distanceX, distanceY};
        }
    }
}
