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
		}

		public class Deck
		{	// lists should be private !!
			public List<Card> _deck;
			private Random randomDeck;
			private System.Text.StringBuilder _sb; 

			public Deck()
			{
				/* this would take forever
				_deck = new List<Card>() {
					new Card{ Suit="Clubs", Kind="2"},
				}
				*/
				_deck = new List<Card>();
				randomDeck=new Random();
				_sb = new System.Text.StringBuilder(); 
				string[] suits = new string[] { "Clubs", "Diamonds", "Hearts", "Screwdrivers" };
				string[] kinds = new string[] { "2", "3", "4","5","6","7","8","9","10","Jack","Queen","King","Ace"};

				foreach (var suit in suits)
				{
					foreach (var kind in kinds)
					{
						_deck.Add(new Card() { Suit = suit, Kind = kind });
					}
				}
			}
			public string Deal(Player p1, Player p2)
			{
				while (_deck.Count > 0)
				{
					dealCard(p1);
					dealCard(p2); 
				}
				// Sent these to dealCard function 
				//p1.Cards.Add(_deck.ElementAt(randomDeck.Next(1, _deck.Count)));
				//p2.Cards.Add(_deck.ElementAt(randomDeck.Next(1, _deck.Count)));
				return _sb.ToString(); 
			}
			private void dealCard(Player p)
			{
				Card card = _deck.ElementAt(randomDeck.Next(_deck.Count));
				p.Cards.Add(card);
				_deck.Remove(card);

				_sb.Append("<br/>");
				_sb.Append(p.Name);
				_sb.Append(" is dealt the ");
				_sb.Append(card.Kind);
				_sb.Append(" of");
				_sb.Append(card.Suit);
			}
		}
		// End of Deck class 
		public class Game
		{
			private Player p1;
			private Player p2; 

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

				
			}
			public string Play()
			{
				Deck deck = new Deck();
				return	deck.Deal(p1, p2); 
			}
		}

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