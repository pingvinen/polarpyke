using System;

namespace PolarPyke.Parsing.Ebnf
{
	public class Token : IEquatable<Token>
	{
		public Token()
		{
			this.Content = String.Empty;
		}

		public TokenType Type { get; set; }

		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the line number in the
		/// EBNF document where the token is defined
		/// </summary>
		public int LineNumber { get; set; }

		/// <summary>
		/// Gets or sets the character position in
		/// the line
		/// </summary>
		public int CharacterPosition { get; set; }

		public override string ToString()
		{
			return string.Format("[Token: Type={0}, Content='{1}', LineNumber={2}, CharacterPosition={3}]", Type, Content, LineNumber, CharacterPosition);
		}

		#region IEquatable implementation
		public bool Equals(Token other)
		{
			if (!this.Type.Equals(other.Type))
			{
				return false;
			}

			if (!this.CharacterPosition.Equals(other.CharacterPosition))
			{
				return false;
			}

			if (!this.LineNumber.Equals(other.LineNumber))
			{
				return false;
			}

			if (!this.Content.Equals(other.Content))
			{
				return false;
			}

			return true;
		}
		#endregion

	}
}