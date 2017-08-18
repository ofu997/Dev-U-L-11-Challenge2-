using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevUL11Challenge2_War
{
	public partial class WebForm2 : System.Web.UI.Page
	{
		// EDIT PLAYER, ADD BATTLE 
		public class Card
		{
			public string Suit { get; set; }
			public string Kind { get; set; }

			public int CardValue()
			{
				int value = 0;

				switch (this.Kind)
				{
					case "Jack":
						value = 11;
						break;
					case "Queen":
						value = 12;
						break;
					case "King":
						value = 13;
						break;
					case "Ace":
						value = 14;
						break;
					default:
						value = int.Parse(this.Kind);
						break;
				}
				return value;
			}
		}

		public class Deck
		{
			private List<Card> _deck;
			private Random _random;
			private System.Text.StringBuilder _sb;

			public Deck()
			{
				_deck = new List<Card>();
				_random = new Random();
				_sb = new System.Text.StringBuilder();

				string[] suits = new string[] { "Clubs", "Diamonds", "Hearts", "Spades" };
				string[] kinds = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

				foreach (var suit in suits)
				{
					foreach (var kind in kinds)
					{
						_deck.Add(new Card() { Suit = suit, Kind = kind });
					}
				}
			}

			// uses another player variable
			public string Deal(Player player1, Player player2)
			{
				while (_deck.Count > 0)
				{
					// Deal a card to each player randomly
					dealCard(player1);
					dealCard(player2);
				}
				return _sb.ToString();
			}
			// uses another player variable
			private void dealCard(Player player)
			{
				Card card = _deck.ElementAt(_random.Next(_deck.Count));
				player.Cards.Add(card);
				_deck.Remove(card);

				_sb.Append("<br/>");
				_sb.Append(player.Name);
				_sb.Append(" is dealt the ");
				_sb.Append(card.Kind);
				_sb.Append(" of ");
				_sb.Append(card.Suit);
				//
				_sb.Append(" Round: ");
				_sb.Append(player.Cards.Count);
			}

		}
		// End of Deck class 
		public class Game
		{
			private Player ourP1;
			private Player ourP2;
			// private List<Card> _bounty;

			public Game(string player1Name, string player2Name)
			{
				ourP1 = new Player() { Name = player1Name };
				ourP2 = new Player() { Name = player2Name };
				// _bounty = new List<Card>();
			}

			public string Play()
			{
				Deck deck = new Deck();
				string result = "<h3>dealing cards   ..</h3>";
				result += deck.Deal(ourP1, ourP2);
				result += "<h3>begin battle ...</h3>";

				int round = 0;
				while (ourP1.Cards.Count != 0 && ourP2.Cards.Count != 0)
				{
					Battle battle = new Battle();
					result += battle.PerformBattle(ourP1, ourP2);

					performEvaluation(ourP1, ourP2, player1Card, player2Card);

					round++;
					if (round > 20)
						break;
				}
				// Determine the winner
				result += determineWinner();
				return result;
			}

			private string determineWinner()
			{
				string result = "";
				if (ourP1.Cards.Count > ourP2.Cards.Count)
					result += "<br/><br/>Player 1 wins";
				else
					result += "<br/><br/>Player 2 wins";

				result += "<br/><br/>Player 1: " + ourP1.Cards.Count + "<br/>Player2: " + ourP2.Cards.Count;
				return result;
			}
			/*
			private Card getCard(Player player)
			{
				Card card = player.Cards.ElementAt(0);
				player.Cards.Remove(card);
				_bounty.Add(card);
				return card;
			}

			private void performEvaluation(Player player1, Player player2, Card card1, Card card2)
			{
				if (card1.CardValue() > card2.CardValue())
					player1.Cards.AddRange(_bounty);
				else
					player2.Cards.AddRange(_bounty);
				_bounty.Clear();
			}
			*/
		} // end GAME

		public class Player
		{
			public string Name { get; set; }
			public List<Card> Cards { get; set; }

			public Player()
			{
				Cards = new List<Card>();
			}

		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Button1_Click(object sender, EventArgs e)
		{   /*
			Deck deck = new Deck();

			foreach (var card in deck._deck)
			{
				resultLabel.Text += "<br/>" + card.Suit + " " + card.Kind;
			}
			*/
			Game game = new Game("p1","p2");
			resultLabel.Text= game.Play();
		}
	}
}

