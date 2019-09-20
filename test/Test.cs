using ai;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions;

namespace test
{
    public class Test
    {
        [Fact]
        public void Deserialize_Game_Message()
        {
            const string input = @"{""board"":[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,1,0,0,0],[0,0,0,1,1,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]],""maxTurnTime"":15000,""player"":2}";
            var obj = JsonConvert.DeserializeObject<GameMessage>(input);

            obj.maxTurnTime.Should().Be(15000);
            obj.player.Should().Be(2);
            obj.board.Length.Should().Be(8);
            obj.board[0].Length.Should().Be(8);
            obj.board[0][0].Should().Be(0);
            obj.board[3][3].Should().NotBe(0);

        }
        
        [Fact]
        public void Check_is_Valid_Move()
        {
            const string input = @"{""board"":[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,1,0,0,0],[0,0,0,1,1,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]],""maxTurnTime"":15000,""player"":2}";
            GameMessage obj = JsonConvert.DeserializeObject<GameMessage>(input);
            bool vaild = AI.IsValidMove(obj, 6, 4, 1);
            vaild.Should().Be(true);
        }

        [Fact]
        public void Check_Player_Move_Score()
        {

        }

        [Fact]
        public void Check_Opp_Move_Score()
        {

        }

    }
}
