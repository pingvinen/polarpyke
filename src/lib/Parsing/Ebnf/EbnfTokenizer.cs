using System;
using System.Collections.Generic;

namespace PolarPyke.Parsing.Ebnf
{
	public class EbnfTokenizer
	{
		public EbnfTokenizer()
		{
		}

		private string source;
		private int linenumber;
		private int charposinsource;
		private int charposinline;

		public IList<Token> Tokenize(string ebnf)
		{
			List<Token> tokens = new List<Token>();

			this.source = ebnf;
			this.linenumber = 0;
			this.charposinsource = -1;
			this.charposinline = -1;

			char cur = '\0';
			while (this.charposinsource < this.source.Length-1)
			{
				this.charposinsource++;
				this.charposinline++;
				cur = this.source[this.charposinsource];

				if (cur == '\n')
				{
					//
					// newlines must be detected
					// in order to help with the
					// error message data
					//
					this.linenumber++;
					this.charposinline = 0;
					continue;
				}

				if (Char.IsWhiteSpace(cur))
				{
					// skip whitespaces
					continue;
				}

				if (cur == ';')
				{
					tokens.Add(new Token() {
						CharacterPosition = this.charposinline,
						LineNumber = this.linenumber,
						Type = TokenType.Termination
					});
					continue;
				}

				if (Char.IsLetter(cur))
				{
					tokens.Add(this.GetIdentifier());
					continue;
				}

				if (cur == '=')
				{
					tokens.Add(new Token() {
						CharacterPosition = this.charposinsource,
						LineNumber = this.linenumber,
						Type = TokenType.Definition
					});
					continue;
				}

				if (cur == '"')
				{
					tokens.Add(this.GetString());
					continue;
				}
			}

			return tokens;
		}

		#region Get identifier
		private Token GetIdentifier()
		{
			Token t = new Token() {
				CharacterPosition = this.charposinline,
				LineNumber = this.linenumber,
				Type = TokenType.Identifier,
				Content = String.Empty
			};

			int start = this.charposinsource;
			int substringlength = 0;

			char cur = '\0';
			while (this.charposinsource < this.source.Length)
			{
				cur = this.source[this.charposinsource];

				//
				// in our world an identifier can consist
				// of letters, digits and underscore
				//
				if (!Char.IsLetterOrDigit(cur) && cur != '_')
				{
					t.Content = this.source.Substring(start, substringlength);
					return t;
				}

				substringlength++;
				this.charposinsource++;
				this.charposinline++;
			}

			throw new EbnfSyntaxException("Unable to find end of identifier");
		}
		#endregion Get identifier
	
		#region Get string
		private Token GetString()
		{
			Token t = new Token() {
				CharacterPosition = this.charposinline,
				LineNumber = this.linenumber,
				Type = TokenType.String,
				Content = String.Empty
			};

			int start = this.charposinsource + 1; // skip string initializer
			int substringlength = 0;

			char cur = '\0';
			while (this.charposinsource < this.source.Length)
			{
				//
				// we can skip the first char as this is
				// the string initializer
				//

				this.charposinsource++;
				this.charposinline++;

				cur = this.source[this.charposinsource];
		
				if (cur == '"')
				{
					t.Content = this.source.Substring(start, substringlength);
					return t;
				}

				substringlength++;
			}

			throw new EbnfSyntaxException("Unable to find end of string");
		}
		#endregion Get string
	}
}