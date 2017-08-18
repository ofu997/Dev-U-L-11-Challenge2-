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
		// ADD BATTLE

		public class Battle
		{
			private List<Card> _bounty;
			private System.Text.StringBuilder _sb;

			public Battle()
			{
				_bounty = new List<Card>();
				_sb = new System.Text.StringBuilder();
			}

			public string PerformBattle(Player player1, Player player2)
			{
				Card player1Card = getCard(player1);
				Card player2Card = getCard(player2);

				performEvaluation(player1, player2, player1Card, player2Card);
				return _sb.ToString();
			}


			private Card getCard(Player player)
			{
				Card card = player.Cards.ElementAt(0);
				player.Cards.Remove(card);
				_bounty.Add(card);
				return card;
			}

			private void performEvaluation(Player player1, Player player2, Card card1, Card card2)
			{
				displayBattleCards(card1, card2);
				if (card1.CardValue() == card2.CardValue())
					war(player1, player2);
				if (card1.CardValue() > card2.CardValue())
					awardWinner(player1);
				else
					awardWinner(player2);

			}

			private void awardWinner(Player player)
			{
				if (_bounty.Count == 0) return;
				displayBountyCards();
				player.Cards.AddRange(_bounty);
				_bounty.Clear();

				_sb.Append("<br/><strong>");
				_sb.Append(player.Name);
				_sb.Append(" wins!</strong><br/>");
			}

			private void war(Player player1, Player player2)
			{
				_sb.Append("<br/>************WAR***************<br/>");
				getCard(player1);
				Card warCard1 = getCard(player1);
				getCard(player1);

				getCard(player2);
				Card warCard2 = getCard(player2);
				getCard(player2);

				performEvaluation(player1, player2, warCard1, warCard2);
			}

			private void displayBattleCards(Card card1, Card card2)
			{
				_sb.Append("<br/>Battle Cards: ");
				_sb.Append(card1.Kind);
				_sb.Append(" of ");
				_sb.Append(card1.Suit);
				_sb.Append(": worth ");
				_sb.Append( card1.CardValue() );
				_sb.Append(" versus ");
				_sb.Append(card2.Kind);
				_sb.Append(" of ");
				_sb.Append(card2.Suit);
				_sb.Append(": worth ");
				_sb.Append(card2.CardValue());
			}

			private void displayBountyCards()
			{
				_sb.Append("<br/>Bounty ...");

				foreach (var card in _bounty)
				{
					_sb.Append("<br/>&nbsp;&nbsp;&nbsp;&nbsp;");
					_sb.Append(card.Kind);
					_sb.Append(" of ");
					_sb.Append(card.Suit);
				}

			}


		}

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
					result += "<br/><span style='color:blue;font-weight:bolder;'>PLAYER 1 WINS</span>";
				else
					result += "<br/><span style='color:blue;font-weight:bolder;'>PLAYER 2 WINS</span>";

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

