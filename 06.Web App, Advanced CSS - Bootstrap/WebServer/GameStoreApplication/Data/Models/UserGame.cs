namespace WebServer.GameStoreApplication.Data.Models
{
    public class UserGame // Many To Many
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}