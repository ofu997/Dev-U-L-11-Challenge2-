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
				/*
				_deck = new List<Card>() {
					new Card { Suit="Clubs", Kind="2"},
					new Card { Suit ="Clubs", Kind="3"},
				}
				 */

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
				_sb.Append(" ");
				_sb.Append(player.Cards.Count);
			}

		}
		// End of Deck class 
		public class Game
		{
			private Player p1;
			private Player p2;
			private List<Card> _bounty;

			public Game(string p1name, string p2name)
			{
				p1 = new Player()
				{
					Name = p1name
				};
				p2 = new Player()
				{
					Name = p2name
				};
				_bounty = new List<Card>();
			}
			public string Play()
			{
				Deck deck = new Deck();
				string result=	deck.Deal(p1, p2);

				int round = 0;
				while (p1.Cards.Count != 0 && p2.Cards.Count != 0)
				{
					//Card p1Card = p1.Cards.ElementAt(0);
					//Card p2Card = p2.Cards.ElementAt(0);
					Card p1Card = getCard(p1);
					Card p2Card = getCard(p2);
					//if ( p1Card.CardValue >  p2Card.CardValue)
					//{}
					performEvaluation(p1, p2, p1Card, p2Card);

					round++;
					if (round > 20)
						break;
				}//determine
				result+= determineWinner();
				return result; 
			}// end Game.Play

			//private List<Card> _bounty; 

			private Card getCard(Player player)
			{
				Card card = player.Cards.ElementAt(0);
				player.Cards.Remove(card);
				_bounty.Add(card);
				return card;
			}

			private void performEvaluation(Player p1, Player p2, Card card1, Card card2)
			{
				if (card1.CardValue() > card2.CardValue())
					p1.Cards.AddRange(_bounty);
				else
				{
					p2.Cards.AddRange(_bounty);
				}

			}

			private string determineWinner()
			{
				string result = "";
				if (p1.Cards.Count > p2.Cards.Count)
					result += "<br/> p1 wins";
				else
					result += "<br/> p2 wins ";
				result += "<br/>Player 1: " + p1.Cards.Count + "  Player 2: " + p2.Cards.Count;
				return result;
			}

		}// End Game

		public class Player
		{
			public string Name { get; set; }
			public List<Card> Cards { get; set; }

			// added in last 
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


// perform evaluation 