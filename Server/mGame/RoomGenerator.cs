using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mGame
{
    class RoomGenerator
    {
        private enum NodeType { Up, Left, Down, Right }
        public static Random Rand = new Random();
        private class Room
        {
            private static List<Room> totalRooms = new List<Room>();
            public static int MaxRoomSize { get; set; }
            public static int MinRoomSize { get; set; }
            public static int MaxDisplacement { get; set; }
            public static int MinDisplacement { get; set; }
            public static int DistanceToConnect { get; set; } = 10;
            public static List<Room> TotalRooms { get { return totalRooms; } }
            private bool[] nodes;
            private Rectangle bounds;
            private Rectangle corridorBounds;
            private Rectangle connectionBounds;
            public Rectangle Bounds { get { return bounds; } }
            public Rectangle CorridorBounds { get { return corridorBounds; } }
            public Rectangle ConnectionBounds { get { return connectionBounds; } }
            public Room(Rectangle rectangle) {
                bounds = rectangle;
                nodes = new bool[4];
                totalRooms.Add(this);
            }
            //From which node came
            protected Room(Room parent, NodeType originNode) {
                nodes = new bool[4];
                //nodes[(int)originNode] = true;
                nodes[((int)originNode + 2)% 4] = true;
                bounds = new Rectangle(0, 0, Rand.Next(MinRoomSize, MaxRoomSize), Rand.Next(MinRoomSize, MaxRoomSize));
                bounds.Location = parent.bounds.Location;
                int displacementLenght = Rand.Next(MinDisplacement, MaxDisplacement);
                Point newLocation = bounds.Location;
                //Set position displacement and corridor calculation
                int parentWidth = parent.Bounds.Width - 1;
                int thisWidth = bounds.Width - 1;
                int parentHeight = parent.Bounds.Height - 1;
                int thisHeight = bounds.Height - 1;
                switch (originNode) {
                    case NodeType.Up: {
                            newLocation.Y -= bounds.Height + displacementLenght;
                            corridorBounds.Y = parent.Bounds.Top - displacementLenght;
                            corridorBounds.X = parent.Bounds.Left + Rand.Next(parentWidth >= thisWidth ? thisWidth : parentWidth);
                            corridorBounds.Height = displacementLenght;
                            corridorBounds.Width = 2;
                        }
                        break;
                    case NodeType.Left: {
                            newLocation.X -= bounds.Width + displacementLenght;
                            corridorBounds.X = parent.Bounds.Left - displacementLenght;
                            corridorBounds.Y = parent.Bounds.Top + Rand.Next(parentHeight >= thisHeight ? thisHeight : parentHeight);
                            corridorBounds.Width = displacementLenght;
                            corridorBounds.Height = 2;
                        }
                        break;
                    case NodeType.Down: {
                            newLocation.Y += parent.bounds.Height + displacementLenght;
                            corridorBounds.Y = parent.Bounds.Bottom;
                            corridorBounds.X = parent.Bounds.Left + Rand.Next(parentWidth >= thisWidth ? thisWidth : parentWidth);
                            corridorBounds.Height = displacementLenght;
                            corridorBounds.Width = 2;
                        }
                        break;
                    case NodeType.Right: {
                            newLocation.X += parent.bounds.Width + displacementLenght;
                            corridorBounds.X = parent.Bounds.Right;
                            corridorBounds.Y = parent.Bounds.Top + Rand.Next(parentHeight >= thisHeight ? thisHeight : parentHeight);
                            corridorBounds.Width = displacementLenght;
                            corridorBounds.Height = 2;
                        }
                        break;
                }
                bounds.Location = newLocation;
            }
            private bool CollideRooms() {
                Rectangle fixedBounds = bounds;
                fixedBounds.X--;
                fixedBounds.Y--;
                fixedBounds.Width += 2;
                fixedBounds.Height += 2;
                foreach (var room in totalRooms) {
                    if (fixedBounds.Intersects(room.Bounds) && bounds !=room.bounds)
                        return true;
                }
                return false;
            }
            /// <summary>
            /// Checks if there's a room nearby and returns a rectangle connecting with it,
            /// if no connection exists it returns an empty rectangle
            /// </summary>
            public void CheckConnection() {
                connectionBounds = new Rectangle();
                for(int i = 0; i < nodes.Length; i++) {
                    //Check an empty node
                    if(!nodes[i]) {
                        Rectangle connectionTest = new Rectangle();
                        switch((NodeType)i) {
                            //Super duper hardcode ahead...
                            case NodeType.Up:
                                connectionTest.Width = bounds.Width;
                                connectionTest.Height = DistanceToConnect;
                                connectionTest.X = bounds.Left;
                                connectionTest.Y = bounds.Top - DistanceToConnect;
                                //Check for a room to connect
                                foreach (var room in totalRooms) {
                                    if (room.ConnectionBounds.IsEmpty) {
                                        Rectangle connectionIntersection = Rectangle.Intersect(connectionTest, room.bounds);
                                        if (!connectionIntersection.IsEmpty) {
                                            connectionBounds.Width = 2;
                                            connectionBounds.Height = bounds.Top - connectionIntersection.Bottom;
                                            connectionBounds.X = connectionIntersection.Left + Rand.Next(connectionIntersection.Width);
                                            connectionBounds.Y = connectionIntersection.Bottom;
                                        }
                                    }
                                }
                                break;
                            case NodeType.Left:
                                connectionTest.Height = bounds.Width;
                                connectionTest.Width = DistanceToConnect;
                                connectionTest.Y = bounds.Top;
                                connectionTest.X = bounds.Left - DistanceToConnect;
                                //Check for a room to connect
                                foreach (var room in totalRooms) {
                                    if (room.ConnectionBounds.IsEmpty) {
                                        Rectangle connectionIntersection = Rectangle.Intersect(connectionTest, room.bounds);
                                        if (!connectionIntersection.IsEmpty) {
                                            connectionBounds.Height = 2;
                                            connectionBounds.Width = bounds.Left - connectionIntersection.Right;
                                            connectionBounds.Y = connectionIntersection.Top + Rand.Next(connectionIntersection.Height);
                                            connectionBounds.X = connectionIntersection.Right;
                                        }
                                    }
                                }
                                break;
                            case NodeType.Down:
                                connectionTest.Width = bounds.Width;
                                connectionTest.Height = DistanceToConnect;
                                connectionTest.X = bounds.Left;
                                connectionTest.Y = bounds.Bottom;
                                //Check for a room to connect
                                foreach (var room in totalRooms) {
                                    if(room.ConnectionBounds.IsEmpty) {
                                        Rectangle connectionIntersection = Rectangle.Intersect(connectionTest, room.bounds);
                                        if (!connectionIntersection.IsEmpty) {
                                            connectionBounds.Width = 2;
                                            connectionBounds.Height = connectionIntersection.Top - bounds.Bottom;
                                            connectionBounds.X = connectionIntersection.Left + Rand.Next(connectionIntersection.Width);
                                            connectionBounds.Y = bounds.Bottom;
                                        }
                                    }
                                }
                                break;
                            case NodeType.Right:
                                connectionTest.Height = bounds.Height;
                                connectionTest.Width = DistanceToConnect;
                                connectionTest.Y = bounds.Top;
                                connectionTest.X = bounds.Right;
                                //Check for a room to connect
                                foreach (var room in totalRooms) {
                                    if (room.ConnectionBounds.IsEmpty) {
                                        Rectangle connectionIntersection = Rectangle.Intersect(connectionTest, room.bounds);
                                        if (!connectionIntersection.IsEmpty) {
                                            connectionBounds.Height = 2;
                                            connectionBounds.Width = connectionIntersection.Left - bounds.Right;
                                            connectionBounds.Y = connectionIntersection.Top + Rand.Next(connectionIntersection.Height);
                                            connectionBounds.X = bounds.Right;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            public List<Room> GenerateChilds() {
                List<Room> childs = new List<Room>();
                int retries = 0;
                //minimun one new child
                int minChildIndex = Rand.Next(4);
                while (nodes[minChildIndex]) { minChildIndex = Rand.Next(4); }
                //adding child...
                Room minChild;
                do { minChild = new Room(this, (NodeType)minChildIndex); retries++; }
                while (minChild.CollideRooms() && retries < 100);
                if (retries < 100) {
                    childs.Add(minChild);
                    nodes[minChildIndex] = true;
                }
                //
                for (int i = 0; i < nodes.Length; i++) {
                    //If there's no node
                    if (!nodes[i]) {
                        //Random creation chance
                        if (Rand.Next(2) == 1) {
                            Room newRoom;
                            retries = 0;
                            do { newRoom = new Room(this, (NodeType)i); retries++; }
                            while (newRoom.CollideRooms() && retries < 100);
                            if (retries < 100) {
                                childs.Add(newRoom);
                                nodes[i] = true;
                            }
                        }
                    }
                }
                totalRooms.AddRange(childs);
                return childs;
            }
            //Fills all neighbors
            public List<Room> GenerateAll() {
                List<Room> childs = new List<Room>();
                for (int i = 0; i < nodes.Length; i++) {
                    //If there's no node
                    if (!nodes[i]) {
                        Room newRoom = new Room(this, (NodeType)i);
                        nodes[i] = true;
                        childs.Add(newRoom);
                    }
                }
                totalRooms.AddRange(childs);
                return childs;
            }
            public static void Reset() {
                totalRooms = new List<Room>();
            }
        }
        public static GraphicsDevice graphicsDevice { get; set; }
        public int TotalWidth { get; set; }
        public int TotalHeight { get; set; }
        public RoomGenerator(int totalWidth, int totalHeight) {
            TotalWidth = totalWidth;
            TotalHeight = totalHeight;
        }
        private T[] ArrayFill<T>(T value, int size) {
            T[] array = new T[size];
            for (int i = 0; i < array.Length; i++) { array[i] = value; }
            return array;
        }
        public UInt32[] Generate(int minRoomSize, int maxRoomSize, int minRoomSeparation, int maxRoomSeparation, int generations = 5 ,int distanceToConnect = 10) {
            //Uses texture2d to fill the data...
            Texture2D levelData = new Texture2D(graphicsDevice, TotalWidth, TotalHeight);
            //init level generation
            Room.MinRoomSize = minRoomSize;
            Room.MaxRoomSize = maxRoomSize;
            Room.MinDisplacement = minRoomSeparation;
            Room.MaxDisplacement = maxRoomSeparation;
            Room.DistanceToConnect = distanceToConnect;
            Room.Reset();
            List<Room> lastGeneratedChilds = new List<Room>();
            Room originRoom = new Room(new Rectangle(TotalWidth / 2, TotalHeight / 2, 16, 12));
            lastGeneratedChilds.AddRange(originRoom.GenerateAll());
            for (int i = 0; i < generations; i++) {
                List<Room> newGeneratedChilds = new List<Room>();
                foreach (Room room in lastGeneratedChilds) {
                    newGeneratedChilds.AddRange(room.GenerateChilds());
                }
                lastGeneratedChilds = newGeneratedChilds;
            }
            foreach (var room in Room.TotalRooms) {
                room.CheckConnection();
                levelData.SetData(0, room.Bounds, ArrayFill(0xffffffff, TotalWidth * TotalHeight), 0, TotalWidth * TotalHeight);
                levelData.SetData(0, room.CorridorBounds, ArrayFill(0xFFFFFF00, TotalWidth * TotalHeight), 0, TotalWidth * TotalHeight);
                levelData.SetData(0, room.ConnectionBounds, ArrayFill(0xff0000ff, TotalWidth * TotalHeight), 0, TotalWidth * TotalHeight);
            }
            UInt32[] output = new UInt32[TotalWidth * TotalHeight];
            levelData.GetData(output, 0, output.Length);
            return output;
        }
    }
}
