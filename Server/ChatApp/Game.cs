using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mGame;
using EasyPOI;
namespace ChatApp
{
    public class LevelStateOnline : LevelState
    {
        public LevelStateOnline(int player, string player1name, string player2name) : 
            base(player,player1name, player2name) { }
        public override void PlayerInput(Direction direction, int gridX, int gridY)
        {
            UdpPacket udpPacket = new UdpPacket(UdpPacketType.PlayerInput);
            udpPacket.WriteData(BitConverter.GetBytes(ClientSession.GameSessionChatID));
            udpPacket.WriteData(BitConverter.GetBytes(ClientSession.IsPlayerOne ? 1 : 2));
            udpPacket.WriteData(BitConverter.GetBytes((int)direction));
            udpPacket.WriteData(BitConverter.GetBytes(gridX));
            udpPacket.WriteData(BitConverter.GetBytes(gridY));
            ClientSession.Connection.SendUdpPacket(udpPacket);
        }
        public override void PlayerShot()
        {
            UdpPacket udpPacket = new UdpPacket(UdpPacketType.PlayerShot);
            udpPacket.WriteData(BitConverter.GetBytes(ClientSession.GameSessionChatID));
            udpPacket.WriteData(BitConverter.GetBytes(ClientSession.IsPlayerOne ? 1 : 2));
            ClientSession.Connection.SendUdpPacket(udpPacket);
        }
        public override void RandomBotInput(Direction direction, int robotID, int gridX, int gridY)
        {
            UdpPacket udpPacket = new UdpPacket(UdpPacketType.RandomBotInput);
            udpPacket.WriteData(BitConverter.GetBytes(ClientSession.GameSessionChatID));
            udpPacket.WriteData(BitConverter.GetBytes(ClientSession.IsPlayerOne ? 1 : 2));
            udpPacket.WriteData(BitConverter.GetBytes((int)direction));
            udpPacket.WriteData(BitConverter.GetBytes(robotID));
            udpPacket.WriteData(BitConverter.GetBytes(gridX));
            udpPacket.WriteData(BitConverter.GetBytes(gridY));
            ClientSession.Connection.SendUdpPacket(udpPacket);
        }
        //public override void EnemyKilled()
        //{
        //    base.EnemyKilled();
        //}
        public override void Out()
        {
            Packet packet = new Packet(PacketType.EnemyKilled);
            packet.tag["user"] = ClientSession.username;
            packet.tag["enemiesKilled"] = enemiesKill;
            ClientSession.Connection.SendPacket(packet);
            base.Out();
        }
        public void MovePlayer(Direction direction, int gridX, int gridY)
        {
            var player = players[playerNumber == 1 ? 0 : 1];
            player.Move(direction);
            if ((int)player.GridPosition.X != gridX || 
                (int)player.GridPosition.Y != gridY)
                player.SetTile(gridX, gridY);
        }
        public void MakeShot()
        {
            players[playerNumber == 1 ? 0 : 1].Shot();
        }
        public void MoveRandomBot(Direction direction, int randomBotID, int gridX, int gridY)
        {
            if (!ClientSession.IsPlayerOne)
            {
                try
                {
                    RandomBot randomBot = randomBots[randomBotID];
                    switch (direction)
                    {
                        case Direction.Right:
                            randomBot.MoveRight();
                            break;
                        case Direction.Left:
                            randomBot.MoveLeft();
                            break;
                        case Direction.Up:
                            randomBot.MoveUp();
                            break;
                        case Direction.Down:
                            randomBot.MoveDown();
                            break;
                    }
                    if ((int)randomBot.GridPosition.X != gridX || 
                        (int)randomBot.GridPosition.Y != gridY)               
                        randomBot.SetTile(gridX, gridY);
                }
                catch { }
            }
        }
    }
    public class ConnectingState : GameState
    {
        private TextSprite text;
        private float elapse;
        public override void Init()
        {
            text = new TextSprite(Assets.retroFont, camera.Value.Center.ToVector2());
            text.text = "Esperando aliado...";
            text.origin = new Microsoft.Xna.Framework.Vector2(180, 10);
            AddGraphic(text);
            base.Init();
        }
        public override void Update()
        {
            elapse += (float)POIGame.DeltaTime * 0.7f;
            float effect = 1.0f + (float)Math.Sin(elapse) * 0.125f;
            text.scale = new Microsoft.Xna.Framework.Vector2(effect, effect);
            base.Update();
        }
    }
}
